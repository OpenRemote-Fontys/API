using Microsoft.AspNetCore.Mvc;
using OpenRemoteAPI.Models;

namespace OpenRemoteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SensorController
{
	private readonly IConfiguration _configuration;

	[HttpGet]
	[Route("/Sensor")]
	public List<Sensor> GetSensors()
	{
		return [];
	}

	[HttpGet]
	[Route("/Sensor/Test")]
	public List<Sensor> GetDummySensors()
	{
		var rand = new Random();

		return
		[
			new Sensor
			{
				Id = 1,
				Name = "Example Sensor 1",
				RoomId = 1,
				Value = (float)Math.Round(rand.NextSingle(), 2),
				SensorType = SensorType.Noise,
				Coordinates = Coordinates.FromArray([51.450917f, 5.453000f])
			},

			new Sensor
			{
			Id = 2,
			Name = "Example Sensor 2",
			RoomId = 2,
			Value = (float)Math.Round(rand.NextSingle(), 2),
			SensorType = SensorType.People,
			Coordinates = Coordinates.FromArray([51.450361f, 5.453139f])
			}
		];
	}
}