﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace ChamaAe.Servico.Domain.Interfaces
{
    public interface IService : IDisposable
    {
    }

    public interface IService<TEntity> : IService where TEntity : EntityBase
    {
        Task<TEntity> Save(TEntity obj, bool forced);

        Task<TEntity> Delete(TEntity obj, bool forced);

        Task<TEntity?> GetSingle(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Expression<Func<TEntity, TEntity>>? selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true);

        Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Expression<Func<TEntity, TEntity>>? selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true);
    }
}