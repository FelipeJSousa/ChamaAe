﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using ChamaAe.Servico.Domain.Entities;
using ChamaAe.Servico.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace ChamaAe.Servico.Application.Services;

public abstract class ServiceBase : IService
{
    private readonly IMapper _mapper;
    private readonly INotificationService _notifications;
    
    #region Constructor

    protected ServiceBase(IServiceProvider serviceProvider)
    {
        _mapper = serviceProvider.GetService<IMapper>();
        _notifications = serviceProvider.GetService<INotificationService>();
    }

    #endregion
        
    #region Mapper

    public TDestination Mapear<TDestination>(object origem) => _mapper.Map<TDestination>(origem);

    public TDestination Merge<TDestination>(TDestination destino, object origem) => _mapper.Map(origem, destino);

    public TDestination MergeInto<TDestination>(TDestination destino, object origem) => _mapper.Map(origem, Mapear<TDestination>(destino));

    #endregion

    #region Notification
    
    protected void NewNotification(string key, string message, NotificationType notificationType = NotificationType.Error) => _notifications.NewNotification(key, message, notificationType);

    protected void NotificationErrors<TEntity>(TEntity model) where TEntity : IModelValidator => _notifications.NotificationErrors(model);

    protected bool HasNotificationsErrors() => _notifications.HasNotificationsErrors();

    #endregion

    #region Dispose

    private bool _disposed;                         // To detect redundant calls

    public void Dispose()                           // Public implementation of Dispose pattern callable by consumers.
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)  // Protected implementation of Dispose pattern.
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
    }

    #endregion
}
    
public abstract class ServiceBase<TEntity> : ServiceBase, IService<TEntity> where TEntity : EntityBase
{
    private readonly IRepository<TEntity> _repoBase;

    protected ServiceBase(IServiceProvider serviceProvider, IRepository<TEntity> repoBase) : base(serviceProvider)
    {
        _repoBase = repoBase;
    }

    public virtual async Task<TEntity> Save(TEntity obj, bool forced = false)
    {
        var retorno = await SaveTransaction(obj);

        await Commit(forced);

        return retorno;
    }

    public virtual async Task<TEntity> Delete(TEntity obj, bool forced)
    {
        var retorno = await DeleteTransaction(obj);

        await Commit(forced);

        return retorno;
    }
        
    public virtual async Task<TEntity?> GetSingle(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Expression<Func<TEntity, TEntity>>? selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true)
    {
        var item = _repoBase.GetSingle(predicate, include, orderBy, selector, disableTracking, take, skip, useSplitQuery);
        return await Task.FromResult(item);
    }
        
    public virtual async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Expression<Func<TEntity, TEntity>>? selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true)
    {
        var lista = _repoBase.GetList(predicate, include, orderBy, selector, disableTracking, take, skip, useSplitQuery);
        return await Task.FromResult(lista);
    }
        
    protected virtual async Task<TEntity> SaveTransaction(TEntity obj)
    {
        if (obj == null)
        {
            NewNotification("Objeto", "Objeto não encontrado");
            return null;
        }

        if (obj.Id > 0)
        {
            obj = Update(obj);
        }
        else
        {
            obj = Add(obj);
        }

        return await Task.FromResult(obj);
    }

    protected virtual async Task<TEntity>  DeleteTransaction(TEntity obj)
    {
        if (obj is null)
        {
            NewNotification("Objeto DTO", "Objeto DTO não encontrado");
            return null;
        }

        if (obj.EhValidoRemover())
        {
            _repoBase.Delete(obj);
        }
        else
        {
            NotificationErrors(obj);
        }

        return await Task.FromResult(obj);
    }
        
    protected virtual TEntity Add(TEntity obj)
    {
        if (obj.EhValido())
        {
            _repoBase.Add(obj);
        }
        else
        {
            NotificationErrors(obj);
        }

        return obj;
    }

    protected virtual TEntity Update(TEntity obj)
    {
        var objExistente = GetSingle(x => x.Id == obj.Id, disableTracking: false).GetAwaiter().GetResult();
        if (objExistente == null)
        {
            NewNotification("Registro", "Registro não encontrado.");
            return obj;
        }

        var objMerge = MergeInto(objExistente, obj);                                              
        if (objMerge.EhValidoAlterar())                                                           
        {
            _repoBase.Update(objExistente, objMerge);
        }
        else
        {
            NotificationErrors(objMerge);
        }

        return objMerge;
    }

    #region UoW

    public async Task<bool> Commit(bool forced = false)
    {
        if (!forced && HasNotificationsErrors())
        {
            return await Task.FromResult(false);
        }
        
        if (_repoBase.Commit())
        {
            return await Task.FromResult(true);
        }
        
        NewNotification("Commit", "Ocorreu um erro ao salvar os dados no banco de dados");
        return await Task.FromResult(false);
    }
        
    #endregion
        
    #region Dispose

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _repoBase?.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion
}