using System.Collections;

namespace Sondor.HttpClient.Args;

/// <summary>
/// Collection of test arguments for <see cref="SondorEnvironments"/>.
/// </summary>
public class SondorEnvironmentArgs :
    IEnumerable
{
    /// <inheritdoc />
    public IEnumerator GetEnumerator()
    {
        yield return SondorEnvironments.Unknown;
        yield return SondorEnvironments.Development;
        yield return SondorEnvironments.Production;
    }
}