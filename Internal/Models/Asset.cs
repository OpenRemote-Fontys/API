using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OpenRemoteAPI.Internal.Models;

public class Asset
{

    /// <summary>
    /// Pattern: ^[0-9A-Za-z]{22}$
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; private set; }

    [JsonProperty("version")]
    public int Version { get; private set; }

    [JsonProperty("createdOn")]
    public string OnCreated { get; private set; }

    [MinLength(1)]
    [MaxLength(1023)]
    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("accessPublicRead")]
    public bool AccessPublicRead { get; private set; }

    /// <summary>
    /// ^[0-9A-Za-z]{22}$
    /// </summary>
    [JsonProperty("parentId")]
    public string ParentId { get; private set; }

    [MinLength(1)]
    [MaxLength(255)]
    [JsonProperty("realm")]
    public string Realm { get; private set; }

    [JsonProperty("type")]
    public string Type { get; private set; }

    [JsonProperty("path")]
    public string[] Path { get; private set; }

    [JsonProperty("attributes")]
    public Dictionary<string, Attribute> Attributes { get; private set; } = new();
}