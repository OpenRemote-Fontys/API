using Newtonsoft.Json;

namespace OpenRemoteAPI.Models;

/* Helper classes */
public class DataPoint()
{
    [JsonProperty("y")] public Dictionary<string, int> frequencies { get; private set; }
}