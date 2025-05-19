using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using N5.Permissions.Application.Services;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Interfaces.Application.Services;
using N5.Permissions.Domain.Interfaces.Infraestructure.Messaging;

namespace N5.Permissions.Application;

/// <summary>
/// Dependency injection configuration for the application layer.
/// This class is responsible for registering services, validators, and mappers.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    /// <summary>
    /// Registers the application services, validators, and mappers.
    /// </summary>
    /// <param name="services">The service collection to register the services into.</param>
    /// <returns>The updated service collection.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the service collection is null.</exception>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR((config) => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient<IPermissionService, PermissionService>();

        return services;
    }
}
