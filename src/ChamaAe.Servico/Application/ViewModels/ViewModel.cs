using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using ChamaAe.Servico.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChamaAe.Servico.Application.ViewModels
{
    [DisplayName("ResponseAppBeneficiario")]
    [DataContract(Name = "ResponseAppBeneficiario", Namespace = "")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ViewModel<T> : ViewModelBase
    {
        public ViewModel(IEnumerable<Notification> notificationError)
        {
            var notificationsList = notificationError.ToList();
            
            if (notificationsList?.Any() != true) return;

            Errors = new List<ViewModelError>();
            foreach (var itemMessage in (notificationsList))
            {
                Errors.Add(new ViewModelError { Key = $"{itemMessage.Key}", Value = $"{itemMessage.Value}"});
            }
        }

        public ViewModel(string path, string remoteAddress, IEnumerable<Notification> notifications) : this(notifications)
        {
            Path = path;
            RemoteAddress = remoteAddress;
        }

        public ViewModel(T data, IEnumerable<Notification> notificationError)
        {
            Data = data;
            
            var notificationsList = notificationError.ToList();
            
            if (notificationsList?.Any() != true) return;
            
            foreach (var itemMessage in (notificationsList))
            {
                Message += $"{itemMessage.Value}" + Environment.NewLine;
            }
        }

        [DataMember(Name = "errors", Order = 99)]
        public List<ViewModelError> Errors { get; set; }

        [DataMember(Name = "remoteAddress")]
        public string? RemoteAddress { get; set; }
        
        [DataMember(Name = "path")]
        public string? Path { get; set; }
        
        [DataMember(Name = "data")]
        public T Data { get; set; }
        
        [DataMember(Name = "message")]
        public string? Message { get; set; }

    }

    public class ViewModelError : ViewModelBase
    {
        [DataMember(Name = "Key")]
        public string Key { get; set; }
        
        [DataMember(Name = "Value")]
        public string Value { get; set; }
    }
}