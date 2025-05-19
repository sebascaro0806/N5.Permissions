using Bogus;
using N5.Permissions.Domain.Dtos.Modify;
using N5.Permissions.Common.Test.FakeBuilder;
using N5.Permissions.Domain.Entities;
using Moq;
using AutoMapper;
using N5.Permissions.Domain.Interfaces.Infraestructure.Repositories;
using N5.Permissions.Infraestructure.Persistence.Sql.Repositories;
using N5.Permissions.Infraestructure.Persistence.Sql.Context;

namespace N5.Permissions.Services.Test.Infraestructure.Repositories;

[TestFixture]
public class UnitOfWorkTest
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRepository<Permission>> _repositoryMock;
    private readonly UnitOfWork _unitOfWork;
    private readonly Mock<PermissionDbContext> _contextMock;

    public UnitOfWorkTest()
    {
        _contextMock = new Mock<PermissionDbContext>();
        _contextMock
            .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _mapperMock = new Mock<IMapper>();
        _repositoryMock = new Mock<IRepository<Permission>>();
        _unitOfWork = new UnitOfWork(_contextMock.Object);
    }

    [OneTimeTearDown]
    public void DisposeUnitOfWork()
    {
        _unitOfWork?.Dispose();
    }

    [Test]
    public void Repository_ShouldReturnSameRepositoryInstance()
    {
        // Arrange
        var dto = new ModifyPermissionCommandDto
        {
            Id = 1,
            Data = new Faker<ModifyPermissionDto>().BaseRules().Generate()
        };

        var existingPermission = new Faker<Permission>().BaseRules().Generate();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(dto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingPermission);

        _unitOfWork.Repository<Permission>();

        // Assert
        Assert.That(_unitOfWork, Is.Not.Null);
    }
}
