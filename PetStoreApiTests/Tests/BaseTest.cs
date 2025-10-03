using PetStoreApiTests.ApiRequests;
using RestSharp;

namespace PetStoreApiTests.Tests;

public abstract class BaseTest : IAsyncLifetime
{
    private const string HttpsPetStoreSwaggerIoV2 = "https://petstore.swagger.io/v2";

    private RestClient? Client { get; set; }
    public required UserRequests UserRequests { get; set; }

    public Task InitializeAsync()
    {
        Client = new RestClient(new RestClientOptions
        {
            BaseUrl = new Uri(HttpsPetStoreSwaggerIoV2)
        });
        Client.AddDefaultHeader("Accept", "application/json");
        Client.AddDefaultHeader("Content-Type", "application/json");

        UserRequests = new UserRequests(Client!);

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        Client!.Dispose();
        return Task.CompletedTask;
    }
}