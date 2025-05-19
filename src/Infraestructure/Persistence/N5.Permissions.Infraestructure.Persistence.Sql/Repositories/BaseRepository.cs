using Microsoft.EntityFrameworkCore;
using N5.Permissions.Domain.Interfaces.Infraestructure.Repositories;
using N5.Permissions.Infraestructure.Persistence.Sql.Context;

namespace N5.Permissions.Infraestructure.Persistence.Sql.Repositories;

/// <summary>
/// Base repository class for managing entities.
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    /// <summary>
    /// The database set for the entity type.
    /// </summary>
    protected readonly DbSet<T> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{T}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public Repository(PermissionDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// Adds a new entity to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The added entity.</returns>
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) => await _dbSet.ToListAsync(cancellationToken);

    /// <summary>
    /// Asynchronously adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The entity with the specified identifier, or null if not found.</returns>
    public T Add(T entity) => _dbSet.Add(entity).Entity;

    /// <summary>
    /// Asynchronously retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity with the specified identifier, or null if not found.</returns>
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken) => await _dbSet.FindAsync(id, cancellationToken);

    /// <summary>
    /// Asynchronously adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public void Update(T entity) => _dbSet.Update(entity);
}