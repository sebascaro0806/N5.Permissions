namespace N5.Permissions.Domain.Entities;

/// <summary>
/// Represents the type of permission.
/// </summary>
public class Permission
{
    /// <summary>
    /// Gets or sets the unique identifier for the permission.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the employee's first name.
    /// </summary>
    public required string EmployeForename { get; set; }

    /// <summary>
    /// Gets or sets the employee's surname.
    /// </summary>
    public required string EmployeSurname { get; set; }

    /// <summary>
    /// Gets or sets the employee permission type.
    /// </summary>
    public int PermissionTypeId { get; set; }

    /// <summary>
    /// Gets or sets the permission date.
    /// </summary>
    public DateTime PermissionDate { get; set; }
}
