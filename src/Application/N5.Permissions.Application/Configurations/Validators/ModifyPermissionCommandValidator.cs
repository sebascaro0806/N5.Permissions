using FluentValidation;
using N5.Permissions.Domain.Dtos.Modify;

namespace N5.Permissions.Application.Configurations.Validators;

/// <summary>
/// Validator for the <see cref="ModifyPermissionDto"/> class.
/// This class is responsible for validating the properties of the <see cref="ModifyPermissionDto"/>.
/// </summary>
public class ModifyPermissionCommandValidator : AbstractValidator<ModifyPermissionDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModifyPermissionCommandValidator"/> class.
    /// Validates the properties of the <see cref="ModifyPermissionDto"/> class.
    /// </summary>
    public ModifyPermissionCommandValidator()
    {
        RuleFor(x => x.EmployeForename)
            .NotEmpty()
            .WithMessage("Employee forename is required.");

        RuleFor(x => x.EmployeSurname)
            .NotEmpty()
            .WithMessage("Employee surname is required.");

        RuleFor(x => x.PermissionDate)
            .NotEmpty()
            .WithMessage("Permission date is required.");
    }
}