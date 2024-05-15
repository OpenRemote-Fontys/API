using OpenRemoteAPI.Internal.Models;

namespace OpenRemoteAPI.Models;

/// <summary>
///     Data model containing data of a single sensor
/// </summary>
public class Sensor
{
	/// <summary>
	///		A unique ID for the sensor
	/// </summary>
	public string Id { get; internal set; }

	/// <summary>
	///		The name of the sensor
	/// </summary>
	public string Name { get; internal set; } = "";

	/// <summary>
	///		The ID of the room the sensor is located in
	/// </summary>
	public int RoomId { get; internal set; }

	/// <summary>
	///		The current value of the sensor between 0 and 1
	/// </summary>
	public float Value { get; internal set; }

	/// <summary>
	///		The type of data the sensor returns
	/// </summary>
	public SensorType SensorType { get; internal set; }

	/// <summary>
	///		The location of the sensor
	/// </summary>
	public float[] Coordinates { get; internal set; }
}