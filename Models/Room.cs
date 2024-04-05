namespace OpenRemoteAPI.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public string VisualizationData { get; set; } = "";
}