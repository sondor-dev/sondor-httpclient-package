using System;

namespace Sondor.HttpClient.Options;

/// <summary>
/// Base Sondor HTTP client options.
/// </summary>
/// <param name="userAgent">The user agent.</param>
public abstract class BaseSondorHttpClientOptions(string userAgent) :
    ISondorHttpClientOptions
{
    /// <inheritdoc/>
    public abstract Uri Uri();

    /// <inheritdoc/>
    public string UserAgent => userAgent;
}