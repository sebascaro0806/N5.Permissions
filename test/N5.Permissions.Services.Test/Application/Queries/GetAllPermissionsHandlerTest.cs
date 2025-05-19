using Bogus;
using Moq;
using N5.Permissions.Application.Queries;
using N5.Permissions.Common.Test.FakeBuilder;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Dtos.GetAll;
using N5.Permissions.Domain.Interfaces.Application.Services;

namespace N5.Permissions.Services.Test.Application.Queries;

/// <summary>
/// Test class for the GetAllPermissionsHandler.
/// This class is responsible for testing the retrieval of all permissions.
/// </summary>
[TestFixture]
public class GetAllPermissionsHandlerTest
{
    private readonly Mock<IPermissionService> _permissionServiceMock;
    private readonly GetAllPermissionsHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllPermissionsHandlerTest"/> class.
    /// This constructor sets up the handler and the mock service for the tests.
    /// </summary>
    public GetAllPermissionsHandlerTest()
    {
        _permissionServiceMock = new Mock<IPermissionService>();
        _handler = new GetAllPermissionsHandler(_permissionServiceMock.Object);
    }

    /// <summary>
    /// Tests the retrieval of all permissions.
    /// This test verifies that the handler returns the expected permissions.
    /// </summary>
    [Test]
    public async Task Handle_Should_Return_All_Permissions()
    {
        // Arrange
        var expectedPermissions = new Faker<PermissionDto>()
            .BaseRules()
            .Generate(2);

        _permissionServiceMock
            .Setup(s => s.GetAllPermissionsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPermissions);

        var request = new GetAllQueryDto();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }
}
