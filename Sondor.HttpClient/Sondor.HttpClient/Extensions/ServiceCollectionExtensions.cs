using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sondor.HttpClient.Options;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Sondor.Options.Extensions;

namespace Sondor.HttpClient.Extensions;

/// <summary>
/// Collection of <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Sondor HTTP client.
    /// </summary>
    /// <typeparam name="THttpClient">The HTTP client type.</typeparam>
    /// <typeparam name="TOptions">The options type.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="section">The section name.</param>
    /// <param name="clientConfig">The HTTP client configuration.</param>
    /// <param name="clientBuilder">The client handler.</param>
    /// <returns>Returns the services.</returns>
    public static IServiceCollection AddSondorHttpClient<THttpClient, TOptions>(this IServiceCollection services,
        string section,
        Action<System.Net.Http.HttpClient>? clientConfig = null,
        Action<IHttpClientBuilder>? clientBuilder = null)
        where TOptions : SondorHttpClientOptions
        where THttpClient : SondorHttpClient<TOptions>
    {
        var provider = services.BuildServiceProvider();
        var clientLogger = provider.GetService<SondorHttpClientLogger>();

        var loggerFactory = provider.GetService<ILoggerFactory>();

        if (loggerFactory is null)
        {
            services.AddLogging();

            provider = services.BuildServiceProvider();
            loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        }

        if (clientLogger is null)
        {
            var logger = loggerFactory.CreateLogger<SondorHttpClientLogger>();

            services.AddSingleton(new SondorHttpClientLogger(logger));

            services.BuildServiceProvider();
        }

        var version = (typeof(TOptions).Assembly.GetName().Version ?? new Version(1, 0, 0)).ToString();

        services.AddSondorOptions<TOptions>(section: section);
        provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<TOptions>>().Value;

        var builder = services.AddHttpClient<THttpClient>(client =>
        {
            client.BaseAddress = options.Uri();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(options.UserAgent, version));

            clientConfig?.Invoke(client);
        });
        
        builder
            .RemoveAllLoggers()
            .AddLogger<SondorHttpClientLogger>();

        clientBuilder?.Invoke(builder);

        return services;
    }
}