using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using static OpenRemoteAPI.Internal.Requests.AssetQuery;

namespace OpenRemoteAPI.Internal.Requests;

public class AssetQuery(
    bool recursive,
    AssetQuery.AccessLevels? access,
    string realmName,
    List<AssetQuery.Name> names,
    HashSet<string> userIds,
    HashSet<string> types,
    AssetQuery.OrderBy? orderQueryBy,
    Int32 limit,
    FilterAttribute? attribute)
{
    [JsonProperty("recursive")] public readonly bool Recursive = recursive;

    [JsonProperty("access")] public readonly AssetQuery.AccessLevels? Access = access;
  
    [JsonProperty("realm")] public readonly RealmName _RealmName = new(realmName);

    [JsonProperty("names")] public readonly List<Name> Names = names;

    [JsonProperty("ids")] public readonly HashSet<string> UserIds = userIds;

    [JsonProperty("types")] public readonly HashSet<string> Types = types;

    [JsonProperty("orderBy")] public readonly OrderBy? OrderQueryBy = orderQueryBy;

    [JsonProperty("limit")] public readonly Int32 Limit = limit;

    [JsonProperty("attributes")] public readonly Dictionary<string, List<FilterAttribute>>? Attributes = attribute == null ? null : new() { { "items", new() { attribute } } };


    public class Name(NameMatch nameMatch, bool caseSensitive, string value, bool negate)
    {
        [JsonProperty("match")] public readonly NameMatch NameMatch = nameMatch;
        [JsonProperty("caseSensitive")] public readonly bool CaseSensitive = caseSensitive;
        [JsonProperty("value")] public readonly string Value = value;
        [JsonProperty("negate")] public readonly bool Negate = negate;
        [JsonProperty("predicateType")] public readonly string PredicateType = "string";
    }


    public class RealmName(string name)
    {
        [JsonProperty("name")] public readonly string Name = name;
    }

    public class FilterAttribute
    {
        public FilterAttribute(string value, string attributeName)
        {
            Name = new() { { "match", "EXACT" }, { "value", attributeName }, { "predicateType", "string" } };
            Value = new() { { "value", value }, { "predicateType", "string" } };
        }

        [JsonProperty("name")] public readonly Dictionary<string, object> Name;
        [JsonProperty("value")] public readonly Dictionary<string, object> Value;
    }

    public class OrderBy(Properties orderByProperty, bool orderByDescending)
    {
        [JsonProperty("property")] public readonly Properties OrderByProperty = orderByProperty;
        [JsonProperty("descending")] public readonly bool OrderByDescending = orderByDescending;
    }

    public enum NameMatch
    {
        EXACT,
        BEGIN,
        END,
        CONTAINS
    }

    public enum AccessLevels
    {
        PRIVATE,
        PROTECTED,
        PUBLIC
    }

    public enum Properties
    {
        CREATED_ON,
        FIRST_NAME,
        LAST_NAME,
        USERNAME,
        EMAIL
    }
}