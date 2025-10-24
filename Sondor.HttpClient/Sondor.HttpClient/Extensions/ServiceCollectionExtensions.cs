using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sondor.HttpClient.Options;
using System.Net.Http.Headers;

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
    /// <param name="options">The options.</param>
    /// <returns>Returns the services.</returns>
    public static IServiceCollection AddSondorHttpClient<THttpClient, TOptions>(this IServiceCollection services,
        TOptions options)
        where TOptions : SondorHttpClientOptions
        where THttpClient : SondorHttpClient<TOptions>
    {
        services.AddSondorHttpClient<THttpClient, TOptions, HttpMessageHandler>(options, new HttpClientHandler());
        
        return services;
    }

    /// <summary>
    /// Add Sondor HTTP client.
    /// </summary>
    /// <typeparam name="THttpClient">The HTTP client type.</typeparam>
    /// <typeparam name="TOptions">The options type.</typeparam>
    /// <typeparam name="THttpMessageHandler">The HTTP message handler.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="options">The options.</param>
    /// <param name="handler">The HTTP handler.</param>
    /// <returns>Returns the services.</returns>
    public static IServiceCollection AddSondorHttpClient<THttpClient, TOptions, THttpMessageHandler>(this IServiceCollection services,
        TOptions options,
        THttpMessageHandler? handler = null)
        where TOptions : SondorHttpClientOptions
        where THttpClient : SondorHttpClient<TOptions>
        where THttpMessageHandler : HttpMessageHandler
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

        var version = (typeof(SondorHttpClientOptions).Assembly.GetName().Version ?? new Version(1, 0, 0)).ToString();

        services.AddSingleton(options);
        var builder = services.AddHttpClient<THttpClient>(client =>
        {
            client.BaseAddress = options.Uri();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(options.UserAgent, version));
        });
        
        builder
            .RemoveAllLoggers()
            .AddLogger<SondorHttpClientLogger>();

        if (handler is not null)
        {
            builder.ConfigurePrimaryHttpMessageHandler(() => handler);
        }

        return services;
    }
}