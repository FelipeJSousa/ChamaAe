using System;
using Microsoft.AspNetCore.Mvc;

namespace ChamaAe.Servico.Application.ViewModels
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public abstract class ViewModelBase : IComparable
    {
        public int CompareTo(object obj)
        {
            return (this == obj) ? 1 : 0;
        }
    }
}