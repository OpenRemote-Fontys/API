namespace OpenRemoteAPI.Models;

public class Sensor
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int RoomId { get; set; }
    public float Value { get; set; }
    public SensorData SensorData { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
}

public enum SensorData
{
    Loudness, Crowdedness
}