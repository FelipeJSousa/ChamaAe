using System;
using System.Reflection;
using AutoMapper;

namespace ChamaAe.Servico.Application.Automapper;

public static class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(ps =>
        {
            ps.AllowNullCollections = true;
                
            ps.AddProfile(new AutoMapperProfile());                
        });
    }

    public static void IgnoreSourceAndDefault<TSource, TDestination>(this IMemberConfigurationExpression<TSource, TDestination, object> opt)
    {
        var destinationType = opt.DestinationMember.GetMemberType();
        var defaultValue = destinationType.GetTypeInfo().IsValueType && destinationType != typeof(bool) ? Activator.CreateInstance(destinationType) : null;

        opt.Condition((_, _, srcValue, destValue) => !Equals(srcValue, defaultValue) && !Equals(srcValue, destValue));
    }

    public static void IgnoreSourceWhenDefault<TSource, TDestination>(this IMemberConfigurationExpression<TSource, TDestination, object> opt)
    {
        var destinationType = opt.DestinationMember.GetMemberType();
        var defaultValue = destinationType.GetTypeInfo().IsValueType && destinationType != typeof(bool) ? Activator.CreateInstance(destinationType) : null;

        opt.Condition((_, _, srcValue, _) => !Equals(srcValue, defaultValue));
    }

    public static void IgnoreWhenDestinationHasValue<TSource, TDestination>(this IMemberConfigurationExpression<TSource, TDestination, object> opt)
    {
        var destinationType = opt.DestinationMember.GetMemberType();
        var defaultValue = destinationType.GetTypeInfo().IsValueType && destinationType != typeof(bool) ? Activator.CreateInstance(destinationType) : null;

        opt.Condition((_, _, _, destValue) => Equals(destValue, defaultValue));
    }

    public static void SetNullFromNullableDefault(object source, object destination)
    {
        foreach (var sourceProperty in source.GetType().GetProperties())
        {
            var value = sourceProperty.GetValue(source, null);

            var underlyingType = Nullable.GetUnderlyingType(sourceProperty.PropertyType);
            if (underlyingType is { })
            {
                var defaultValue = underlyingType.IsValueType && underlyingType != typeof(bool) ? Activator.CreateInstance(underlyingType) : null;
                if (defaultValue != null && defaultValue.Equals(value))
                {
                    var destinationProperty = destination.GetType().GetProperty(sourceProperty.Name);

                    destinationProperty?.SetValue(destination, null);
                }
            }
            else if (sourceProperty.PropertyType == typeof(string) && value != null && string.Empty.Equals(value))
            {
                var destinationProperty = destination.GetType().GetProperty(sourceProperty.Name);

                destinationProperty?.SetValue(destination, null);
            }
        }
    }

    private static Type GetMemberType(this MemberInfo memberInfo)
    {
        return memberInfo switch
        {
            MethodInfo info => info.ReturnType,
            PropertyInfo info1 => info1.PropertyType,
            FieldInfo info2 => info2.FieldType,
            _ => null
        };
    }
}