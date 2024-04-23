namespace OpenRemoteAPI.Models;

public class Map
{
    public string MapUrl { get; set; } = "";
    public float[]? TopLeftBounds { get; set; }
    public float[]? TopRightBounds { get; set; }
    public List<Room>? Rooms { get; set; }
}