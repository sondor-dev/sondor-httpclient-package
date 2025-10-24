using Sondor.HttpClient.Options;

namespace Sondor.HttpClient.Tests.Examples;

/// <summary>
/// The test HTTP client options.
/// </summary>
public class TestSondorHttpClientOptions() :
    SondorHttpClientOptions("test-agent")
{
    /// <inheritdoc />
    public override string Service => "Test";
}
