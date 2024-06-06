using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenRemoteAPI.Internal.Requests;
using OpenRemoteAPI.Internal;
using OpenRemoteAPI.Models;
using System.Net;
using System;
using Newtonsoft.Json;
using System.Text;
using OpenRemoteAPI.Internal.Models;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json.Linq;


namespace OpenRemoteAPI.Controllers
{    
    [ApiController]
    public class RoomController : Controller
    {

        [HttpGet]
        [Route("/Room/Free")]
        public async Task<IActionResult> GetFreeRooms()
        {
            var openRemote = new OpenRemoteApi();
            var roomQuery = new AssetQueryBuilder()
                .SetRealmName("master")
                .SetLimit(0)
                .AddTypes("RoomAsset")
                .Build();

            List<Asset> rooms = await openRemote.QueryAssets(roomQuery);

            List<Asset> emptyRoomAssets = new List<Asset>();

            foreach (Asset room in rooms)
            {
                var sensorQuery = new AssetQueryBuilder()
                    .SetRealmName("master")
                    .SetLimit(0)
                    .SetFilterAttribute("Room", room.Id)
                    .Build();

                Asset? sensor = (await openRemote.QueryAssets(sensorQuery)).FirstOrDefault();

                if (sensor == null) continue;

                Dictionary<string, object> jsonObject = new() { { "fromTime", (DateTime.Now - TimeSpan.FromMinutes(1)).ToString("yyyy-MM-ddTHH:mm:ss.fffff") }, { "toTime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffff") }, { "type", "string" } };
                string json = JsonConvert.SerializeObject(jsonObject);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await openRemote.MakeHttpCall("asset/datapoint/" + sensor.Id + "/JSONReadings2", Internal.Requests.HttpMethod.POST, content);
                var jsonString = await response.Content.ReadAsStringAsync();
                
                List<DataPoint> datapoints = JsonConvert.DeserializeObject<List<DataPoint>>(jsonString);

                if (IsFree(datapoints)) emptyRoomAssets.Add(room);
            }

            List<Room> emptyRooms = new List<Room>();
            foreach (Asset room in emptyRoomAssets)
            {
                var roomBounds = ((JArray)room.Attributes["roomBounds"].Value).ToObject<List<float[]>>();
                emptyRooms.Add(new Room() { Id = room.Id, Name = room.Name, RoomBounds = roomBounds, Color = room.Attributes["color"].Value });
            }

            return Ok(emptyRooms);
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


        /* Helper classes */
        public class DataPoint()
        {
            [JsonProperty("y")] public Dictionary<string, int> frequencies { get; private set; }
        }

        /* Helper Methods */
        private bool IsFree(List<DataPoint> datapoints) 
        {
            int thresholdMetCount = 0;
            foreach (DataPoint datapoint in datapoints)
            {
                int totalValues = 0;
                for (int i = datapoint.frequencies.Count - 4; i < datapoint.frequencies.Count; i++)
                {
                    totalValues += datapoint.frequencies.ElementAt(i).Value;
                }
                if (totalValues > /*5000*/50000) thresholdMetCount++;
            }
            return thresholdMetCount < (datapoints.Count / 2);
        }
    }
}

// 5000Hz > 6000