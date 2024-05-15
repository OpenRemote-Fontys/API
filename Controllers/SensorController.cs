using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenRemoteAPI.Internal;
using OpenRemoteAPI.Internal.Models;
using OpenRemoteAPI.Internal.Requests;
using OpenRemoteAPI.Models;
using CoordinatesInfo = OpenRemoteAPI.Models.CoordinatesInfo;

namespace OpenRemoteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SensorController
{
	private readonly IConfiguration _configuration;

	//TODO Fix value, so it's not random
	private readonly Random random = new();

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

			var SensorData1 = ((JObject)asset.Attributes["JSONReadings1"].Value).ToObject<Dictionary<string, float>>();
			var SensorData2 = ((JObject)asset.Attributes["JSONReadings2"].Value).ToObject<Dictionary<string, float>>();

			// Combining the dictionaries using Concat and ToDictionary
			var SensorData = SensorData1.Concat(SensorData2)
				.GroupBy(kvp => kvp.Key)
				.ToDictionary(group => group.Key, group => group.Sum(kvp => kvp.Value));

			// make calculations based on the sensor data so it becomes float between 0 and 1 that reflects the loudness

			var coordinatesInfo = ((JObject)asset.Attributes["location"].Value).ToObject<CoordinatesInfo>();



			//TODO Fix value, so it's not random
			return new Sensor
			{
				Id = asset.Id,
				Name = asset.Name,
				RoomId = 1,
				Value = (float)Math.Round(random.NextSingle(), 2),
				SensorType = SensorType.Noise,
				Coordinates = coordinatesInfo.ToArray()
			};
		}).ToList();

		return sensors;
	}
}