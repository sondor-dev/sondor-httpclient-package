using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace Sondor.HttpClient;

/// <summary>
/// Sondor HTTP client logger.
/// </summary>
/// <remarks>
/// Create a new instance of <see cref="SondorHttpClientLogger"/>.
/// </remarks>
/// <param name="logger">The logger.</param>
[ExcludeFromCodeCoverage]
public class SondorHttpClientLogger(ILogger<SondorHttpClientLogger> logger) : IHttpClientLogger
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<SondorHttpClientLogger> _logger = logger;
    
    /// <inheritdoc />
    public object? LogRequestStart(HttpRequestMessage request)
    {
        _logger.LogInformation(
            "Sending '{Request.Method}' to '{Request.Host}{Request.Path}'",
            request.Method,
            request.RequestUri?.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped),
            request.RequestUri!.PathAndQuery);
        
        return null;
    }

    /// <inheritdoc />
    public void LogRequestStop(object? context,
        HttpRequestMessage request,
        HttpResponseMessage response,
        TimeSpan elapsed)
    {
        _logger.LogInformation(
            "Received '{Response.StatusCodeInt} {Response.StatusCodeString}' after {Response.ElapsedMilliseconds}ms",
            (int)response.StatusCode,
            response.StatusCode,
            elapsed.TotalMilliseconds.ToString("F1"));
    }

    /// <inheritdoc />
    public void LogRequestFailed(object? context,
        HttpRequestMessage request,
        HttpResponseMessage? response,
        Exception exception,
        TimeSpan elapsed)
    {
        _logger.LogError(
            exception,
            "Request towards '{Request.Host}{Request.Path}' failed after {Response.ElapsedMilliseconds}ms",
            request.RequestUri?.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped),
            request.RequestUri!.PathAndQuery,
            elapsed.TotalMilliseconds.ToString("F1"));
    }
}