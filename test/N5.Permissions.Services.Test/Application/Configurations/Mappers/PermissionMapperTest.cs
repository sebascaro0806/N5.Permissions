using AutoMapper;
using N5.Permissions.Application.Configurations.Mappers;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Dtos.Modify;
using N5.Permissions.Domain.Dtos.Request;
using N5.Permissions.Domain.Entities;

namespace N5.Permissions.Services.Test.Application.Configurations.Mappers;

/// <summary>
/// Test class for the <see cref="PermissionMapper"/> class.
/// This class is responsible for testing the mapping between the <see cref="Permission"/> entity and the <see cref="ModifyPermissionCommandDto"/> data transfer object.
/// </summary>
[TestFixture]
public class PermissionMapperTest
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionMapperTest"/> class.
    /// This constructor sets up the AutoMapper configuration for the tests.
    /// </summary>
    public PermissionMapperTest()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PermissionMapper>();
        });

        _mapper = config.CreateMapper();
    }

    /// <summary>
    /// Tests the mapping of <see cref="Permission"/> to <see cref="ModifyPermissionDto"/>.
    /// This test verifies that the mapping is successful and the result is not null.
    /// </summary>
    [Test]
    public void Should_Map_Permission_To_ModifyPermissionDto()
    {
        // Arrange
        var permission = new Permission
        {
            Id = 1,
            PermissionTypeId = 99,
            EmployeForename = null!,
            EmployeSurname = null!
        };

        // Act
        var result = _mapper.Map<ModifyPermissionDto>(permission);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests the mapping of <see cref="ModifyPermissionDto"/> to <see cref="Permission"/>.
    /// This test verifies that the mapping is successful and the result is not null.
    /// </summary>
    [Test]
    public void Should_Map_ModifyPermissionDto_To_Permission_Ignoring_Id_And_PermissionTypeId()
    {
        // Arrange
        var dto = new ModifyPermissionDto
        {
            EmployeForename = null!,
            EmployeSurname = null!,
        };

        // Act
        var result = _mapper.Map<Permission>(dto);

        // Assert
        Assert.That(result.PermissionTypeId, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests the mapping of <see cref="Permission"/> to <see cref="PermissionDto"/>.
    /// This test verifies that the mapping is successful and the result is not null.
    /// </summary>
    [Test]
    public void Should_Map_Permission_To_PermissionDto_With_Mapped_PermissionType()
    {
        // Arrange
        var permission = new Permission
        {
            PermissionTypeId = 5,
            EmployeForename = null!,
            EmployeSurname = null!
        };

        // Act
        var result = _mapper.Map<PermissionDto>(permission);

        // Assert
        Assert.That(permission.PermissionTypeId, Is.EqualTo(result.PermissionType));
    }

    /// <summary>
    /// Tests the mapping of <see cref="RequestPermissionCommandDto"/> to <see cref="Permission"/>.
    /// This test verifies that the mapping is successful and the result is not null.
    /// </summary>
    [Test]
    public void Should_Map_RequestPermissionCommandDto_To_Permission_With_PermissionTypeId()
    {
        // Arrange
        var request = new RequestPermissionCommandDto
        {
            PermissionType = 10,
            EmployeForename = null!,
            EmployeSurname = null!,
        };

        // Act
        var result = _mapper.Map<Permission>(request);

        // Assert
        Assert.That(request.PermissionType, Is.EqualTo(result.PermissionTypeId));
    }
}
