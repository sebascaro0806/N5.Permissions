using Bogus;
using N5.Permissions.Domain.Dtos.Common;

namespace N5.Permissions.Common.Test.FakeBuilder;

/// <summary>
/// Fake builder for the PermissionDto.
/// This class is used to create fake instances of the PermissionDto for testing purposes.
/// </summary>
public static class PermissionDtoFakeBuilder
{
    /// <summary>
    /// Creates a new instance of the <see cref="Faker{PermissionDto}"/> class.
    /// This class is used to generate fake data for the <see cref="PermissionDto"/> entity.
    /// </summary>
    /// <returns>A new instance of the <see cref="Faker{PermissionDto}"/> class.</returns>
    public static Faker<PermissionDto> BaseRules(this Faker<PermissionDto> faker)
    {
        return faker
            .RuleFor(x => x.Id, f => f.IndexFaker)
            .RuleFor(x => x.PermissionType, f => f.Random.Int(1, 10))
            .RuleFor(x => x.EmployeForename, f => f.Name.FirstName())
            .RuleFor(x => x.EmployeSurname, f => f.Name.LastName())
            .RuleFor(x => x.PermissionDate, f => f.Date.Past(1));
    }
}
