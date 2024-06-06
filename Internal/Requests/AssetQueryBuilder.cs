namespace OpenRemoteAPI.Internal.Requests;
using static OpenRemoteAPI.Internal.Requests.AssetQuery;

public class AssetQueryBuilder
{
    public bool Recursive { get; private set; }
    public AccessLevels? Access { get; private set; }

    public string RealmName { get; private set; }

    public List<Name> Names { get; private set; }

    public HashSet<string> UserIds { get; private set; }
  
    public HashSet<string> Types { get; private set; }

    public Properties? OrderByProperty { get; private set; }

    public bool OrderByDescending { get; private set; }

    public Int32 Limit { get; private set; }

    public FilterAttribute? Attribute { get; private set; }



    public AssetQueryBuilder IsRecursive(bool recursive)
    {
        this.Recursive = recursive;
        return this;
    }

    public AssetQueryBuilder SetRealmName(string realmName)
    {
        this.RealmName = realmName;
        return this;
    }

    public AssetQueryBuilder AddName(params AssetQuery.Name[] names)
    {
        this.Names ??= [];
        this.Names.AddRange(names);
        return this;
    }

    public AssetQueryBuilder SetAccess(AssetQuery.AccessLevels accessLevels)
    {
        this.Access = accessLevels;
        return this;
    }

    public AssetQueryBuilder AddUser(params string[] userIds)
    {
        this.UserIds ??= [];
        this.UserIds.UnionWith(userIds);
        return this;
    }

    public AssetQueryBuilder AddTypes(params string[] types)
    {
        this.Types ??= [];
        this.Types.UnionWith(types);
        return this;
    }

    public AssetQueryBuilder OrderBy(AssetQuery.Properties orderProperty, bool descending)
    {
        this.OrderByProperty = orderProperty;
        this.OrderByDescending = descending;
        return this;
    }

    public AssetQueryBuilder SetLimit(Int32 limit)
    {
        this.Limit = limit;
        return this;
    }

    public AssetQueryBuilder SetFilterAttribute(string name, string value)
    {
        this.Attribute = new(value, name);
        return this;
    }

    public AssetQuery Build()
    {
        return new AssetQuery(
            Recursive,
            Access,
            RealmName,
            Names,
            UserIds,
            Types,
            OrderByProperty == null ? null : new AssetQuery.OrderBy(OrderByProperty.Value, OrderByDescending),
            Limit,
            Attribute);
    }
}