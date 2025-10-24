using Microsoft.Extensions.DependencyInjection;
using Sondor.HttpClient.Extensions;
using Sondor.Translations;
using Sondor.Translations.Args;
using Sondor.Translations.Exceptions;
using Sondor.Translations.Extensions;
using Sondor.Translations.Options;
using System.Globalization;
using Sondor.Tests.Args;

namespace Sondor.HttpClient.Tests.Extensions;

/// <summary>
/// Tests for <see cref="SondorTranslationManagerExtensions"/>.
/// </summary>
[TestFixtureSource(typeof(LanguageArgs))]
public class SondorTranslationManagerExtensionsTests
{
    /// <summary>
    /// The language.
    /// </summary>
    private readonly string _language;

    /// <summary>
    /// The translation manager.
    /// </summary>
    private readonly ISondorTranslationManager _translationManager;

    /// <summary>
    /// The translation exceptions.
    /// </summary>
    private readonly Dictionary<string, ClientTranslations[]> _commonExceptions = new()
    {
        { "cs", [ClientTranslations.Logo] },
        { "cy", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "da", [ClientTranslations.Alias, ClientTranslations.Logo, ClientTranslations.Organisation] },
        { "de", [ClientTranslations.Alias, ClientTranslations.Logo, ClientTranslations.Name, ClientTranslations.Organisation] },
        { "es", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "et", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "fi", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "fil", [ClientTranslations.Logo] },
        { "fr", [ClientTranslations.Alias, ClientTranslations.Logo, ClientTranslations.Organisation, ClientTranslations.Page] },
        { "hr", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "it", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "lv", [ClientTranslations.Alias] },
        { "mt", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "nl", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "pl", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "ro", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "sk", [ClientTranslations.Alias, ClientTranslations.Logo] },
        { "sv", [ClientTranslations.Alias, ClientTranslations.Organisation] }
    };

    /// <summary>
    /// Create a new instance of <see cref="SondorTranslationManagerExtensionsTests"/>.
    /// </summary>
    public SondorTranslationManagerExtensionsTests(string language)
    {
        _language = language;

        var services = new ServiceCollection()
            .AddLogging()
            .AddTestTranslation(new SondorTranslationOptions
            {
                DefaultCulture = "en",
                SupportedCultures = new LanguageArgs().Cast<string>().ToArray(),
                UseKeyAsDefaultValue = true
            }, "Test:Translation");
        var provider = services.BuildServiceProvider();

        _translationManager = provider.GetRequiredService<ISondorTranslationManager>();
    }

    /// <summary>
    /// Test setup.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var culture = new CultureInfo(_language);

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }

    /// <summary>
    /// Ensures that <see cref="SondorTranslationManagerExtensions.CommonClientTranslation"/> retrieves the correct client translation based on the provided translation key.
    /// </summary>
    /// <param name="translation">The translation.</param>
    [TestCaseSource(typeof(EnumArgs<ClientTranslations>))]
    public void CommonClientTranslation(ClientTranslations translation)
    {
        // arrange
        if (translation.Equals(ClientTranslations.Unknown))
        {
            Assert.Throws<UnsupportedTranslationKeyException>(() => translation.GetTranslationKey());

            return;
        }

        var expected = translation.GetTranslationDefault();

        // act
        var result = _translationManager.CommonClientTranslation(translation);

        // assert
        if (_language.Equals("en") ||
            _commonExceptions.TryGetValue(_language, out var exceptions) && exceptions.Contains(translation))
        {
            Assert.That(result, Is.EqualTo(expected));

            return;
        }

        Assert.That(result, Is.Not.EqualTo(expected));
    }

    /// <summary>
    /// Ensures that <see cref="SondorTranslationManagerExtensions.ValidationClientTranslation"/> retrieves the correct client translation based on the provided translation key.
    /// </summary>
    /// <param name="translation">The translation.</param>
    [TestCaseSource(typeof(EnumArgs<ClientValidationTranslations>))]
    public void ValidationClientTranslation(ClientValidationTranslations translation)
    {
        // arrange
        if (translation.Equals(ClientValidationTranslations.Unknown))
        {
            Assert.Throws<UnsupportedTranslationKeyException>(() => translation.GetTranslationKey());

            return;
        }

        var expected = translation.GetTranslationDefault();

        // act
        var result = _translationManager.ValidationClientTranslation(translation);

        // assert
        if (_language.Equals("en"))
        {
            Assert.That(result, Is.EqualTo(expected));

            return;
        }

        Assert.That(result, Is.Not.EqualTo(expected));
    }
}