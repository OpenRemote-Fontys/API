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
            MapUrl = "https://autumn.revolt.chat/attachments/RfqzEfntQZNjAT2uVc-AGm27kkYvZF_7WBtRQx11FH/TQ.svg",
            TopLeftBounds = [51.450472f, 5.452806f],
            TopRightBounds = [51.451806f, 5.453639f]
        };

        return map;
    }
}