using N5.Permissions.Infraestructure.Persistence.Sql.Context;
using N5.Permissions.Infraestructure.Persistence.Sql.Context.Seed;

namespace N5.Permissions.Api.HostedServices;

/// <summary>
/// A hosted service that seeds the database with initial data.
/// </summary>
internal class SeedHostedService : IHostedService
{
    /// <summary>
    /// The service provider for resolving dependencies.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="SeedHostedService"/> class.
    /// </summary>
    public SeedHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Starts the hosted service and seeds the database.
    /// </summary>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PermissionDbContext>();

        await ApplicationDbSeeder.SeedAsync(dbContext);
    }

    /// <summary>
    /// Stops the hosted service.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}