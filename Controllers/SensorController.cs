using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenRemoteAPI.Internal;
using OpenRemoteAPI.Internal.Models;
using OpenRemoteAPI.Internal.Requests;
using OpenRemoteAPI.Models;

namespace OpenRemoteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SensorController
{
	private readonly IConfiguration _configuration;

	private readonly OpenRemoteApi openRemoteApi = new OpenRemoteApi();


	[HttpGet]
	[Route("/Sensor")]
	public async Task<List<Sensor>> GetSensors()
	{
		AssetQuery query = new AssetQueryBuilder()
			.SetLimit(0)
			.IsRecursive(true)
			.AddName(new AssetQuery.Name(AssetQuery.NameMatch.CONTAINS, false, "esp32", false))
			.Build();

		var queryAssets = await openRemoteApi.QueryAssets(query);

		var sensors = queryAssets.Select(asset =>
		{

			// Extracting the coordinates array

			var coordinatesInfo = ((JObject)asset.Attributes["location"].Value).ToObject<CoordinatesInfo>();


			//TODO Fix vallue
			return new Sensor
			{
				Id = asset.Id,
				Name = asset.Name,
				RoomId = 1,
				Value = 0.5f,
				SensorType = SensorType.Noise,
				Coordinates = Coordinates.FromArray(coordinatesInfo.Coordinates)
			};
		}).ToList();

		return sensors;
	}
}