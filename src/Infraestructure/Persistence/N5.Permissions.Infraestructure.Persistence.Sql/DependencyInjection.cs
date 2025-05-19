using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5.Permissions.Domain.Interfaces.Infraestructure.Repositories;
using N5.Permissions.Infraestructure.Persistence.Sql.Context;
using N5.Permissions.Infraestructure.Persistence.Sql.Repositories;

namespace N5.Permissions.Infraestructure.Persistence.Sql;

/// <summary>
/// Dependency injection extensions for SQL persistence.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds SQL persistence services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The configuration to use for setting up the services.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddSqlPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PermissionDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
