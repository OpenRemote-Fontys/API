using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using OpenRemoteAPI.Models;

namespace OpenRemoteAPI.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class MapController
{
    private readonly IConfiguration _configuration;

    public MapController(IConfiguration configuration){
        _configuration = configuration;
    }

    [HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("/Map")]
    public Map GetMap()
    {
        Map map = new();





        return map;
    }

    [HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("/Map/Test")]
    public Map GetDummyMap()
    {
        Room exampleRoom1 = new Room{
            Id = 1,
            Name = "Example Room 1",
            Latitude = 51.4423907f,
            Longitude = 5.4669287f,
            VisualizationData = "#FFA500"
        };

        Room exampleRoom2 = new Room{
            Id = 1,
            Name = "Example Room 2",
            Latitude = 51.4223907f,
            Longitude = 5.4569287f,
            VisualizationData = "#FFA500"
        };

        List<Room> rooms = new();
        rooms.Add(exampleRoom1);
        rooms.Add(exampleRoom2);

        Map map = new Map{ 
            Latitude = 51.4508647f, 
            Longitude = 5.4509124f, 
            SvgMap = "https://simplemaps.com/static/demos/resources/svg-library/svgs/world.svg",
            Rooms = rooms
        };

        return map;
    }
}