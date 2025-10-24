using Sondor.HttpClient.Constants;
using Sondor.Translations.Attributes;

namespace Sondor.HttpClient;

/// <summary>
/// Collection of client translation keys.
/// </summary>
public enum ClientTranslations
{
    /// <summary>
    /// The default and unknown translation.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The alias translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.Alias)]
    [TranslationDefault(TranslationDefaultConstants.Alias)]
    Alias = 1,

    /// <summary>
    /// The created translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.Created)]
    [TranslationDefault(TranslationDefaultConstants.Created)]
    Created = 2,

    /// <summary>
    /// The id translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.Id)]
    [TranslationDefault(TranslationDefaultConstants.Id)]
    Id = 3,

    /// <summary>
    /// The logo translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.Logo)]
    [TranslationDefault(TranslationDefaultConstants.Logo)]
    Logo = 4,

    /// <summary>
    /// The name translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.Name)]
    [TranslationDefault(TranslationDefaultConstants.Name)]
    Name = 5,

    /// <summary>
    /// The organisation translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.Organisation)]
    [TranslationDefault(TranslationDefaultConstants.Organisation)]
    Organisation = 6,

    /// <summary>
    /// The organisation id translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.OrganisationId)]
    [TranslationDefault(TranslationDefaultConstants.OrganisationId)]
    OrganisationId = 7,

    /// <summary>
    /// The page translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.Page)]
    [TranslationDefault(TranslationDefaultConstants.Page)]
    Page = 8,

    /// <summary>
    /// The page size translation.
    /// </summary>
    [TranslationKey(TranslationKeyConstants.PageSize)]
    [TranslationDefault(TranslationDefaultConstants.PageSize)]
    PageSize = 9,
}