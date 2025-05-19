using AutoMapper;
using Bogus;
using Moq;
using N5.Permissions.Application.Services;
using N5.Permissions.Common.Test.FakeBuilder;
using N5.Permissions.Domain;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Dtos.Modify;
using N5.Permissions.Domain.Dtos.Request;
using N5.Permissions.Domain.Entities;
using N5.Permissions.Domain.Interfaces.Infraestructure.Messaging;
using N5.Permissions.Domain.Interfaces.Infraestructure.Repositories;

namespace N5.Permissions.Services.Test.Application.Services;

[TestFixture]
public class PermissionServiceTest
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IEventProducer> _eventProducerMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IRepository<Permission>> _repositoryMock;
    private readonly PermissionService _service;

    public PermissionServiceTest()
    {
        _mapperMock = new Mock<IMapper>();
        _eventProducerMock = new Mock<IEventProducer>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repositoryMock = new Mock<IRepository<Permission>>();

        _unitOfWorkMock
            .Setup(u => u.Repository<Permission>())
            .Returns(_repositoryMock.Object);

        _service = new PermissionService(_mapperMock.Object, _eventProducerMock.Object, _unitOfWorkMock.Object);
    }

    [Test]
    public async Task ModifyPermissionAsync_WhenPermissionExists_ShouldModifyAndPublish()
    {
        // Arrange
        var existingPermission = new Faker<Permission>().BaseRules().Generate();
        var modifyDto = new Faker<ModifyPermissionDto>().BaseRules().Generate();

        var commandDto = new ModifyPermissionCommandDto
        {
            Id = 1,
            Data = modifyDto
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingPermission);

        _mapperMock
            .Setup(m => m.Map(modifyDto, existingPermission))
            .Returns(existingPermission);

        // Act
        var result = await _service.ModifyPermissionAsync(commandDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _repositoryMock.Verify(r => r.Update(existingPermission), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _eventProducerMock.Verify(e => e.PublishAsync(commandDto, Topics.ModifyPermission), Times.Once);
    }

    [Test]
    public void ModifyPermissionAsync_WhenPermissionNotFound_ShouldThrowException()
    {
        // Arrange
        var commandDto = new ModifyPermissionCommandDto
        {
            Id = 99,
            Data = new Faker<ModifyPermissionDto>().BaseRules().Generate()
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Permission)null!);

        // Act & Assert
        var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _service.ModifyPermissionAsync(commandDto, CancellationToken.None));

        Assert.That(ex.ParamName, Is.EqualTo("permission"));
    }

    [Test]
    public async Task GetAllPermissionsAsync_ShouldReturnMappedPermissions_AndPublishEvent()
    {
        // Arrange
        var permissions = new Faker<Permission>().BaseRules().Generate(2);
        var permissionDtos = new Faker<PermissionDto>().BaseRules().Generate(2);

        _repositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(permissions);

        _mapperMock
            .Setup(m => m.Map<IEnumerable<PermissionDto>>(permissions))
            .Returns(permissionDtos);

        // Act
        var result = await _service.GetAllPermissionsAsync(CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(permissionDtos));
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task RequestPermission_ShouldAddSaveAndReturnMappedPermission()
    {
        // Arrange
        var requestDto = new Faker<RequestPermissionCommandDto>().BaseRules().Generate();
        var permission = new Faker<Permission>().BaseRules().Generate();
        var permissionDto = new Faker<PermissionDto>().BaseRules().Generate();

        _mapperMock
            .Setup(m => m.Map<Permission>(requestDto))
            .Returns(permission);

        _repositoryMock
            .Setup(r => r.Add(permission))
            .Returns(permission);

        _mapperMock
            .Setup(m => m.Map<PermissionDto>(permission))
            .Returns(permissionDto);

        // Act
        var result = await _service.RequestPermission(requestDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(permissionDto));
        _repositoryMock.Verify(r => r.Add(permission), Times.AtLeastOnce);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        _eventProducerMock.Verify(e => e.PublishAsync(requestDto, Topics.RequestPermission), Times.AtLeastOnce);
    }
}
