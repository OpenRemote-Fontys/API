namespace OpenRemoteAPI.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public float[][]? LocationArrays { get; set; }
    public string VisualizationData { get; set; } = "";
}