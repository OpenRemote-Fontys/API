namespace OpenRemoteAPI.Models;

/// <summary>
///     Data model containing all map data
/// </summary>
public class Map
{
	/// <summary>
	///     Coordinates of the top left pixel of the image
	/// </summary>
	public CoordinatesInfo TopLeftBounds { get; internal set; } = new();

	/// <summary>
	///     Coordinates of the bottom right pixel of the image
	/// </summary>
	public CoordinatesInfo BottomRightBounds { get; internal set; } = new();

	/// <summary>
	///     List of all rooms on the map
	/// </summary>
	public List<Room> Rooms { get; internal set; } = [];

	/// <summary>
	/// Center of the map displayed to the frontend
	/// </summary>
	public CoordinatesInfo Center { get; internal set; } = new();
}