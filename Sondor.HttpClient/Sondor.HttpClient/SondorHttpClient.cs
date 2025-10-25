using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sondor.HttpClient.Exceptions;
using Sondor.HttpClient.Extensions;
using Sondor.HttpClient.Options;
using Sondor.ProblemResults;
using Sondor.Translations.Args;
using Sondor.Translations.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sondor.Tests.Extensions;
using Sondor.Translations.Extensions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Sondor.HttpClient;

/// <summary>
/// Sondor HTTP client.
/// </summary>
/// <remarks>
/// Create a new instance of <see cref="SondorHttpClient{TOptions}"/>.
/// </remarks>
/// <param name="options">The options.</param>
/// <param name="client">The HTTP client.</param>
/// <typeparam name="TOptions">The options type.</typeparam>
public class SondorHttpClient<TOptions>(IOptions<TOptions> options,
    System.Net.Http.HttpClient client)
    where TOptions : SondorHttpClientOptions
{
    /// <summary>
    /// The options.
    /// </summary>
    public readonly TOptions Options = options.Value;

    /// <summary>
    /// The HTTP client.
    /// </summary>
    public readonly System.Net.Http.HttpClient Client = client;

    /// <summary>
    /// Sets the base URI.
    /// </summary>
    /// <param name="uri">The URI.</param>
    public void SetBaseUri(Uri uri)
    {
        Client.BaseAddress = uri;
    }

    /// <summary>
    /// Read response
    /// </summary>
    /// <param name="response">The response.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the response.</returns>
    public async Task<HttpClientResponse> ReadResponse(HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new HttpClientResponse();
        }

        if (!SondorProblemDetails.IsValidJson(json))
        {
            return new HttpClientResponse();
        }

        var problem = HandleProblemJson(json);

        return new HttpClientResponse(problem!);
    }

    /// <summary>
    /// Read response
    /// </summary>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <param name="response">The response.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the response.</returns>
    /// <exception cref="FailedToDeserializeProblemException">This exception is thrown when a problem detail failed to deserialize.</exception>
    public async Task<HttpClientResponse<TResult>> ReadResponse<TResult>(HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        var problem = HandleProblemJson(json);

        if (problem is not null)
        {
            return new HttpClientResponse<TResult>(problem);
        }

        if (!json.IsValidJson())
        {
            return new HttpClientResponse<TResult>();
        }

        var result = JsonSerializer.Deserialize<TResult>(json) ??
            throw new FailedToDeserializeResultException<TResult>();

        return new HttpClientResponse<TResult>(result);
    }

    /// <summary>
    /// Generate Bearer token using <see cref="DefaultAzureCredential"/>.
    /// </summary>
    /// <param name="applicationId">The application id.</param>
    /// <param name="azureCredentials">The Azure credentials.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the access token, using the default azure credentials.</returns>
    [ExcludeFromCodeCoverage(Justification = "Investigate how to test, without exposing secrets")]
    public async Task<string> GenerateToken(string applicationId,
        DefaultAzureCredential? azureCredentials = null,
        CancellationToken cancellationToken = default)
    {
        azureCredentials ??= new DefaultAzureCredential();

        var appScope = $"api://{applicationId}/.default";

        var accessToken = await azureCredentials.GetTokenAsync(new TokenRequestContext([appScope]), cancellationToken);

        return accessToken.Token;
    }

    /// <summary>
    /// Generate Bearer token for JWT.
    /// </summary>
    /// <param name="tenantId">The tenant id.</param>
    /// <param name="clientId">The client id.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="instance">The instance.</param>
    /// <param name="apiApplicationId">The API application id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the access token.</returns>
    [ExcludeFromCodeCoverage(Justification = "Investigate how to test, without exposing secrets")]
    public async Task<string> ClientCredentials(string tenantId,
        string clientId,
        string clientSecret,
        string instance,
        string apiApplicationId,
        CancellationToken cancellationToken = default)
    {
        var httpClient = new System.Net.Http.HttpClient
        {
            BaseAddress = new Uri(instance)
        };

        var appScope = $"api://{clientId}/.default";

        var request = new HttpRequestMessage(HttpMethod.Post, $"{tenantId}/oauth2/v2.0/token")
        {
            Content = new FormUrlEncodedContent([
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("scope", appScope),
                new KeyValuePair<string, string>("scopes", $"api://{apiApplicationId}/Organisation.Read api://{apiApplicationId}/Organisation.Write"),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            ])
        };

        var response = await httpClient.SendAsync(request, cancellationToken);

        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        var tokenResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(json) ??
            throw new FailedToDeserializeResultException<Dictionary<string, object>>();

        if (tokenResponse.TryGetValue("access_token", out var accessTokenObj))
        {
            return accessTokenObj.ToString() ?? string.Empty;
        }

        throw new InvalidOperationException("Failed to get access token.");
    }

    /// <summary>
    /// Send async request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the client response.</returns>
    public async Task<HttpClientResponse> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        var response = await Client.SendAsync(request, cancellationToken);

        var result = await ReadResponse(response, cancellationToken);

        return result;
    }

    /// <summary>
    /// Send async request.
    /// </summary>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the client response.</returns>
    [ExcludeFromCodeCoverage]
    public async Task<HttpClientResponse<TResult>> SendAsync<TResult>(HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        var response = await Client.SendAsync(request, cancellationToken);
        
        var result = await ReadResponse<TResult>(response, cancellationToken);
        
        return result;
    }

    /// <summary>
    /// Clears authorization.
    /// </summary>
    public void ClearAuthorization()
    {
        Client.DefaultRequestHeaders.Authorization = null;
    }

    /// <summary>
    /// Sets the bearer token.
    /// </summary>
    /// <param name="token">The token.</param>
    public void SetBearerToken(string token)
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    /// <summary>
    /// Sets the client accept language header.
    /// </summary>
    /// <param name="language">The language.</param>
    public void SetAcceptLanguage(string language)
    {
        Client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
    }

    /// <summary>
    /// Creates a test client.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="options">The options.</param>
    /// <param name="clientConfig">The client config.</param>
    /// <param name="clientBuilder">The client builder.</param>
    /// <param name="loggingBuilder">The logging builder.</param>
    /// <typeparam name="TClient">The client type.</typeparam>
    /// <returns>Returns the test client.</returns>
    public static TClient CreateTestClient<TClient>(TOptions options,
        string section = "client",
        Action<System.Net.Http.HttpClient>? clientConfig = null,
        Action<IHttpClientBuilder>? clientBuilder = null,
        Action<ILoggingBuilder>? loggingBuilder = null)
        where TClient : SondorHttpClient<TOptions>
    {
        loggingBuilder ??= builder => builder.AddConsole();

        var configuration = new ConfigurationBuilder()
            .AddOptions(options, sectionName: section)
            .Build();

        var services = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddLogging(loggingBuilder)
            .AddTestTranslation(new SondorTranslationOptions
            {
                DefaultCulture = "en",
                SupportedCultures = new LanguageArgs().Cast<string>().ToArray(),
                UseKeyAsDefaultValue = true
            }, "Test:Translation");

        services.AddSondorHttpClient<TClient, TOptions>(section, clientConfig, clientBuilder);

        var provider = services.BuildServiceProvider();
        var client = provider.GetRequiredService<TClient>();

        return client;
    }

    /// <summary>
    /// Handles the provided <paramref name="json"/>, if the json is problem object.
    /// </summary>
    /// <param name="json">The JSON.</param>
    /// <returns>Returns the problem.</returns>
    /// <exception cref="FailedToDeserializeProblemException">This exception is thrown when a problem detail failed to deserialize.</exception>
    [ExcludeFromCodeCoverage]
    private static SondorProblemDetails? HandleProblemJson(string json)
    {
        if (!SondorProblemDetails.IsValidJson(json))
        {
            return null;
        }

        var problem = JsonSerializer.Deserialize<SondorProblemDetails>(json) ??
            throw new FailedToDeserializeProblemException(json);

        if (problem.Extensions.TryGetValue("extensions", out var extensions) && extensions is not null)
        {
            var extensionsJson = extensions.ToString() ?? "[]";

            problem.Extensions = JsonConvert.DeserializeObject<Dictionary<string, object?>>(extensionsJson) ?? [];
        }

        problem = HandleExtensions(problem);

        return problem;
    }

    /// <summary>
    /// Handle extension formatting.
    /// </summary>
    /// <param name="problem">The problem.</param>
    /// <returns>Returns the formatted problem.</returns>
    [ExcludeFromCodeCoverage]
    private static SondorProblemDetails HandleExtensions(SondorProblemDetails problem)
    {
        if (!problem.Extensions.TryGetValue("errors", out var errorsObject))
        {
            return problem;
        }
        
        var errorsJson = errorsObject?.ToString() ?? "[]";
        var failures = JsonConvert.DeserializeObject<ValidationError[]>(errorsJson) ?? [];

        problem.Extensions["errors"] = failures.ToList();

        return problem;
    }
}