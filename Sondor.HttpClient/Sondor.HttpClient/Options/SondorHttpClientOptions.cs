using Sondor.HttpClient.Constants;
using System;
using System.Diagnostics.CodeAnalysis;
using Sondor.Errors.Exceptions;
using Sondor.HttpClient.Extensions;

namespace Sondor.HttpClient.Options;

/// <summary>
/// Sondor HTTP client options.
/// </summary>
/// <remarks>
/// Create a new instance of <see cref="SondorHttpClientOptions"/>.
/// </remarks>
/// <param name="userAgent">The user agent.</param>
public class SondorHttpClientOptions(string userAgent)
{
    /// <summary>
    /// The service name.
    /// </summary>
    public virtual string Service { get; init; } = string.Empty;

    /// <summary>
    /// Determines whether to use HTTPs.
    /// </summary>
    public virtual bool UseHttps { get; init; } = true;

    /// <summary>
    /// The user agent.
    /// </summary>
    public string UserAgent => userAgent;

    /// <summary>
    /// The domain.
    /// </summary>
    public virtual string Domain { get; init; } = OptionsConstants.SondorServicesDomain;

    /// <summary>
    /// The URI format.
    /// </summary>
    public virtual string UriFormat { get; init; } = OptionsConstants.UriFormat;

    /// <summary>
    /// The client options.
    /// </summary>
    public virtual SondorEnvironments Environment { get; init; } = SondorEnvironments.Production;

    /// <summary>
    /// Gets the HTTP client URI.
    /// </summary>
    /// <returns>Returns the constructed URI.</returns>
    public virtual Uri Uri()
    {
        var protocol = UseHttps ? "https" : "http";
        var environment = Environment.ToUriFragment();
        var uri = string.Format(UriFormat, protocol, Service, Domain, environment);
        
        return new Uri(uri, UriKind.Absolute);
    }

    /// <summary>
    /// Asserts the provided <paramref name="actual"/> matches the provided <paramref name="expected"/>.
    /// </summary>
    /// <param name="actual">The actual.</param>
    /// <param name="expected">The expected.</param>
    /// <exception cref="SondorAssertionException">This exception is thrown when an assertion fails.</exception>
    [ExcludeFromCodeCoverage]
    public static void Assert(SondorHttpClientOptions? actual, SondorHttpClientOptions? expected)
    {
        if (actual is null && expected is null)
        {
            return;
        }

        if (actual is null && expected is not null)
        {
            throw new SondorAssertionException(
                "Unfortunately, the provided 'actual' value is null but was expected not to be.");
        }

        if (actual is not null && expected is null)
        {
            throw new SondorAssertionException(
                "Unfortunately, the provided 'actual' value is not null but was expected to be.");
        }

        if (actual!.UseHttps != expected!.UseHttps)
        {
            throw new SondorAssertionException(
                $"The 'UseHttps' values are different. Expected '{expected.UseHttps}', but was '{actual.UseHttps}'.");
        }

        if (actual.Domain != expected.Domain)
        {
            throw new SondorAssertionException(
                $"The 'Domain' values are different. Expected '{expected.Domain}', but was '{actual.Domain}'.");
        }

        if (actual.UriFormat != expected.UriFormat)
        {
            throw new SondorAssertionException(
                $"The 'UriFormat' values are different. Expected '{expected.UriFormat}', but was '{actual.UriFormat}'.");
        }

        if (actual.Environment != expected.Environment)
        {
            throw new SondorAssertionException(
                $"The 'Environment' values are different. Expected '{expected.Environment}', but was '{actual.Environment}'.");
        }
        
        if (actual.Service != expected.Service)
        {
            throw new SondorAssertionException(
                $"The 'Service' values are different. Expected '{expected.Service}', but was '{actual.Service}'.");
        }

        if (actual.UserAgent != expected.UserAgent)
        {
            throw new SondorAssertionException(
                $"The 'UserAgent' values are different. Expected '{expected.UserAgent}', but was '{actual.UserAgent}'.");
        }
    }
}