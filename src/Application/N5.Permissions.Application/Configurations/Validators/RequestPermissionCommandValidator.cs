using FluentValidation;
using N5.Permissions.Domain.Dtos.Request;

namespace N5.Permissions.Application.Configurations.Validators;

/// <summary>
/// Validator for the <see cref="RequestPermissionCommandDto"/> class.
/// This class is responsible for validating the properties of the <see cref="RequestPermissionCommandDto"/>.
/// </summary>
public class RequestPermissionCommandValidator : AbstractValidator<RequestPermissionCommandDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RequestPermissionCommandValidator"/> class.
    /// Validates the properties of the <see cref="RequestPermissionCommandDto"/> class.
    /// </summary>
    public RequestPermissionCommandValidator()
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

        RuleFor(x => x.PermissionType)
            .NotEmpty()
            .WithMessage("Permission type is required.")
            .Must(x => x > 0)
            .WithMessage("Permission type must be greater than 0.");
    }
}