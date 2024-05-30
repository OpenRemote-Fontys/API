using Newtonsoft.Json;

namespace OpenRemoteAPI.Internal.Models;

public class Config
{
    [JsonProperty("openremote_token")] 
    public string OpenRemoteToken { get; set; }
  
    [JsonProperty("base_url")] 
    public string BaseUrl { get; set; }
}