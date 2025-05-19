using Bogus;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using N5.Permissions.Application.Commands;
using N5.Permissions.Domain.Dtos.Modify;
using N5.Permissions.Domain.Interfaces.Application.Services;
using N5.Permissions.Common.Test.FakeBuilder;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace N5.Permissions.Services.Test.Application.Commands;

/// <summary>
/// Test class for the ModifyPermissionHandler.
/// This class contains unit tests for the ModifyPermissionHandler.
/// </summary>
[TestFixture]
public class ModifyPermissionHandlerTest
{
    private readonly Mock<IValidator<ModifyPermissionDto>> _validatorMock;
    private readonly Mock<IPermissionService> _serviceMock;
    private readonly ModifyPermissionHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModifyPermissionHandlerTest"/> class.
    /// </summary>
    public ModifyPermissionHandlerTest()
    {
        _validatorMock = new Mock<IValidator<ModifyPermissionDto>>();
        _serviceMock = new Mock<IPermissionService>();
        _handler = new ModifyPermissionHandler(_validatorMock.Object, _serviceMock.Object);
    }

    /// <summary>
    /// Handles the modification of permissions.
    /// This method is called when a request to modify a permission is made.
    /// </summary>
    [Test]
    public async Task Handle_ShouldModifyPermission_WhenValidationPasses()
    {
        // Arrange
        var dto = new Faker<ModifyPermissionDto>().BaseRules().Generate();
        var command = new ModifyPermissionCommandDto { Data = dto };

        _validatorMock
            .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _serviceMock
            .Setup(s => s.ModifyPermissionAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _validatorMock.Verify(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()), Times.Once);
        _serviceMock.Verify(s => s.ModifyPermissionAsync(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Handles the modification of permissions.
    /// Validation fails and throws a ValidationException.
    /// </summary>
    [Test]
    public void Handle_ShouldThrowValidationException_WhenValidationFails()
    {
        // Arrange
        _serviceMock.Reset();
        
        var dto = new Faker<ModifyPermissionDto>().BaseRules().Generate();
        var command = new ModifyPermissionCommandDto { Data = dto };

        var failures = new[] { new ValidationFailure("Field", "Error") };
        _validatorMock
            .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        // Act & Assert
        Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        _validatorMock.Verify(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        _serviceMock.Verify(s => s.ModifyPermissionAsync(It.IsAny<ModifyPermissionCommandDto>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}