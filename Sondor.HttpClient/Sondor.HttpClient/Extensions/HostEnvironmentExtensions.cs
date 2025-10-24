using Microsoft.Extensions.Hosting;

namespace Sondor.HttpClient.Extensions;

/// <summary>
/// Collection of <see cref="IHostEnvironment"/> extensions.
/// </summary>
public static class HostEnvironmentExtensions
{
    /// <summary>
    /// Converts the <see cref="IHostEnvironment"/> to a <see cref="SondorEnvironments"/>.
    /// </summary>
    /// <param name="hostEnvironment">The host environment.</param>
    /// <returns>Returns the environment.</returns>
    public static SondorEnvironments ToSondorEnvironment(this IHostEnvironment hostEnvironment)
    {
        if (hostEnvironment.IsDevelopment())
        {
            return SondorEnvironments.Development;
        }

        return hostEnvironment.IsProduction() ?
            SondorEnvironments.Production :
            SondorEnvironments.Unknown;
    }
}