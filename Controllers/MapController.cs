using Microsoft.AspNetCore.Mvc;
using OpenRemoteAPI.Models;

namespace OpenRemoteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapController(IConfiguration configuration)
{
	private readonly IConfiguration _configuration = configuration;

	[HttpGet]
	[Route("/Map")]
	public Map GetMap()
	{
		return new Map
		{
			MapUrl = "https://autumn.revolt.chat/attachments/RfqzEfntQZNjAT2uVc-AGm27kkYvZF_7WBtRQx11FH/TQ.svg",
			TopLeftBounds = Coordinates.FromArray([51.450472f, 5.452806f]),
			BottomRightBounds = Coordinates.FromArray([51.451806f, 5.453639f]),
			Center = Coordinates.FromArray([51.451139f, 5.4532225f]),
			Rooms =
			[
				new Room
				{
					Id = 1,
					Name = "Room 1",
					RoomBounds =
					[
						new Coordinates { Longitude = 51.451194f, Latitude = 5.452639f },
						new Coordinates { Longitude = 51.450806f, Latitude = 5.452500f },
						new Coordinates { Longitude = 51.450667f, Latitude = 5.453722f },
						new Coordinates { Longitude = 51.451056f, Latitude = 5.453833f }
					],
					Color = "#5F5F5F"
				},
				new Room
				{
					Id = 2,
					Name = "Room 2",
					RoomBounds =
					[
						new Coordinates { Longitude = 51.450611f, Latitude = 5.452417f },
						new Coordinates { Longitude = 51.450194f, Latitude = 5.452306f },
						new Coordinates { Longitude = 51.450056f, Latitude = 5.453500f },
						new Coordinates { Longitude = 51.450444f, Latitude = 5.453639f }
					],
					Color = "#2F2F2F"
				}
			]
		};
	}
}