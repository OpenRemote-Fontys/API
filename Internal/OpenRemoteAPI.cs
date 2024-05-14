using System.Net.Http.Headers;
using Newtonsoft.Json;
using OpenRemoteAPI.Internal.Models;
using OpenRemoteAPI.Internal.Requests;
using static OpenRemoteAPI.Internal.Requests.Route.Asset;
using HttpMethod = OpenRemoteAPI.Internal.Requests.HttpMethod;

namespace OpenRemoteAPI.Internal;

internal class OpenRemoteApi
{

    private readonly HttpClient _httpClient = new();
    private readonly Config _config;

    private readonly JsonSerializerSettings settings = new()
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    internal OpenRemoteApi()
    {
        _config = LoadConfig();
        _httpClient.BaseAddress = new Uri(_config.BaseUrl);
    }

    internal static Config LoadConfig()
    {
        using StreamReader r = new StreamReader("./Properties/config.json");
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<Config>(json) ?? throw new InvalidOperationException("Missing config");
    }

    public async Task<List<Asset>> QueryAssets(AssetQuery query)
    {
        string json = JsonConvert.SerializeObject(query, settings);
        HttpContent httpContent = new StringContent(json, MediaTypeHeaderValue.Parse("application/json"));


        HttpResponseMessage response = await MakeHttpCall(postAssetQuery.ToUrl(), postAssetQuery.HttpMethod, httpContent);

        string readAsStringAsync = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Return JSON: " + readAsStringAsync);
        List<Asset> assets = JsonConvert.DeserializeObject<List<Asset>>(readAsStringAsync) ?? [];
        return assets;
    }

    public async Task<Asset?> QueryAsset(string assetId)
    {
        HttpResponseMessage response = await MakeHttpCall(getAsset.ToUrl(assetId), getAsset.HttpMethod);

        string readAsStringAsync = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Return JSON: " + readAsStringAsync);
        Asset? asset = JsonConvert.DeserializeObject<Asset>(readAsStringAsync);
        return asset;
    }

    public async Task<HttpResponseMessage> MakeHttpCall(string routeUrl, HttpMethod httpMethod)
    {
        return await MakeHttpCall(routeUrl, httpMethod, null);
    }

    internal async Task<HttpResponseMessage> MakeHttpCall(string routeUrl, HttpMethod httpMethod, HttpContent httpContent)
    {
        Console.WriteLine("Calling `" + routeUrl + "` with " + httpMethod);

        string url = _config.BaseUrl + routeUrl;

        Task<HttpResponseMessage> httpCallResponse = httpMethod switch
        {
            HttpMethod.DELETE => _httpClient.DeleteAsync(url),
            HttpMethod.GET => _httpClient.GetAsync(url),
            HttpMethod.POST => _httpClient.PostAsync(url, httpContent),
            HttpMethod.PUT => _httpClient.PutAsync(url, httpContent),
            HttpMethod.PATCH => _httpClient.PatchAsync(url, httpContent),
            _ => throw new ArgumentOutOfRangeException(nameof(httpMethod), httpMethod, null)
        };

        return await httpCallResponse;
    }
}