using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChamaAe.Servico.Infra.Data.Extensions
{
    public static class EfCoreExtensions
    {
        public static Expression<Func<TEntity, bool>> FilterByPrimaryKeyPredicate<TEntity>(this DbContext dbContext, long id)
        {
            var array = new object[1];
            array[0] = id;

            return FilterByPrimaryKeyPredicate<TEntity>(dbContext, array);
        }

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes == null || includes.Length == 0)
            {
                return query;
            }

            return includes.Aggregate(query, (current, include) => EvaluateInclude(current, include));
        }

        private static IReadOnlyList<IProperty> GetPrimaryKeyProperties<TEntity>(this DbContext dbContext)
        {
            if (dbContext.Model.FindEntityType(typeof(TEntity)) is { } type)
            {
                return type.FindPrimaryKey().Properties;
            }

            return null;
        }

        private static Expression<Func<TEntity, bool>> FilterByPrimaryKeyPredicate<TEntity>(this DbContext dbContext, object[] id)
        {
            var keyProperties = dbContext.GetPrimaryKeyProperties<TEntity>();
            var parameter = Expression.Parameter(typeof(TEntity), "e");

            // e => e.PK[i] == id[i]
            var body = keyProperties?
                .Select((p, i) => Expression.Equal
                (
                    Expression.Property(parameter, p.Name),
                    Expression.Convert
                    (
                        Expression.PropertyOrField(Expression.Constant(new { id = id[i] }), "id"),
                        p.ClrType
                    )
                )).Aggregate(Expression.AndAlso);

            return body != null ? Expression.Lambda<Func<TEntity, bool>>(body, parameter) : null;
        }

        private static IQueryable<T> EvaluateInclude<T>(IQueryable<T> current, Expression<Func<T, object>> include) where T : class
        {
            if (include.Body is MethodCallExpression expression)
            {
                var arguments = expression.Arguments;
                if (arguments.Count > 1)
                {
                    var navigationPath = new StringBuilder();
                    for (var i = 0; i < arguments.Count; i++)
                    {
                        var arg = arguments[i];
                        var path = arg.ToString()[(arg.ToString().IndexOf('.') + 1)..];

                        navigationPath.Append((i > 0 ? "." : string.Empty) + path);
                    }

                    return current.Include(navigationPath.ToString());
                }
            }

            return current.Include(include);
        }
    }
}
