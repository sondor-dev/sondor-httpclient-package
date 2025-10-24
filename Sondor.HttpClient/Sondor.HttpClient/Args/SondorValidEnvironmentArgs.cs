using System.Collections;

namespace Sondor.HttpClient.Args;

/// <summary>
/// Collection of test arguments for <see cref="SondorEnvironments"/>.
/// </summary>
public class SondorValidEnvironmentArgs :
    IEnumerable
{
    /// <inheritdoc />
    public IEnumerator GetEnumerator()
    {
        foreach (var environment in new SondorEnvironmentArgs())
        {
            if (environment.Equals(SondorEnvironments.Unknown))
            {
                continue;
            }

            yield return environment;
        }
    }
}