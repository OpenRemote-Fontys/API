using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenRemoteAPI.Internal;
using OpenRemoteAPI.Internal.Requests;
using OpenRemoteAPI.Models;

namespace OpenRemoteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapController(IConfiguration configuration)
{
	private readonly IConfiguration _configuration = configuration;

	[HttpGet]
	[Route("/Map")]
	public async Task<Map> GetMap()
	{
		var openRemote = new OpenRemoteApi();

		var assetQuery = new AssetQueryBuilder()
			.AddUser("7HoUNjdgA7JYVVgFnYU7ps")
			.IsRecursive(true)
			.Build();

		var mapAsset = await openRemote.QueryAssets(assetQuery);

		var map = new Map();
		var rooms = new List<Room>();

		mapAsset.ForEach((asset) =>
		{
			switch (asset.Type)
			{
				case "BuildingAsset":
					var bounds = ((JArray)asset.dictionary["bounds"].Value).ToObject<float[][]>(); // JArray to float[][]

					map = new Map()
					{
						TopLeftBounds = Coordinates.FromArray(bounds[0]),
						BottomRightBounds = Coordinates.FromArray(bounds[1]),
						Rooms = []
					};
					break;

				case "RoomAsset":
					var roomBounds = ((JArray)asset.dictionary["roomBounds"].Value).ToObject<List<float[]>>(); // JArrau to List<float[]>

					rooms.Add(new Room()
					{
						Id = asset.Id,
						Name = asset.Name,
						RoomBounds = roomBounds.Select(Coordinates.FromArray).ToList(),
						Color = asset.dictionary["color"].Value
					});
					break;

				default:
					Console.WriteLine($"Received unexpected assetType: {asset.Type}");
					break;
			}
		});

		map.Rooms = rooms;

		return map;
	}
}