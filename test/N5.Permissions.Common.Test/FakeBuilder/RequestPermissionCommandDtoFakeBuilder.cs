using Bogus;
using N5.Permissions.Domain.Dtos.Request;

namespace N5.Permissions.Common.Test.FakeBuilder;

/// <summary>
/// Fake builder for the RequestPermissionCommandDto.
/// This class is used to create fake instances of the RequestPermissionCommandDto for testing purposes.
/// </summary>
public static class RequestPermissionCommandDtoFakeBuilder
{
    /// <summary>
    /// Creates a new instance of the <see cref="Faker{RequestPermissionCommandDto}"/> class.
    /// This class is used to generate fake data for the <see cref="RequestPermissionCommandDto"/> entity.
    /// </summary>
    /// <returns>A new instance of the <see cref="Faker{RequestPermissionCommandDto}"/> class.</returns>
    public static Faker<RequestPermissionCommandDto> BaseRules(this Faker<RequestPermissionCommandDto> faker)
    {
        return faker
            .RuleFor(x => x.EmployeForename, f => f.Name.FirstName())
            .RuleFor(x => x.EmployeSurname, f => f.Name.LastName())
            .RuleFor(x => x.PermissionDate, f => f.Date.Past(1));
    }
}
