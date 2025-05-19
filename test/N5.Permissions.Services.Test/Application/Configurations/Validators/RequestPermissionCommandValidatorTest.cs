using N5.Permissions.Application.Configurations.Validators;
using N5.Permissions.Domain.Dtos.Request;

namespace N5.Permissions.Services.Test.Application.Configurations.Validators;

/// <summary>
/// Test class for the <see cref="RequestPermissionCommandValidator"/>.
/// </summary>
[TestFixture]
public class RequestPermissionCommandValidatorTest
{
    private readonly RequestPermissionCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestPermissionCommandValidatorTest"/> class.
    /// This constructor sets up the validator for the tests.
    /// </summary>
    public RequestPermissionCommandValidatorTest()
    {
        _validator = new RequestPermissionCommandValidator();
    }

    /// <summary>
    /// Tests the validation of the <see cref="RequestPermissionCommandDto"/> when the employee surname is empty.
    /// This test verifies that the validation fails when the employee surname is empty.
    /// </summary>
    [Test]
    public void Should_Have_Error_When_EmployeForename_Is_Empty()
    {
        // Arrange
        var model = new RequestPermissionCommandDto { EmployeForename = "", EmployeSurname = "" };

        // Act
        var result = _validator.Validate(model);

        // Assert
        Assert.That(result.IsValid, Is.False);
    }
}
