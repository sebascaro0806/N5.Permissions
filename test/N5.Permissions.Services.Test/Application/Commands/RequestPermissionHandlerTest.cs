using Bogus;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using N5.Permissions.Application.Commands;
using N5.Permissions.Domain.Dtos.Request;
using N5.Permissions.Domain.Interfaces.Application.Services;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;
using N5.Permissions.Common.Test.FakeBuilder;
using N5.Permissions.Domain.Dtos.Common;

namespace N5.Permissions.Services.Test.Application.Commands;

/// <summary>
/// Test class for the RequestPermissionHandler.
/// This class contains unit tests for the RequestPermissionHandler.
/// </summary>
[TestFixture]
public class RequestPermissionHandlerTest
{
    private readonly Mock<IValidator<RequestPermissionCommandDto>> _validatorMock;
    private readonly Mock<IPermissionService> _serviceMock;
    private readonly RequestPermissionHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestPermissionHandlerTest"/> class.
    /// </summary>
    public RequestPermissionHandlerTest()
    {
        _validatorMock = new Mock<IValidator<RequestPermissionCommandDto>>();
        _serviceMock = new Mock<IPermissionService>();
        _handler = new RequestPermissionHandler(_validatorMock.Object, _serviceMock.Object);
    }

    /// <summary>
    /// Handles the modification of permissions.
    /// This method is called when a request to modify a permission is made.
    /// </summary>
    [Test]
    public async Task Handle_ShouldModifyPermission_WhenValidationPasses()
    {
        // Arrange
        var dto = new Faker<RequestPermissionCommandDto>().BaseRules().Generate();        

        _validatorMock
            .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _serviceMock
            .Setup(s => s.RequestPermission(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Faker<PermissionDto>().BaseRules().Generate());

        // Act
        var result = await _handler.Handle(dto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        _validatorMock.Verify(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()), Times.Once);
        _serviceMock.Verify(s => s.RequestPermission(dto, It.IsAny<CancellationToken>()), Times.Once);
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
        var dto = new Faker<RequestPermissionCommandDto>().BaseRules().Generate();

        var failures = new[] { new ValidationFailure("Field", "Error") };
        _validatorMock
            .Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        // Act & Assert
        Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(dto, CancellationToken.None));
        _validatorMock.Verify(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        _serviceMock.Verify(s => s.RequestPermission(It.IsAny<RequestPermissionCommandDto>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
