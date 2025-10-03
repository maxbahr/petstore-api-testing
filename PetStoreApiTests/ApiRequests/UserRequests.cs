using PetStoreApiTests.Model;
using RestSharp;

namespace PetStoreApiTests.ApiRequests;

public class UserRequests(RestClient restClient)
{
    private const string EndpointUrl = "/user";

    public async Task<RestResponse<ResponseMsgDto>> CreateUser<T>(T user) where T : class
    {
        var request = new RestRequest(EndpointUrl, Method.Post)
            .AddJsonBody(user);

        return await restClient.ExecuteAsync<ResponseMsgDto>(request);
    }

    public async Task<RestResponse<ResponseMsgDto>> CreateUsers<T>(T users) where T : class
    {
        var request = new RestRequest($"{EndpointUrl}/createWithArray", Method.Post)
            .AddJsonBody(users);

        return await restClient.ExecuteAsync<ResponseMsgDto>(request);
    }

    public async Task<RestResponse<ResponseMsgDto>> UpdateUser(string username, UserDto user)
    {
        var request = new RestRequest($"{EndpointUrl}/{username}", Method.Put)
            .AddJsonBody(user);

        return await restClient.ExecuteAsync<ResponseMsgDto>(request);
    }

    public async Task<RestResponse> DeleteUser(string username)
    {
        var request = new RestRequest($"{EndpointUrl}/{username}", Method.Delete);

        return await restClient.ExecuteAsync(request);
    }

    public async Task<RestResponse<T>> GetUser<T>(string username)
    {
        var request = new RestRequest($"{EndpointUrl}/{username}");

        return await restClient.ExecuteAsync<T>(request);
    }
}