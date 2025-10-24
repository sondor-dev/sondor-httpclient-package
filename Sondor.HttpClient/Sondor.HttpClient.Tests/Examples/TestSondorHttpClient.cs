namespace Sondor.HttpClient.Tests.Examples;

/// <summary>
/// The test Sondor HTTP client.
/// </summary>
/// <remarks>
/// Create a new instance of <see cref="TestSondorHttpClient"/>.
/// </remarks>
/// <param name="client">The client.</param>
/// <param name="options">The options.</param>
public class TestSondorHttpClient(System.Net.Http.HttpClient client,
    TestSondorHttpClientOptions options) :
    SondorHttpClient<TestSondorHttpClientOptions>(options, client)
{
    /// <summary>
    /// Find total.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the total.</returns>
    public async Task<HttpClientResponse<long>> FindTotalAsync(CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "v1.0/Total");
        
        var response = await Client.SendAsync(request, cancellationToken);
        
        var result = await ReadResponse<long>(response, cancellationToken);
        
        return result;
    }
}