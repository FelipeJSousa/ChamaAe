using ChamaAe.Servico.Application.Services;
using ChamaAe.Servico.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChamaAe.Servico.Infra.IoC;

public class DependencyInjectorApplication
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IChamadoService, ChamadoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IUsuarioTipoService, UsuarioTipoService>();
        services.AddScoped<ILoginService, LoginService>();
    }
}