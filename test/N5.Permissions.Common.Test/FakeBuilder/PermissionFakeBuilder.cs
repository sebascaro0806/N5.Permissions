using Bogus;
using N5.Permissions.Domain.Entities;

namespace N5.Permissions.Common.Test.FakeBuilder;

/// <summary>
/// This class is used to generate fake data for the <see cref="Permission"/> entity.
/// It uses the Bogus library to create realistic fake data.
/// </summary>
public static class PermissionFakeBuilder
{
    /// <summary>
    /// Creates a new instance of the <see cref="Faker{Permission}"/> class.
    /// This class is used to generate fake data for the <see cref="Permission"/> entity.
    /// </summary>
    /// <returns>A new instance of the <see cref="Faker{Permission}"/> class.</returns>
    public static Faker<Permission> BaseRules(this Faker<Permission> faker)
    {
        return faker
            .RuleFor(x => x.Id, f => f.IndexFaker)
            .RuleFor(x => x.PermissionTypeId, f => f.Random.Int(1, 10))
            .RuleFor(x => x.EmployeForename, f => f.Name.FirstName())
            .RuleFor(x => x.EmployeSurname, f => f.Name.LastName())
            .RuleFor(x => x.PermissionDate, f => f.Date.Past(1));
    }
}
