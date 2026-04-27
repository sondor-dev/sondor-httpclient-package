using Newtonsoft.Json;
using System;
using System.Text;

namespace Sondor.HttpClient.Extensions;

/// <summary>
/// Provides extension methods for object manipulation and processing.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Creates a hash string representation of the specified object.
    /// </summary>
    /// <param name="request">The object to be hashed. It is serialized to JSON before hashing.</param>
    /// <returns>A hexadecimal string representing the MD5 hash of the serialized object.</returns>
    /// <remarks>
    /// This method uses JSON serialization to convert the object into a string and then computes its MD5 hash.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="request"/> is null.</exception>
    public static string CreateHash(this object request)
    {
        var json = JsonConvert.SerializeObject(request, Formatting.None);
        var hashBytes = System.Security.Cryptography.MD5.HashData(Encoding.UTF8.GetBytes(json));
        var hashString = Convert.ToHexString(hashBytes);

        return hashString;
    }
}