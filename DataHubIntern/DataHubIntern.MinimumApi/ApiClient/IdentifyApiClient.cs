using DataHubIntern.MinimumApi.ApiClient.Models;
using DataHubIntern.Shared.File;

namespace DataHubIntern.MinimumApi.ApiClient;

public interface IIdentifyApiClient
{
    public Task<IdentifyResponse> IdentifyAsync(RawData request, CancellationToken cancellationToken = default);
}

public class IdentifyApiClient : IIdentifyApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    public const string ClientName = "IdentifyApi";

    public IdentifyApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IdentifyResponse> IdentifyAsync(RawData request, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(ClientName);
        
        return new IdentifyResponse("");
    }
}