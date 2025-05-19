
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using N5.Permissions.Domain.Entities;

namespace N5.Permissions.Infraestructure.Persistence.Sql.Context.Seed;

/// <summary>
/// Class for seeding the database with initial data.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ApplicationDbSeeder
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    public static async Task SeedAsync(PermissionDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        var vacation = new PermissionType { Description = "Vacation" };
        var dbSet = context.Set<PermissionType>();

        if (!await dbSet.AnyAsync())
        {
            dbSet.Add(vacation);
        }

        await context.SaveChangesAsync();
    }
}