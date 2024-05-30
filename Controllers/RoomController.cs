using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenRemoteAPI.Models;
using System.Net;

namespace OpenRemoteAPI.Controllers
{    
    [ApiController]
    public class RoomController : Controller
    {
        [HttpGet]
        [Route("/Room/Free")]
        public IActionResult GetFreeRooms()
        {   
            return null;
        }

        [HttpGet]
        [Route("/Room/Free/Test")]
        [ProducesResponseType(typeof(List<Room>), 200)]
        public IActionResult Test()
        {
            return Ok(new List<Room> { 
                new Room { Id = 1, Name = "2.254", Color = "0000ff" },
                new Room { Id = 2, Name = "2.356", Color = "00ff00" },
                new Room { Id = 3, Name = "2.125", Color = "ff0000" }
            });
        }
    }
}