using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using OpenRemoteAPI.Internal.Models;
using OpenRemoteAPI.Internal.Requests;
using OpenRemoteAPI.Models;
using static OpenRemoteAPI.Internal.Requests.Route.Asset;
using static OpenRemoteAPI.Internal.Requests.Route.AssetDatapoint;
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

        HttpResponseMessage response = await MakeHttpCall(PostAssetQuery.ToUrl(), PostAssetQuery.HttpMethod, httpContent);

        string responseString = await response.Content.ReadAsStringAsync();





        List<Asset> assets = JsonConvert.DeserializeObject<List<Asset>>(responseString) ?? [];


        assets.Select(asset => {

            Dictionary<string, object> jsonObject = new() { { "fromTime", (DateTime.Now - TimeSpan.FromMinutes(10)).ToString("yyyy-MM-ddTHH:mm:ss.fffff") }, { "toTime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffff") }, { "type", "string" } };
            string json = JsonConvert.SerializeObject(jsonObject);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await MakeHttpCall(GetDatapoints.ToUrl(asset.Id, "JSONReadings1"), GetDatapoints.HttpMethod, content);
            string responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<DataPoint>>(responseString) ?? [];;

            var attributeData = asset.Attributes.Select(async pair =>
            {
                string attributeName = pair.Key;


                Dictionary<string, object> jsonObject = new() { { "fromTime", (DateTime.Now - TimeSpan.FromMinutes(10)).ToString("yyyy-MM-ddTHH:mm:ss.fffff") }, { "toTime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffff") }, { "type", "string" } };
                string json = JsonConvert.SerializeObject(jsonObject);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await MakeHttpCall(GetDatapoints.ToUrl(asset.Id, attributeName), GetDatapoints.HttpMethod, content);
                string responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<DataPoint>>(responseString) ?? [];;


            }).ToList();

            return asset.Attributes.Select();
        });

        string assetId = "3ixRNBYVh8ouCsTF9cLkI5";
        string attributeName = "JSONreadings1";
    }

    public async Task<Asset?> QueryAsset(string assetId)
    {
        HttpResponseMessage response = await MakeHttpCall(GetAsset.ToUrl(assetId), GetAsset.HttpMethod);

        string readAsStringAsync = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Asset>(readAsStringAsync);
    }

    public async Task<HttpResponseMessage> MakeHttpCall(string routeUrl, HttpMethod httpMethod)
    {
        return await MakeHttpCall(routeUrl, httpMethod, null);
    }

    internal async Task<HttpResponseMessage> MakeHttpCall(string routeUrl, HttpMethod httpMethod, HttpContent httpContent)
    {
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