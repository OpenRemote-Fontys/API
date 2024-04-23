namespace OpenRemoteAPI.Models;

/// <summary>
///     Data model containing all data on a room
/// </summary>
public class Room
{
    /// <summary>
    ///     A unique ID for the room
    /// </summary>
    public int Id { get; internal set; }

    /// <summary>
    ///     The name of the room
    /// </summary>
    public string Name { get; internal set; } = "";

    /// <summary>
    ///     List of coordinates of all corners of the room
    /// </summary>
    public List<Coordinates> LocationArrays { get; internal set; } = [];

    /// <summary>
    ///     Color of the room
    /// </summary>
    public string Color { get; internal set; } = "#FFFFFF";
}