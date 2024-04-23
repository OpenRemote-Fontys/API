using System.Data;

namespace OpenRemoteAPI.Models;

/// <summary>
///     Data model for coordinates
/// </summary>
public class Coordinates
{
	public float Longitude { get; internal set; }
	public float Latitude { get; internal set; }

	public override string ToString()
	{
		return $"[{Longitude}, {Latitude}]";
	}

	/// <summary>
	///     Converts a float array to a <see cref="Coordinates" /> object
	/// </summary>
	/// <param name="x">Array in format [Longitude, Latitude]</param>
	/// <returns>
	///     <see cref="Coordinates" />
	/// </returns>
	/// <exception cref="DataException">Raised when input array is not of length 2</exception>
	public static Coordinates FromArray(float[] x)
	{
		if (x.Length != 2) throw new DataException("Length of float array must be 2 [Longitude, Latitude]");
		return new Coordinates { Longitude = x[0], Latitude = x[1] };
	}
}