using Bogus;
using PetStoreApiTests.Model;

namespace PetStoreApiTests.Data;

public static class TestData
{
    private static readonly Faker<UserDto> User = new Faker<UserDto>("en")
        .StrictMode(true)
        .RuleFor(o => o.Id, f => f.Random.Int(1, 1000000))
        .RuleFor(o => o.UserStatus, f => 0)
        .RuleFor(o => o.FirstName, f => f.Name.FirstName())
        .RuleFor(o => o.LastName, f => f.Name.LastName())
        .RuleFor(o => o.Email, f => f.Internet.Email().ToLower())
        .RuleFor(o => o.Phone, f => f.Phone.PhoneNumber())
        .RuleFor(o => o.Password, f => "Passw0rd")
        .RuleFor(o => o.Username, f => f.Random.Word().ToLower());

    public static UserDto GenerateUser()
    {
        return User.Generate();
    }

    public static IEnumerable<UserDto> GenerateUsers(int count)
    {
        return User.Generate(count);
    }
}