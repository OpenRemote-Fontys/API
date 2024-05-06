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
		var exampleSensor1 = new Sensor
		{
			Id = 1,
			Name = "Example Sensor 1",
			RoomId = 1,
			Value = 0.5324234f,
			SensorType = SensorType.Noise,
			Coordinates = Coordinates.FromArray([51.4423907f, 5.4669287f])
		};

		var exampleSensor2 = new Sensor
		{
			Id = 2,
			Name = "Example Sensor 2",
			RoomId = 2,
			Value = 0.2345262f,
			SensorType = SensorType.People,
			Coordinates = Coordinates.FromArray([51.4223907f, 5.4669287f])
		};

		return [exampleSensor1, exampleSensor2];
	}
}