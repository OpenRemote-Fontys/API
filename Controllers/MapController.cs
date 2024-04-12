using Microsoft.AspNetCore.Mvc;
using OpenRemoteAPI.Models;

namespace OpenRemoteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapController
{
    private readonly IConfiguration _configuration;

    public MapController(IConfiguration configuration){
        _configuration = configuration;
    }

    [HttpGet]
    [Route("/Map")]
    public Map GetMap()
    {
        Map map = new Map{
            MapUrl = "https://file.io/DFRxyLFmAGc80",
            TopLeftBounds = [51.450472f, 5.452806f],
            TopRightBounds = [51.451806f, 5.453639f]
        };

        return map;
    }

    [HttpGet]
    [Route("/Map/Test")]
    public Map GetDummyMap()
    {
        Room exampleRoom1 = new Room{
            Id = 1,
            Name = "Example Room 1",
            TopLeftBounds = [51.4423907f, 5.4669287f],
            VisualizationData = "#FFA500"
        };

        Room exampleRoom2 = new Room{
            Id = 1,
            Name = "Example Room 2",
            TopLeftBounds = [51.4223907f, 5.4569287f],
            VisualizationData = "#FFA500"
        };

        List<Room> rooms = new();
        rooms.Add(exampleRoom1);
        rooms.Add(exampleRoom2);

        Map map = new Map{ 
            TopLeftBounds = [51.4508647f, 5.4509124f],
            MapUrl = "https://file.io/6voBJ5Q3w3cQ",
            Rooms = rooms
        };

        return map;
    }
}