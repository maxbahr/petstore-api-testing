using System.Net;
using Polly;
using Polly.Retry;
using RestSharp;

namespace PetStoreApiTests.Polly;

public static class PollyPolicy<T>
{
    public static readonly AsyncRetryPolicy<RestResponse<T>> Retry404ForData =
        Policy
            .HandleResult<RestResponse<T>>(r =>
                r.StatusCode == HttpStatusCode.NotFound || r.Data == null)
            .WaitAndRetryAsync(
                5,
                attempt =>
                    TimeSpan.FromMilliseconds(200 * Math.Pow(2, attempt - 1)));

    public static readonly AsyncRetryPolicy<RestResponse> Retry404 =
        Policy
            .HandleResult<RestResponse>(r =>
                r.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(
                5,
                attempt =>
                    TimeSpan.FromMilliseconds(200 * Math.Pow(2, attempt - 1)));
}