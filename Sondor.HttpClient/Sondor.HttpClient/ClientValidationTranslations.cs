using Sondor.HttpClient.Constants;
using Sondor.Translations.Attributes;

namespace Sondor.HttpClient;

/// <summary>
/// Collection of client validation translation keys.
/// </summary>
public enum ClientValidationTranslations
{
    /// <summary>
    /// The default and unknown translation.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The must be greater than zero translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.MustBeGreaterThanZero)]
    [TranslationDefault(TranslationDefaultConstants.MustBeGreaterThanZero)]
    MustBeGreaterThanZero = 1,

    /// <summary>
    /// The is required translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.IsRequired)]
    [TranslationDefault(TranslationDefaultConstants.IsRequired)]
    IsRequired = 2,

    /// <summary>
    /// Must not be empty translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.MustNotBeEmpty)]
    [TranslationDefault(TranslationDefaultConstants.MustNotBeEmpty)]
    MustNotBeEmpty = 3,

    /// <summary>
    /// Must not exceed maximum length translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.MustNotExceedMaximumLength)]
    [TranslationDefault(TranslationDefaultConstants.MustNotExceedMaximumLength)]
    MustNotExceedMaximumLength = 4,

    /// <summary>
    /// Must be an absolute URI translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.MustBeAnAbsoluteUri)]
    [TranslationDefault(TranslationDefaultConstants.MustBeAnAbsoluteUri)]
    MustBeAnAbsoluteUri = 5
}