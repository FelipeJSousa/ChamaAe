using ChamaAe.Servico.Application.Services;
using ChamaAe.Servico.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChamaAe.Servico.Infra.IoC;

public static class DependencyInjectorDotNet
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();       
    }
}