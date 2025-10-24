using Sondor.HttpClient.Exceptions;

namespace Sondor.HttpClient.Extensions;

/// <summary>
/// Collection of <see cref="SondorEnvironments"/> extensions.
/// </summary>
public static class SondorEnvironmentsExtensions
{
    /// <summary>
    /// Converts the provided <paramref name="environment"/> to the URI fragment.
    /// </summary>
    /// <param name="environment">The environment.</param>
    /// <returns>Returns the URI fragment.</returns>
    /// <exception cref="UnsupportedSondorEnvironmentException">This exception is thrown when an unsupported environment is provided.</exception>
    public static string ToUriFragment(this SondorEnvironments environment)
    {
        return environment switch
        {
            SondorEnvironments.Development => "dev",
            SondorEnvironments.Production => "co.uk",
            // ReSharper disable once PatternIsRedundant
            SondorEnvironments.Unknown or _ => throw new UnsupportedSondorEnvironmentException(environment)
        };
    }
}