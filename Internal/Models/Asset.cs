using System.ComponentModel.DataAnnotations;

namespace OpenRemoteAPI.Internal.Models;

public class Asset
{

    /// <summary>
    /// Pattern: ^[0-9A-Za-z]{22}$
    /// </summary>
    public string Id { get; private set; }

    public int Version { get; private set; }

    public string OnCreated { get; private set; }

    [MinLength(1)]
    [MaxLength(1023)]
    public string Name { get; private set; }

    public bool AccessPublicRead { get; private set; }

    /// <summary>
    /// ^[0-9A-Za-z]{22}$
    /// </summary>
    public string ParentId { get; private set; }

    [MinLength(1)]
    [MaxLength(255)]
    public string Realm { get; private set; }

    public string Type { get; private set; }

    public string[] Path { get; private set; }

    public List<Attribute> Attributes { get; private set; }
}