using System.Data;
using Newtonsoft.Json;

namespace OpenRemoteAPI.Models;

/// <summary>
///     Data model for coordinates
/// </summary>
public class CoordinatesInfo
{
	[JsonProperty("coordinates")]
	public float[] Coordinates;

	[JsonProperty("type")]
	public string Type;

	public float Longitude()
	{
		return Coordinates[0];
	}

	public float Latitude()
	{
		return Coordinates[1];
	}

	/// <summary>
	///     Converts a float array to a <see cref="Coordinates" /> object
	/// </summary>
	/// <param name="x">Array in format [Longitude, Latitude]</param>
	/// <returns>
	///     <see cref="Coordinates" />
	/// </returns>
	/// <exception cref="DataException">Raised when input array is not of length 2</exception>
	public static CoordinatesInfo FromArray(float[] x)
	{
		if (x.Length != 2) throw new DataException("Length of float array must be 2 [Longitude, Latitude]");
		return new CoordinatesInfo { Coordinates = x };
	}

	public float[] ToArray()
	{
		return Coordinates;
	}
}