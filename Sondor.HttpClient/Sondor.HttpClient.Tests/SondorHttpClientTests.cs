using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using Sondor.HttpClient.Constants;
using Sondor.HttpClient.Options;
using Sondor.HttpClient.Tests.Examples;
using Sondor.ProblemResults.Extensions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Sondor.ProblemResults;
using Sondor.Tests.Args;

namespace Sondor.HttpClient.Tests;

/// <summary>
/// Tests for <see cref="SondorHttpClient{TOptions}"/>.
/// </summary>
[TestFixture]
public class SondorHttpClientTests
{
    /// <summary>
    /// Ensures the <see cref="SondorHttpClient{TOptions}"/> constructor sets properties correctly.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        var expectedOptions = CreateTestOptions();
        
        // act
        var actual = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(options: expectedOptions);

        // assert
        using (Assert.EnterMultipleScope())
        {
            SondorHttpClientOptions.Assert(actual.Options, expectedOptions);
            Assert.That(actual.Client, Is.Not.Null);
        }
    }

    /// <summary>
    /// Ensures the <see cref="SondorHttpClient{TOptions}.ReadResponse{TResult}"/> works as expected.
    /// </summary>
    [Test]
    public async Task ReadResponse_success()
    {
        // arrange
        const long expected = 100;
        var options = CreateTestOptions();
        var mockHttpHandler = new MockHttpMessageHandler();
        mockHttpHandler
            .When(HttpMethod.Get, $"{options.Uri()}v1.0/Total")
            .Respond(HttpStatusCode.OK, JsonContent.Create(expected));

        var client = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(options: options, clientBuilder: clientBuilder =>
        {
            clientBuilder.ConfigurePrimaryHttpMessageHandler(() => mockHttpHandler);
        });

        // act
        var actual = await client.FindTotalAsync(TestContext.CurrentContext.CancellationToken);

        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual.Problem, Is.Null);
            Assert.That(actual.Value, Is.Not.Negative);
            Assert.That(actual.Value, Is.EqualTo(expected));
        }
    }

    /// <summary>
    /// Ensures the <see cref="SondorHttpClient{TOptions}.ReadResponse{TResult}"/> works as expected.
    /// </summary>
    [TestCaseSource(typeof(StringArgs))]
    public async Task ReadResponseTyped_NoPayload(string? value)
    {
        // arrange
        var options = CreateTestOptions();
        var mockHttpHandler = new MockHttpMessageHandler();
        mockHttpHandler
            .When(HttpMethod.Get, $"{options.Uri()}v1.0/Total")
            .Respond(HttpStatusCode.OK, new StringContent(value ?? string.Empty));

        var client = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(options: options, clientBuilder: clientBuilder =>
        {
            clientBuilder.ConfigurePrimaryHttpMessageHandler(() => mockHttpHandler);
        });

        // act
        var actual = await client.FindTotalAsync(TestContext.CurrentContext.CancellationToken);

        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual.Problem, Is.Null);
            Assert.That(actual.Value, Is.Zero);
        }
    }

    /// <summary>
    /// Ensures the <see cref="SondorHttpClient{TOptions}.ReadResponse{TResult}"/> works as expected.
    /// </summary>
    [TestCaseSource(typeof(StringArgs))]
    public async Task ReadResponse_NoPayload(string? value)
    {
        // arrange
        var options = CreateTestOptions();
        var mockHttpHandler = new MockHttpMessageHandler();
        mockHttpHandler
            .When(HttpMethod.Get, $"{options.Uri()}v1.0/Total")
            .Respond(HttpStatusCode.OK, new StringContent(value ?? string.Empty));

        var client = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(options: options, clientBuilder: clientBuilder =>
        {
            clientBuilder.ConfigurePrimaryHttpMessageHandler(() => mockHttpHandler);
        });
        var request = new HttpRequestMessage(HttpMethod.Get, "v1.0/Total");

        // act
        var actual = await client.SendAsync(request, TestContext.CurrentContext.CancellationToken);

        // assert
        Assert.That(actual.Problem, Is.Null);
    }

    /// <summary>
    /// Ensures the <see cref="SondorHttpClient{TOptions}.ReadResponse{TResult}"/> works as expected when a problem is returned.
    /// </summary>
    [Test]
    public async Task ReadResponseTyped_problem()
    {
        // arrange
        var options = CreateTestOptions();
        var expected = new DefaultHttpContext()
            .ResourceNotFoundProblem("title", "detail", "error", "test", "property-name", "property-value");
        var mockHttpHandler = new MockHttpMessageHandler();
        mockHttpHandler
            .When(HttpMethod.Get, $"{options.Uri()}v1.0/Total")
            .Respond(HttpStatusCode.NotFound, JsonContent.Create(expected));
        var client = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(options: options, clientBuilder: clientBuilder =>
        {
            clientBuilder.ConfigurePrimaryHttpMessageHandler(() => mockHttpHandler);
        });

        var request = new HttpRequestMessage(HttpMethod.Get, "v1.0/Total");
        var actual = await client.SendAsync<int>(request, TestContext.CurrentContext.CancellationToken);

        // act & assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual.Value, Is.Default);
            Assert.That(actual.Problem, Is.Not.Null);
            SondorProblemDetails.Assert(actual.Problem, expected);
        }
    }

    /// <summary>
    /// Ensures the <see cref="SondorHttpClient{TOptions}.ReadResponse{TResult}"/> works as expected when a problem is returned.
    /// </summary>
    [Test]
    public async Task ReadResponse_problem()
    {
        // arrange
        var options = CreateTestOptions();
        var expected = new DefaultHttpContext()
            .ResourceNotFoundProblem("title", "detail", "error", "test", "property-name", "property-value");
        var mockHttpHandler = new MockHttpMessageHandler();
        mockHttpHandler
            .When(HttpMethod.Get, $"{options.Uri()}v1.0/Total")
            .Respond(HttpStatusCode.NotFound, JsonContent.Create(expected));
        var client = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(options: options, clientBuilder: clientBuilder =>
        {
            clientBuilder.ConfigurePrimaryHttpMessageHandler(() => mockHttpHandler);
        });

        var request = new HttpRequestMessage(HttpMethod.Get, "v1.0/Total");
        var actual = await client.SendAsync(request, TestContext.CurrentContext.CancellationToken);

        // act & assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual.Problem, Is.Not.Null);
            SondorProblemDetails.Assert(actual.Problem, expected);
        }
    }

    /// <summary>
    /// Ensures that <see cref="SondorHttpClient{TOptions}.SetBaseUri"/>
    /// </summary>
    [Test]
    public void SetBaseUri()
    {
        // arrange
        var options = new TestSondorHttpClientOptions();
        var client = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(options: options);
        var expected = new Uri("https://example.com/");
        
        // act
        client.SetBaseUri(expected);
        
        // assert
        Assert.That(client.Client.BaseAddress, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures that <see cref="SondorHttpClient{TOptions}.SetBearerToken"/> clears authorization header.
    /// </summary>
    [Test]
    public void SetBearerToken()
    {
        // arrange
        const string token = "test-token";
        var client = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(CreateTestOptions());

        // act
        client.SetBearerToken(token);

        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(client.Client.DefaultRequestHeaders.Authorization, Is.Not.Null);
            Assert.That(client.Client.DefaultRequestHeaders.Authorization!.Scheme, Is.EqualTo("Bearer"));
            Assert.That(client.Client.DefaultRequestHeaders.Authorization!.Parameter, Is.EqualTo(token));
        }
    }

    /// <summary>
    /// Ensures that <see cref="SondorHttpClient{TOptions}.SetAcceptLanguage"/> clears authorization header.
    /// </summary>
    [Test]
    public void SetAcceptLanguage()
    {
        // arrange
        const string language = "es";
        StringWithQualityHeaderValue[] expected = [new (language)];
        var client = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(CreateTestOptions());

        // act
        client.SetAcceptLanguage(language);

        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(client.Client.DefaultRequestHeaders.AcceptLanguage, Is.Not.Null);
            Assert.That(client.Client.DefaultRequestHeaders.AcceptLanguage.Count, Is.EqualTo(1));
            Assert.That(client.Client.DefaultRequestHeaders.AcceptLanguage, Is.EqualTo(expected));
        }
    }

    /// <summary>
    /// Ensures that <see cref="SondorHttpClient{TOptions}.ClearAuthorization"/> clears authorization header.
    /// </summary>
    [Test]
    public void ClearAuthorization()
    {
        // arrange
        var client = TestSondorHttpClient.CreateTestClient<TestSondorHttpClient>(CreateTestOptions());
        client.SetBearerToken("test-token");

        // act
        client.ClearAuthorization();

        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(client.Client.DefaultRequestHeaders.Authorization, Is.Null);
        }
    }

    /// <summary>
    /// Creates a test client options.
    /// </summary>
    /// <returns>Returns the test client options.</returns>
    private static TestSondorHttpClientOptions CreateTestOptions()
    {
        var options = new TestSondorHttpClientOptions
        {
            Domain = "domain",
            Service = "service",
            Environment = SondorEnvironments.Development,
            UriFormat = OptionsConstants.UriFormat,
            UseHttps = false
        };

        return options;
    }
}