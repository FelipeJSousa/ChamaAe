using ChamaAe.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChamaAe.Servico.Infra.Data.Context;

public class DbContextBase : DbContext
{
    public DbContextBase(DbContextOptions options) : base(options)
    {
    }
        
    protected static readonly ILoggerFactory Logger = LoggerFactory.Create(builder =>
    {
        builder.AddFilter
            (
                (category, level) => category == DbLoggerCategory.Database.Command.Name &&
                                     (level is LogLevel.Information or LogLevel.Error)
            )
            .AddConsole();
    });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextBase).Assembly);
        modelBuilder.Ignore<EntityBase>();
            
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(Logger)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
}