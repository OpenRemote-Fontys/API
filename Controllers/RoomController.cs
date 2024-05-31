using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenRemoteAPI.Internal.Requests;
using OpenRemoteAPI.Internal;
using OpenRemoteAPI.Models;
using System.Net;
using System;
using Newtonsoft.Json;
using System.Text;

namespace OpenRemoteAPI.Controllers
{    
    [ApiController]
    public class RoomController : Controller
    {
        [HttpGet]
        [Route("/Room/Free")]
        public async Task<IActionResult> GetFreeRooms(int index)
        {
            var openRemote = new OpenRemoteApi();
            var roomQuery = new AssetQueryBuilder()
                .SetRealmName("master")
                .SetLimit(0)
                .AddTypes("RoomAsset")
                .Build();

            var rooms = await openRemote.QueryAssets(roomQuery);

            var sensorQuery = new AssetQueryBuilder()
                .SetRealmName("master")
                .SetLimit(0)
                .SetFilterAttribute("Room", rooms[index].Id)
                .Build();

            var sensors = await openRemote.QueryAssets(sensorQuery);
            return Ok(sensors);
        }

        [HttpGet]
        [Route("/Room/All")]
        public async Task<IActionResult> GetAllRooms()
        {
            var openRemote = new OpenRemoteApi();

            var assetQuery = new AssetQueryBuilder()
                .SetRealmName("master")
                .SetLimit(0)
                .AddTypes("RoomAsset")
                .Build();

            var response = await openRemote.QueryAssets(assetQuery);
            return Ok(response);
        }

        [HttpGet]
        [Route("/Room/Data")]
        public async Task<IActionResult> GetRoomData(int index)
        {
            var openRemote = new OpenRemoteApi();
            var roomQuery = new AssetQueryBuilder()
                .SetRealmName("master")
                .SetLimit(0)
                .AddTypes("RoomAsset")
                .Build();

            var rooms = await openRemote.QueryAssets(roomQuery);

            var sensorQuery = new AssetQueryBuilder()
                .SetRealmName("master")
            .SetLimit(0)
                .SetFilterAttribute("Room", rooms[index].Id)
                .Build();

            var sensors = await openRemote.QueryAssets(sensorQuery);

            Dictionary<string, object> jsonObject = new() { { "fromTime", (DateTime.Now - TimeSpan.FromMinutes(1)).ToString("yyyy-MM-ddTHH:mm:ss.fffff") }, { "toTime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffff") }, { "type", "string" } };
            string json = JsonConvert.SerializeObject(jsonObject);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await openRemote.MakeHttpCall("asset/datapoint/" + sensors[0].Id + "/JSONReadings2", Internal.Requests.HttpMethod.POST, content);


            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpGet]
        [Route("/Room/Free/Test")]
        [ProducesResponseType(typeof(List<Room>), 200)]
        public IActionResult Test()
        {
            return Ok(new List<Room> { 
                new Room { Id = "1", Name = "2.254", Color = "0000ff" },
                new Room { Id = "2", Name = "2.356", Color = "00ff00" },
                new Room { Id = "3", Name = "2.125", Color = "ff0000" }
            });
        }
    }
}