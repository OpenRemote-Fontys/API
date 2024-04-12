namespace OpenRemoteAPI.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public float[]? TopLeftBounds { get; set; }
    public float[]? TopRightBounds { get; set; }
    public string VisualizationData { get; set; } = "";
}