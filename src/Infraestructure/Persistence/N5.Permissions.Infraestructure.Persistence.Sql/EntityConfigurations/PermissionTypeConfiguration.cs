using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5.Permissions.Domain.Entities;

namespace N5.Permissions.Infraestructure.Persistence.Sql.EntityConfigurations;

/// <summary>
/// Configuration for the Permission type entity.
/// </summary>
[ExcludeFromCodeCoverage]
internal class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
{
    /// <summary>
    /// Configures the Permission type entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<PermissionType> builder)
    {
        builder.ToTable("PermissionType", "dbo");

        builder.HasKey(pt => pt.Id);

        builder.Property(pt => pt.Id)
            .ValueGeneratedOnAdd();

        builder.Property(pt => pt.Description)
            .IsRequired()
            .HasMaxLength(250);
    }
}
