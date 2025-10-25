using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sondor.HttpClient.Extensions;
using Sondor.HttpClient.Options;
using Sondor.HttpClient.Tests.Examples;
using Sondor.Tests.Extensions;
using Sondor.Translations.Args;
using Sondor.Translations.Extensions;
using Sondor.Translations.Options;

namespace Sondor.HttpClient.Tests.Extensions;

/// <summary>
/// Tests for <see cref="Sondor.HttpClient.Extensions.ServiceCollectionExtensions"/>.
/// </summary>
[TestFixture]
public class ServiceCollectionExtensionsTests
{
    /// <summary>
    /// The section.
    /// </summary>
    private const string _section = "client";

    /// <summary>
    /// The services.
    /// </summary>
    private readonly IServiceCollection _services;

    /// <summary>
    /// Create a new instance of <see cref="ServiceCollectionExtensionsTests"/>.
    /// </summary>
    public ServiceCollectionExtensionsTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddOptions(new TestSondorHttpClientOptions(), _section)
            .Build();
        
        _services = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddTestTranslation(new SondorTranslationOptions
            {
                DefaultCulture = "en",
                SupportedCultures = new LanguageArgs().Cast<string>().ToArray(),
                UseKeyAsDefaultValue = true
            }, "Test:Translation");
    }

    /// <summary>
    /// Ensures that <see cref="Sondor.HttpClient.Extensions.ServiceCollectionExtensions.AddSondorHttpClient{THttpClient,TOptions}"/> sets up HTTP client correctly.
    /// </summary>
    [Test]
    public void AddHttpClient_AddsHttpClient()
    {
        // arrange
        var expected = new TestSondorHttpClientOptions();

        // act
        _services.AddSondorHttpClient<TestSondorHttpClient, TestSondorHttpClientOptions>(_section);

        // assert
        using (Assert.EnterMultipleScope())
        {
            var provider = _services.BuildServiceProvider();
            var sondorHttpClient = provider.GetRequiredService<TestSondorHttpClient>();
            var actual = sondorHttpClient.Options;
            
            SondorHttpClientOptions.Assert(actual, expected);
        }
    }

    /// <summary>
    /// Ensures that <see cref="Sondor.HttpClient.Extensions.ServiceCollectionExtensions.AddSondorHttpClient{THttpClient,TOptions}"/> sets up HTTP client correctly.
    /// </summary>
    [Test]
    public void AddHttpClient_AddsHttpClientWithOptions()
    {
        // arrange
        var expected = new TestSondorHttpClientOptions();

        // act
        _services.AddSondorHttpClient<TestSondorHttpClient, TestSondorHttpClientOptions>(_section);

        // assert
        using (Assert.EnterMultipleScope())
        {
            var provider = _services.BuildServiceProvider();
            var sondorHttpClient = provider.GetRequiredService<TestSondorHttpClient>();
            var actual = sondorHttpClient.Options;

            SondorHttpClientOptions.Assert(actual, expected);
        }
    }
}