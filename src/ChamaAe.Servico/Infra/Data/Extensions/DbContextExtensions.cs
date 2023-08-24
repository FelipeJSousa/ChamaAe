using ChamaAe.Servico.Infra.Data.Context;
using ChamaAe.Servico.Infra.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChamaAe.Servico.Infra.Data.Extensions;

public static class DbContextExtensions
{
    public static void AddDbContextBase<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContextBase
    {
        services.AddDbContext<T>(options =>
        {
            options.UseMySQL(configuration.GetConnectionString("Connection"), x =>
            {
                x.MigrationsAssembly("SqlServerMigrations");
                x.ExecutionStrategy(e => new MyExecutionStrategy(e));
            }).AddInterceptors(new RemoveCrasisInterceptor());
        });
    }
}