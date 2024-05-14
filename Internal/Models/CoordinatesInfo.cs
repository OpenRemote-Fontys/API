using Newtonsoft.Json;

namespace OpenRemoteAPI.Internal.Models;

public class CoordinatesInfo
{

    [JsonProperty("coordinates")]
    public float[] Coordinates;

    [JsonProperty("type")]
    public string Type;

    public float Longitude()
    {
        return Coordinates[0];
    }

    public float Latitude()
    {
        return Coordinates[1];
    }
}