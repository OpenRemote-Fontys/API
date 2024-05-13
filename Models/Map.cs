namespace OpenRemoteAPI.Models;

/// <summary>
///     Data model containing all map data
/// </summary>
public class Map
{
	/// <summary>
	///     URL of the map image to display (preferably SVG)
	/// </summary>
	public string MapUrl { get; internal set; } = "";

	/// <summary>
	///     Coordinates of the top left pixel of the image
	/// </summary>
	public Coordinates TopLeftBounds { get; internal set; } = new();

	/// <summary>
	///     Coordinates of the bottom right pixel of the image
	/// </summary>
	public Coordinates BottomRightBounds { get; internal set; } = new();

	/// <summary>
	///     List of all rooms on the map
	/// </summary>
	public List<Room> Rooms { get; internal set; } = [];

	/// <summary>
	/// Center of the map displayed to the frontend
	/// </summary>
	public Coordinates Center { get; internal set; } = new();
}