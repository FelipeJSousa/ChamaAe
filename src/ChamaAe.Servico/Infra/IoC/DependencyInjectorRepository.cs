using ChamaAe.Servico.Domain.Interfaces;
using ChamaAe.Servico.Infra.Data.Context;
using ChamaAe.Servico.Infra.Data.Extensions;
using ChamaAe.Servico.Infra.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChamaAe.Servico.Infra.IoC;

public static class DependencyInjectorRepository
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextBase<DbContextBase>(configuration);
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
    }
}