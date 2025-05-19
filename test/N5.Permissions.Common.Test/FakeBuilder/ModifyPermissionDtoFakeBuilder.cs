using Bogus;
using N5.Permissions.Domain.Dtos.Modify;

namespace N5.Permissions.Common.Test.FakeBuilder;

/// <summary>
/// Fake builder for the ModifyPermissionDto.
/// This class is used to create fake instances of the ModifyPermissionDto for testing purposes.
/// </summary>
public static class ModifyPermissionDtoFakeBuilder
{
    /// <summary>
    /// Creates a new instance of the <see cref="Faker{ModifyPermissionDto}"/> class.
    /// This class is used to generate fake data for the <see cref="ModifyPermissionDto"/> entity.
    /// </summary>
    /// <returns>A new instance of the <see cref="Faker{ModifyPermissionDto}"/> class.</returns>
    public static Faker<ModifyPermissionDto> BaseRules(this Faker<ModifyPermissionDto> faker)
    {
        return faker
            .RuleFor(x => x.EmployeForename, f => f.Name.FirstName())
            .RuleFor(x => x.EmployeSurname, f => f.Name.LastName())
            .RuleFor(x => x.PermissionDate, f => f.Date.Past(1));
    }
}
