using FluentValidation.TestHelper;
using N5.Permissions.Application.Configurations.Validators;
using N5.Permissions.Domain.Dtos.Modify;

namespace N5.Permissions.Services.Test.Application.Configurations.Validators;

/// <summary>
/// Test class for the <see cref="ModifyPermissionCommandValidator"/>.
/// This class is responsible for testing the validation of the <see cref="ModifyPermissionDto"/>.
/// </summary>
[TestFixture]
public class ModifyPermissionCommandValidatorTest
{
    private readonly ModifyPermissionCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModifyPermissionCommandValidatorTest"/> class.
    /// This constructor sets up the validator for the tests.
    /// </summary>
    public ModifyPermissionCommandValidatorTest()
    {
        _validator = new ModifyPermissionCommandValidator();
    }

    /// <summary>
    /// Tests the validation of the <see cref="ModifyPermissionDto"/> when the employee surname is empty.
    /// This test verifies that the validation fails when the employee surname is empty.
    /// </summary>
    [Test]
    public void Should_Have_Error_When_EmployeForename_Is_Empty()
    {
        // Arrange
        var model = new ModifyPermissionDto { EmployeForename = "", EmployeSurname = "" };

        // Act
        var result = _validator.Validate(model);

        // Assert
        Assert.That(result.IsValid, Is.False);
    }
}