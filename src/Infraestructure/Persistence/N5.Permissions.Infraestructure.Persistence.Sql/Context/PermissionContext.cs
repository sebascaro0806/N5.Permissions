using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using N5.Permissions.Infraestructure.Persistence.Sql.EntityConfigurations;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace N5.Permissions.Infraestructure.Persistence.Sql.Context;

/// <summary>
/// Represents the database context for permissions.
/// </summary>
[ExcludeFromCodeCoverage]
public class PermissionDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionDbContext"/> class.
    /// </summary>
    public PermissionDbContext() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the context.</param>
    public PermissionDbContext(DbContextOptions<PermissionDbContext> options) : base(options) { }

    /// <summary>
    /// Applies the configuration for entities in the context.
    /// </summary>
    /// <param name="optionsBuilder">The options builder used to configure the context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionTypeConfiguration());
    }
}
