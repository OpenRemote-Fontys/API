using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenRemoteAPI.BusinessLogic;
using OpenRemoteAPI.Internal;
using OpenRemoteAPI.Internal.Requests;
using OpenRemoteAPI.Models;
using CoordinatesInfo = OpenRemoteAPI.Models.CoordinatesInfo;

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
			.SetRealmName("master")
			.SetLimit(0)
			.IsRecursive(true)
			.AddName(new AssetQuery.Name(AssetQuery.NameMatch.CONTAINS, false, "unit", false))
			.Build();

		var queryAssets = await openRemoteApi.QueryAssets(query);

		var sensors = queryAssets.Select(asset =>
		{

			var SensorData1 = ((JObject)asset.Attributes["JSONReadings1"].Value).ToObject<Dictionary<string, int>>();
			var SensorData2 = ((JObject)asset.Attributes["JSONReadings2"].Value).ToObject<Dictionary<string, int>>();

			// Combining the dictionaries using Concat and ToDictionary
			var SensorData = SensorData1.Concat(SensorData2)
				.GroupBy(kvp => kvp.Key)
				.ToDictionary(group => group.Key, group => group.Sum(kvp => kvp.Value));

			float loudness = ProcessingFrequencySpread.CalculateLoudness(SensorData);

			var coordinatesInfo = ((JObject)asset.Attributes["location"].Value).ToObject<CoordinatesInfo>();


			return new Sensor
			{
				Id = asset.Id,
				Name = asset.Name,
				RoomId = 1,
				Value = loudness,
				SensorType = SensorType.Noise,
				Coordinates = coordinatesInfo.ToArray()
			};
		}).ToList();

		return sensors;
	}
}