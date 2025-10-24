using Sondor.HttpClient.Constants;
using Sondor.Translations;
using Sondor.Translations.Extensions;

namespace Sondor.HttpClient.Extensions;

/// <summary>
/// Collection of extensions for <see cref="ISondorTranslationManager"/>.
/// </summary>
public static class SondorTranslationManagerExtensions
{
    /// <summary>
    /// Get common translation by key.
    /// </summary>
    /// <param name="translationManager">The translation manager.</param>
    /// <param name="translation">The translation key.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>Returns the common translation.</returns>
    public static string CommonClientTranslation(this ISondorTranslationManager translationManager,
        ClientTranslations translation,
        params object[] parameters)
    {
        var key = translation.GetTranslationKey();
        var defaultValue = translation.GetTranslationDefault();

        return translationManager.Translate(key,
            TranslationConstants.Location,
            TranslationConstants.CommonResource,
            defaultValue,
            parameters);
    }

    /// <summary>
    /// Get validation translation by key.
    /// </summary>
    /// <param name="translationManager">The translation manager.</param>
    /// <param name="translation">The translation.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>Returns the validation translation.</returns>
    public static string ValidationClientTranslation(this ISondorTranslationManager translationManager,
        ClientValidationTranslations translation,
        params object[] parameters)
    {
        var key = translation.GetTranslationKey();
        var defaultValue = translation.GetTranslationDefault();
        return translationManager.Translate(key,
            TranslationConstants.Location,
            TranslationConstants.ValidationResource,
            defaultValue,
            parameters);
    }
}
