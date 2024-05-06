namespace OpenRemoteAPI.Internal.Requests;

public class AssetQueryBuilder
{
    public bool Recursive { get; private set; }
    public AssetQuery.AccessLevels Access { get; private set; }

    public string RealmName { get; private set; }

    public List<AssetQuery.Name> Names { get; private set; }

    public HashSet<string> UserIds { get; private set; } = [];
    public HashSet<string> Types { get; private set; } = [];

    public AssetQuery.Properties OrderByProperty { get; private set; }
    public bool OrderByDescending { get; private set; }

    public int Limit { get; private set; }



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
        this.UserIds.UnionWith(userIds);
        return this;
    }

    public AssetQueryBuilder AddTypes(params string[] types)
    {
        this.Types.UnionWith(types);
        return this;
    }

    public AssetQueryBuilder OrderBy(AssetQuery.Properties orderProperty, bool descending)
    {
        this.OrderByProperty = orderProperty;
        this.OrderByDescending = descending;
        return this;
    }

    public AssetQueryBuilder SetLimit(int limit)
    {
        this.Limit = limit;
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
            new AssetQuery.OrderBy(OrderByProperty, OrderByDescending),
            Limit);
    }
}