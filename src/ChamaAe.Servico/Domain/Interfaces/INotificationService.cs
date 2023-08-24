using System;
using System.Collections.Generic;
using System.Threading;
using ChamaAe.Servico.Domain.Entities;

namespace ChamaAe.Servico.Domain.Interfaces
{
    public interface INotificationService : IDisposable
    {
        void Handle(Notification notification, CancellationToken cancellationToken);

        void NewNotification(string key, string message, NotificationType type);

        void NotificationErrors<TEntity>(TEntity model) where TEntity : IModelValidator;

        IEnumerable<Notification> GetNotifications();

        bool HasNotifications();

        bool HasNotificationsErrors();
    }
}