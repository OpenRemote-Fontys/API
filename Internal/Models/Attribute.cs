using Newtonsoft.Json;

namespace OpenRemoteAPI.Internal.Models;

public class Attribute
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("type")]
    public string Type;

    [JsonProperty("meta")]
    public Meta Meta;

    [JsonProperty("value")]
    public dynamic Value;

    // TODO
    // [JsonProperty("timestamp")]
    // public DateTime Timestamp;
}

public class Meta
{
    // TODO
}