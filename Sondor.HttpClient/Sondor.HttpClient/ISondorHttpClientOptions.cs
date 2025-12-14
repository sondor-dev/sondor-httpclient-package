using System;

namespace Sondor.HttpClient;

/// <summary>
/// Sondor HTTP client options.
/// </summary>
public interface ISondorHttpClientOptions
{
    /// <summary>
    /// Gets the HTTP client URI.
    /// </summary>
    /// <returns>Returns the constructed URI.</returns>
    Uri Uri();

    /// <summary>
    /// The user agent.
    /// </summary>
    string UserAgent { get; }
}