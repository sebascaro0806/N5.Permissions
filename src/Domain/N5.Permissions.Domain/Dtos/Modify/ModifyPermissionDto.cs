namespace N5.Permissions.Domain.Dtos.Modify;

public record ModifyPermissionDto
{
    /// <summary>
    /// Gets or sets the employee's first name.
    /// </summary>
    public required string EmployeForename { get; set; }

    /// <summary>
    /// Gets or sets the employee's surname.
    /// </summary>
    public required string EmployeSurname { get; set; }

    /// <summary>
    /// Gets or sets the permission date.
    /// </summary>
    public DateTime PermissionDate { get; set; }
}
