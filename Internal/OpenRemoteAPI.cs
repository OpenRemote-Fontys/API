using Newtonsoft.Json;
using OpenRemoteAPI.Internal.Models;
using OpenRemoteAPI.Internal.Requests;
using Route = OpenRemoteAPI.Internal.Requests.Route;
using static OpenRemoteAPI.Internal.Requests.Route.Asset;
using HttpMethod = OpenRemoteAPI.Internal.Requests.HttpMethod;

namespace OpenRemoteAPI.Internal;

internal class OpenRemoteApi
{

    private readonly HttpClient _httpClient = new();
    private readonly Config _config;


    internal OpenRemoteApi()
    {
        _config = LoadConfig();
    }

    internal static Config LoadConfig()
    {
        using StreamReader r = new StreamReader("./Properties/config.json");
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<Config>(json) ?? throw new InvalidOperationException("Missing config");
    }

    public void QueryAssets(AssetQuery query)
    {
        string url = postAssetQuery.ToUrl();

        string json = JsonConvert.SerializeObject(query);
        // new HttpContent

        MakeHttpCall(postAssetQuery.ToUrl(), HttpMethod.PUT);
    }

    public Task<HttpResponseMessage> MakeHttpCall(string routeUrl, HttpMethod httpMethod)
    {
        return MakeHttpCall(routeUrl, httpMethod, null);
    }

    internal Task<HttpResponseMessage> MakeHttpCall(string routeUrl, HttpMethod httpMethod, HttpContent httpContent)
    {
        string url = _config.BaseUrl + routeUrl;

        Task<HttpResponseMessage> httpCallResponse = httpMethod switch
        {
            HttpMethod.DELETE => _httpClient.DeleteAsync(url),
            HttpMethod.GET => _httpClient.GetAsync(url),
            HttpMethod.POST => _httpClient.PostAsync(url, httpContent),
            HttpMethod.PUT => _httpClient.PutAsync(url, httpContent),
            HttpMethod.PATCH => _httpClient.PatchAsync(url, httpContent),
        };

        return httpCallResponse;
    }
}