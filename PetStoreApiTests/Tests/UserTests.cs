using System.Net;
using FluentAssertions;
using PetStoreApiTests.Data;
using PetStoreApiTests.Model;
using PetStoreApiTests.Polly;

namespace PetStoreApiTests.Tests;

public class UserTests : BaseTest
{
// Automation task:
// 3. Please automate the below REST Api’s:
// ● From the https://petstore.swagger.io/ , use the PET endpoints and automate any 4
// request types (POST, PUT, GET, DELETE).
// ● Verify with assertions at least 3 available status codes (2xx, 4xx, etc.).
// ● Verify with assertions that the record created with the POST matches with the response
//     of the GET    


    [Fact]
    public async Task CreateUserTest()
    {
        var user = TestData.GenerateUser();
        var response = await UserRequests.CreateUser(user);
        response.StatusCode.Should().Be(HttpStatusCode.OK, "creating a new user");
        response.Content.Should().NotBeEmpty();
        response.Data.Should().NotBeNull();
        response.Data.Code.Should().Be(200);
        response.Data.Type.Should().Be("unknown");
        response.Data.Message.Should().Be(user.Id.ToString());
    }

    [Fact]
    public async Task CreateUserWithoutDataTest()
    {
        var user = new { };
        var response = await UserRequests.CreateUser(user);
        response.StatusCode.Should().Be(HttpStatusCode.OK, "creating a new user");
        response.Content.Should().NotBeEmpty();
        response.Data.Should().NotBeNull();
        response.Data.Code.Should().Be(200);
        response.Data.Type.Should().Be("unknown");
        response.Data.Message.Should().Be("0");
    }

    [Fact]
    public async Task CreateUsersTest()
    {
        const int count = 3;
        var users = TestData.GenerateUsers(count).ToList();
        var respPost = await UserRequests.CreateUsers(users);
        respPost.StatusCode.Should().Be(HttpStatusCode.OK, "creating a list of user");
        respPost.Content.Should().NotBeEmpty();
        respPost.Data.Should().NotBeNull();
        respPost.Data.Code.Should().Be(200);
        respPost.Data.Type.Should().Be("unknown");
        respPost.Data.Message.Should().Be("ok");

        for (var i = 0; i < count; i++)
        {
            var response =
                await PollyPolicy<UserDto>.Retry404ForData.ExecuteAsync(() =>
                    UserRequests.GetUser<UserDto>(users[i].Username));

            response.StatusCode.Should().Be(HttpStatusCode.OK, "getting the user");
            response.Data.Should().NotBeNull();
            response.Data.Id.Should().Be(users[i].Id);
        }
    }

    [Fact]
    public async Task GetUserTest()
    {
        var user = TestData.GenerateUser();
        var resPost = await UserRequests.CreateUser(user);
        resPost.StatusCode.Should().Be(HttpStatusCode.OK, "creating a new user");

        var response =
            await PollyPolicy<UserDto>.Retry404ForData.ExecuteAsync(() => UserRequests.GetUser<UserDto>(user.Username));

        response.StatusCode.Should().Be(HttpStatusCode.OK, "getting the user");
        response.Data.Should().NotBeNull();
        response.Data.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task GetUser404Test()
    {
        var response = await UserRequests.GetUser<ResponseMsgDto>("NotUsername");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Data.Should().NotBeNull();
        response.Data.Should().BeEquivalentTo(new { Code = 1, Type = "error", Message = "User not found" });
    }

    [Fact]
    public async Task UpdateUserTest()
    {
        var user = TestData.GenerateUser();
        var resPost = await UserRequests.CreateUser(user);
        resPost.StatusCode.Should().Be(HttpStatusCode.OK, "creating a new user");

        await PollyPolicy<UserDto>.Retry404ForData.ExecuteAsync(() => UserRequests.GetUser<UserDto>(user.Username));

        var newUser = TestData.GenerateUser();
        var response = await UserRequests.UpdateUser(user.Username, newUser);
        response.StatusCode.Should().Be(HttpStatusCode.OK, "updating a new user");

        var resGet =
            await PollyPolicy<UserDto>.Retry404ForData.ExecuteAsync(() =>
                UserRequests.GetUser<UserDto>(newUser.Username));
        resGet.StatusCode.Should().Be(HttpStatusCode.OK, "getting the updated user");
        resGet.Data.Should().NotBeNull();
        resGet.Data.Should().BeEquivalentTo(newUser);
    }

    [Fact]
    public async Task DeleteUserTest()
    {
        var user = TestData.GenerateUser();
        var resPost = await UserRequests.CreateUser(user);
        resPost.StatusCode.Should().Be(HttpStatusCode.OK, "creating a new user");

        var r = await PollyPolicy<UserDto>.Retry404ForData.ExecuteAsync(() =>
            UserRequests.GetUser<UserDto>(user.Username));
        r.Data!.Username.Should().Be(user.Username, "getting the new user");

        var response = await PollyPolicy<UserDto>.Retry404.ExecuteAsync(() => UserRequests.DeleteUser(user.Username));
        response.StatusCode.Should().Be(HttpStatusCode.OK, "deleting the user");
    }

    [Fact]
    public async Task DeleteUser404Test()
    {
        var response = await UserRequests.DeleteUser("notUsername");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, "deleting the user");
    }

    [Fact]
    public async Task DeleteUser405Test()
    {
        var response = await UserRequests.DeleteUser(null!);
        response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed, "deleting the user");
    }
}