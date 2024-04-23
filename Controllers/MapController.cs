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
			Rooms =
			[
				new Room
				{
					Id = 1,
					Name = "Room 1",
					LocationArrays =
					[
						new Coordinates { Longitude = 51.45098336666666f, Latitude = 5.4530463552083335f },
						new Coordinates { Longitude = 51.45100683518518f, Latitude = 5.4530463552083335f },
						new Coordinates { Longitude = 51.45100683518518f, Latitude = 5.453057201562499f },
						new Coordinates { Longitude = 51.45098336666666f, Latitude = 5.453057201562499f }
					],
					Color = "#5F5F5F"
				},
				new Room
				{
					Id = 2,
					Name = "Room 2",
					LocationArrays =
					[
						new Coordinates { Longitude = 51.451041420370366f, Latitude = 5.453043318229167f },
						new Coordinates { Longitude = 51.45113652962963f, Latitude = 5.453043318229167f },
						new Coordinates { Longitude = 51.45113652962963f, Latitude = 5.453057635416666f },
						new Coordinates { Longitude = 51.451042655555554f, Latitude = 5.453057635416666f }
					],
					Color = "#2F2F2F"
				}
			]
		};
	}
}