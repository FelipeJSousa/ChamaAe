using ChamaAe.Servico.Application.Automapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChamaAe.Servico.Infra.IoC

{
    public static class ApiConfigurations
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            InjectorBootstrap.RegisterServices(services, configuration);

            // AuthenticationRegister.AddAuthentication(services);
            
            services.AddAutoMapper(typeof(AutoMapperConfig), typeof(AutoMapperProfile));

        }
    }
}