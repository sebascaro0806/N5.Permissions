namespace N5.Permissions.Domain.Interfaces.Infraestructure.Repositories;

/// <summary>
/// Generic repository interface for managing entities.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets a repository for the specified entity type.
    /// </summary>
    IRepository<T> Repository<T>() where T : class;

    /// <summary>
    /// Asynchronously saves changes to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}