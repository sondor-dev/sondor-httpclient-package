using System.Collections;
using Microsoft.Extensions.Hosting;

namespace Sondor.HttpClient.Tests.Args;

/// <summary>
/// Collection of Microsoft environments test arguments.
/// </summary>
public class MicrosoftEnvironmentsArgs :
    IEnumerable
{
    /// <inheritdoc />
    public IEnumerator GetEnumerator()
    {
        yield return Environments.Development;
        yield return Environments.Staging;
        yield return Environments.Production;
    }
}