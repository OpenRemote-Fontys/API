using Microsoft.AspNetCore.Mvc;
using OpenRemoteAPI.Models;

namespace OpenRemoteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SensorController
{
    private readonly IConfiguration _configuration;

    public SensorController(IConfiguration configuration){
        _configuration = configuration;
    }

    [HttpGet]
    [Route("/Sensor")]
    public Map GetSensors()
    {
        Map map = new();





        return map;
    }

    [HttpGet]
    [Route("/Sensor/Test")]
    public List<Sensor> GetDummySensors()
    {
        Sensor exampleSensor1 = new Sensor{
            Id = 1,
            Name = "Example Sensor 1",
            RoomId = 1,
            Value = 0.5324234f,
            SensorData = "Crowdedness",
            Latitude = 51.4423907f,
            Longitude = 5.4669287f,
        };

        Sensor exampleSensor2 = new Sensor{
            Id = 1,
            Name = "Example Sensor 2",
            RoomId = 2,
            Value = 0.2345262f,
            SensorData = "Loudness",
            Latitude = 51.4223907f,
            Longitude = 5.4569287f,
        };

        List<Sensor> sensors = new();
        sensors.Add(exampleSensor1);
        sensors.Add(exampleSensor2);

        return sensors;
    }
}