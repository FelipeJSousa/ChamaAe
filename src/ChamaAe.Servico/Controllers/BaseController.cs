using System;
using System.Collections.Generic;
using AutoMapper;
using ChamaAe.Servico.Application.ViewModels;
using ChamaAe.Servico.Domain.Entities;
using ChamaAe.Servico.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ChamaAe.Servico.Controllers
{
    
     public abstract class BaseController : ControllerBase
    {
        protected readonly INotificationService Notifications;
        protected readonly IMapper Mapper;

        protected BaseController(IServiceProvider serviceProvider)
        {
            Mapper = serviceProvider.GetService<IMapper>();
            Notifications = serviceProvider.GetService<INotificationService>();
        }

        #region Mapper

        protected TRetorno Mapear<TRetorno>(object origem) => Mapper.Map<TRetorno>(origem);

        protected TDestination Merge<TDestination>(object origem, TDestination destino) => Mapper.Map(origem, destino);

        protected TDestination MergeInto<TDestination>(TDestination destino, object origem) => Mapper.Map(origem, Mapear<TDestination>(destino));

        #endregion

        #region Notifications

        protected void NewNotification(string key, string message, NotificationType type = NotificationType.Error) => Notifications.NewNotification(key, message, type);

        protected void NotificationErrors<TEntity>(TEntity model) where TEntity : IModelValidator? => Notifications.NotificationErrors(model);

        #endregion

        protected bool OperacaoValida()
        {
            return !Notifications.HasNotificationsErrors();
        }
    }
     
    public abstract class ApiBaseController : BaseController
    {
        protected ApiBaseController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected IActionResult ResponseGeneric<T>(T result)
        {
            if (!Notifications.HasNotificationsErrors())
            {
                return Ok(new ViewModel<T>(result, Notifications.GetNotifications()));
            }

            return BadRequest(new ViewModel<T>
            (
                path: Request.Path.Value,
                remoteAddress: HttpContext?.Connection.RemoteIpAddress?.ToString(),
                notifications: Notifications.GetNotifications()
            ));
        }

        protected IActionResult CreateResponse<T>(T result) where T : IComparable
        {
            if (!Notifications.HasNotifications())
            {
                return Created("", result);
            }

            if (OperacaoValida())
            {
                return Accepted(new ViewModel<T>
                (
                    notificationError: Notifications.GetNotifications()
                ));
            }

            return BadRequest(new ViewModel<T>
            (
                path: Request.Path.Value,
                remoteAddress: HttpContext?.Connection.RemoteIpAddress?.ToString(),
                notifications: Notifications.GetNotifications()
            ));
        }
        
        protected IActionResult Response<T>(T result) where T : IComparable => ResponseGeneric(result);

        protected IActionResult Response<T>(IEnumerable<T> result) where T : IComparable => ResponseGeneric(result);

        protected IActionResult Response(IEnumerable<dynamic> result) => ResponseGeneric(result);

    }
}