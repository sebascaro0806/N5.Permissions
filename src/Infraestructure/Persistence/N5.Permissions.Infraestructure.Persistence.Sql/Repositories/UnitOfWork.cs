using N5.Permissions.Domain.Interfaces.Infraestructure.Repositories;
using N5.Permissions.Infraestructure.Persistence.Sql.Context;

namespace N5.Permissions.Infraestructure.Persistence.Sql.Repositories;

/// <summary>
/// Unit of Work implementation for managing repositories and database context.
/// </summary>
/// <param name="_context">The database context.</param>
public class UnitOfWork(PermissionDbContext _context) : IUnitOfWork
{
    /// <summary>
    /// Dictionary to hold repositories for different entity types.
    /// </summary>
    private readonly Dictionary<Type, object> _repositories = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    public IRepository<T> Repository<T>() where T : class
    {
        if (!_repositories.TryGetValue(typeof(T), out var repo))
        {
            repo = new Repository<T>(_context);
            _repositories[typeof(T)] = repo;
        }

        return (IRepository<T>)repo;
    }

    /// <summary>
    /// Asynchronously saves changes to the database.
    /// </summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

    /// <summary>
    /// Disposes the Unit of Work and its resources.
    /// </summary>
    public void Dispose() => _context.Dispose();
}