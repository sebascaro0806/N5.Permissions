using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5.Permissions.Domain.Entities;

namespace N5.Permissions.Infraestructure.Persistence.Sql.EntityConfigurations;

/// <summary>
/// Configuration for the Permission entity.
/// </summary>
[ExcludeFromCodeCoverage]
internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    /// <summary>
    /// Configures the Permission entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permission", "dbo");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.EmployeForename)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.EmployeSurname)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.PermissionDate)
            .IsRequired();

        builder.Property(p => p.PermissionTypeId)
            .HasColumnName("PermissionType")
            .IsRequired();

        builder.HasOne<PermissionType>()
            .WithMany()
            .HasForeignKey(p => p.PermissionTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}