using static OpenRemoteAPI.Internal.Requests.HttpMethod;

namespace OpenRemoteAPI.Internal.Requests;

public class Route
{
    public static class Agent
    {
        public static Route GetAssetDiscovery = new Route(GET, "agent/assetDiscovery/{agentId}");
        public static Route AssetImport = new Route(POST, "agent/assetImport/{agentId}");
        public static Route GetInstanceDiscovery = new Route(GET, "agent/instanceDiscovery/{agentId}");
    }

    public static class Configuration
    {
        public static Route PostConfigurationFile = new Route(POST, "configuration/manager/file");
        public static Route Configuratin = new Route(PUT, "configuration/manager");
    }

    public static class UiApps
    {
        public static Route GetAppsInfo = new Route(GET, "apps/info");
        public static Route GetApps = new Route(GET, "apps");
        public static Route GetAppsConsoleConfig = new Route(GET, "apps/consoleConfig");
    }

    public static class AssetModel
    {
        public static Route GetAssetDescriptors = new Route(GET, "model/assetDescriptors");
        public static Route GetAssetInfo = new Route(GET, "model/assetInfo/{assetType}");
        public static Route GetAssetsInfo = new Route(GET, "model/assetInfos");
        public static Route GetMetaItemDescriptors = new Route(GET, "model/metaItemDescriptors");
        public static Route GetValueDescriptors = new Route(GET, "model/valueDescriptors");
    }

    public static class Asset
    {
        public static Route PostAsset = new Route(POST, "asset");
        public static Route DeleteAsset = new Route(DELETE, "asset");
        public static Route GetUserLinkedAssets = new Route(POST, "asset/user/link");
        public static Route PostUserLinkedAssets = new Route(POST, "asset/user/link");

        public static Route DeleteUserLinkedAssetById = new Route(DELETE, "asset/user/link/{realm}/{userId}");
        public static Route DeleteUserAsset = new Route(DELETE, "asset/user/link/{realm}/{userId}/{assetId}");

        public static Route DeleteUserLinkedAsset = new Route(DELETE, "asset/user/link/delete");
        public static Route GetAsset = new Route(GET, "asset/{assetId}");
        public static Route PutAsset = new Route(PUT, "asset/{assetId}");
        public static Route GetCurrentAsset = new Route(GET, "asset/user/current");

        public static Route GetPartialAsset = new Route(GET, "asset/partial/{assetId}");
        public static Route PostAssetQuery = new Route(POST, "asset/query");
        public static Route DeleteAssetParent = new Route(DELETE, "asset/parent");
        public static Route PutParentAsset = new Route(PUT, "asset/{parentAssetId}/child");
        public static Route PutAttributeOnAsset = new Route(PUT, "asset/{assetId}/attribute/{attributeName}");
        public static Route PutAttributesOnAsset = new Route(PUT, "asset");
    }

    public static class AssetDatapoint
    {
        public static Route ExportDatapoint = new Route(GET, "asset/datapoint/export");
        public static Route PeriodsDatapoint = new Route(GET, "asset/datapoint/periods");
        public static Route GetDatapoints = new Route(POST, "asset/datapoint/{assetId}/{attributeName}");
    }


    public HttpMethod HttpMethod { get; private set; }
    private readonly string _route;
    private readonly int _paramCount;

    public Route(HttpMethod httpMethod, string route)
    {
        this.HttpMethod = httpMethod;
        this._route = route;

        string[] template = route.Split("/");


        int arguments = 0;

        foreach (string element in template)
        {
            int openers = element.Count(x => x == '{');
            int closers = element.Count(x => x == '}');


            if (openers == 1 && closers == 1)
            {
                if (!element.StartsWith('{') || !element.EndsWith('}'))
                {
                    throw new ArgumentException("Invalid syntax of route (" + route + ") element (" + element + ")" );
                }

                arguments++;
            } else if (!(openers == 0 && closers == 0))
            {
                throw new ArgumentException("Too many hanging closers and openers (" + route + ") element (" + element + ")");
            }

            this._paramCount = arguments;

        }
    }

    public string ToUrl(params string[] arguments)
    {
        if (arguments.Length != _paramCount)
        {
            throw new ArgumentException("Invalid parameter count for route");
        }

        if (arguments.Length <= 1) return _route;

        int paramIndex = 0;
        string[] template = _route.Split("/");

        List<string> urlElementList = [];

        foreach (string element in template)
        {

            if (element.StartsWith('{'))
            {
                string name = element.Substring(1, element.Length - 1);
                string value = arguments[paramIndex];
                paramIndex++;

                urlElementList.Add(name + '=' + value);
            }
            else
            {
                urlElementList.Add(element);
            }
        }


        return string.Join('/', urlElementList);
    }
}