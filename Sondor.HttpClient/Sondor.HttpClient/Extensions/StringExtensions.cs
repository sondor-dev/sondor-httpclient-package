using System;
using System.Text.Json;

namespace Sondor.HttpClient.Extensions;

/// <summary>
/// Collection of <see cref="string"/> extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Determines if the provided string is valid JSON.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>Returns true if valid, otherwise false.</returns>
    public static bool IsValidJson(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }
        
        try
        {
            JsonDocument.Parse(value);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}