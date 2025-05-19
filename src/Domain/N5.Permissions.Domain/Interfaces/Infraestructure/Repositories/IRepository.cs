namespace N5.Permissions.Domain.Interfaces.Infraestructure.Repositories;

/// <summary>
/// Generic repository interface for managing entities.
/// This interface provides methods for retrieving and updating entities.
/// </summary>
/// <typeparam name="T">The type of the entity managed by the repository.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously gets an entity by its identifier.
    /// </summary>
    /// <param name="entity">The entity to get.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The added entity.</returns>
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves all entities from the repository.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to get.</param>
    /// <returns>The added entity.</returns>
    T Add(T entity);

    /// <summary>
    /// Asynchronously updates an entity in the repository.
    /// </summary>
    void Update(T entity);
}