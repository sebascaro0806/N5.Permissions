using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using N5.Permissions.Domain.Entities;
using N5.Permissions.Domain.Interfaces.Infraestructure.Repositories;
using N5.Permissions.Infraestructure.Persistence.Sql.Context;
using N5.Permissions.Infraestructure.Persistence.Sql.Repositories;
using N5.Permissions.Common.Test.FakeBuilder;
using Moq.EntityFrameworkCore;

namespace N5.Permissions.Services.Test.Infraestructure.Repositories;

/// <summary>
/// Test class for the base repository.
/// </summary>
[TestFixture]
internal class BaseRepositoryTest
{
    private readonly Mock<DbSet<Permission>> _mockSet;
    private readonly Mock<PermissionDbContext> _mockContext;
    private readonly IRepository<Permission> _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepositoryTest"/> class.
    /// </summary>
    public BaseRepositoryTest()
    {
        _mockSet = new Mock<DbSet<Permission>>();

        _mockContext = new Mock<PermissionDbContext>(new DbContextOptions<PermissionDbContext>());
        _mockContext.Setup(m => m.Set<Permission>()).Returns(_mockSet.Object);

        _repository = new Repository<Permission>(_mockContext.Object);
    }

    /// <summary>
    /// Test for the Add method of the repository.
    /// </summary>
    [Test]
    public async Task GetAllAsync_ReturnsEntities()
    {
        // Arrange
        var data = new Faker<Permission>().BaseRules().Generate(3);

        _mockContext.Setup(m => m.Set<Permission>()).ReturnsDbSet(data);

        var repo = new Repository<Permission>(_mockContext.Object);

        // Act
        var result = await repo.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.That(data.Count, Is.EqualTo(result.Count()));
    }

    /// <summary>
    /// Test for the get by id method of the repository.
    /// </summary>
    [Test]
    public async Task GetByIdAsync_ReturnsEntity()
    {
        // Arrange
        var entity = new Faker<Permission>().BaseRules().Generate();

        _mockSet.Setup(m => m.FindAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

        // Act
        var result = await _repository.GetByIdAsync(It.IsAny<int>(), CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Test for the Update method of the repository.
    /// </summary>
    [Test]
    public void Update_ReturnsEntity()
    {
        // Arrange
        var data = new Faker<Permission>().BaseRules().Generate(3);

        _mockContext.Setup(m => m.Set<Permission>()).ReturnsDbSet(data);

        var repo = new Repository<Permission>(_mockContext.Object);

        // Act
        repo.Update(data.First());

        // Assert
        Assert.That(data.First(), Is.Not.Null);
    }
}
