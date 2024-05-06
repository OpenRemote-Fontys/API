using JetBrains.Annotations;
using static OpenRemoteAPI.Internal.Requests.HttpMethod;

namespace OpenRemoteAPI.Internal.Requests;

public class Route
{
    public static class Agent
    {
        public static Route getAssetDiscovery = new Route(GET, "/agent/assetDiscovery/{agentId}");
        public static Route assetImport = new Route(POST, "/agent/assetImport/{agentId}");
        public static Route getInstanceDiscovery = new Route(GET, "/agent/instanceDiscovery/{agentId}");
    }

    public static class Configuration
    {
        public static Route postConfigurationFile = new Route(POST, "/configuration/manager/file");
        //TODO
        public static Route configuratin = new Route(PUT, "/configuration/manager");
    }

    public static class UiApps
    {
        public static Route getAppsInfo = new Route(GET, "/apps/info");
        public static Route getApps = new Route(GET, "/apps");
        public static Route getAppsConsoleConfig = new Route(GET, "/apps/consoleConfig");
    }

    public static class AssetModel
    {
        public static Route getAssetDescriptors = new Route(GET, "/model/assetDescriptors");
        public static Route getAssetInfo = new Route(GET, "/model/assetInfo/{assetType}");
        public static Route getAssetsInfo = new Route(GET, "/model/assetInfos");
        public static Route getMetaItemDescriptors = new Route(GET, "/model/metaItemDescriptors");
        public static Route getValueDescriptors = new Route(GET, "/model/valueDescriptors");
    }

    public static class Asset
    {
        public static Route postAsset = new Route(POST, "/asset");
        public static Route deleteAsset = new Route(DELETE, "/asset");
        public static Route getUserLinkedAssets = new Route(POST, "/asset");
        public static Route deleteUserLinkedAssetById = new Route(DELETE, "/asset/user/link/{realm}/{userId}");

        //TODO
        public static Route deleteUserAsset = new Route(DELETE, "/asset/user/link/{realm}/{userId}/{assetId}");

        public static Route deleteUserLinkedAsset = new Route(DELETE, "/asset");
        public static Route getAsset = new Route(GET, "/asset/user/link/delete");
        public static Route putAsset = new Route(PUT, "/asset/{assetId}");
        public static Route getCurrentAsset = new Route(GET, "/asset/{assetId}");

        public static Route getPartialAsset = new Route(GET, "/asset");
        public static Route postAssetQuery = new Route(POST, "/asset");
        public static Route deleteAssetParent = new Route(DELETE, "/asset");
        public static Route putParentAsset = new Route(PUT, "/asset");
        public static Route putAttributeOnAsset = new Route(PUT, "/asset");
        public static Route putAttributesOnAsset = new Route(PUT, "/asset");
    }

    public HttpMethod HttpMethod { get; private set; }
    private readonly string _route;
    private readonly int _paramCount;

    public Route(HttpMethod httpMethod, [RouteTemplate] string route)
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
                    throw new ArgumentException("Invalid syntax of route element");
                }

                arguments++;
            } else if (!(openers == 0 && closers == 0))
            {
                throw new ArgumentException("Too many hanging closers and openers");
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