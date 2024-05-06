using Newtonsoft.Json;

namespace OpenRemoteAPI.Internal.Models;

public class Attribute
{
    [JsonProperty("name")]
    private string Name;

    [JsonProperty("type")]
    private string Type;

    [JsonProperty("meta")]
    private Meta Meta;

    [JsonProperty("value")]
    private dynamic Value;

    // TODO
    // [JsonProperty("timestamp")]
    // private DateTime Timestamp;
}

internal class Meta
{
    // TODO
}