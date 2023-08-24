using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChamaAe.Servico.Infra.IoC;

public static class InjectorBootstrap
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        DependencyInjectorApplication.Register(services, configuration);

        DependencyInjectorRepository.Register(services, configuration);
            
        DependencyInjectorDotNet.Register(services);
    }
}