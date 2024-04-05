namespace OpenRemoteAPI.Models;

public class Map
{
    public string SvgMap { get; set; } = "";
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public List<Room>? Rooms { get; set; }
}