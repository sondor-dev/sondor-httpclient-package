namespace Sondor.HttpClient;

/// <summary>
/// Represents metadata information for a paginated envelope in the Sondor Admin Hub domain.
/// </summary>
/// <remarks>
/// This record provides details about pagination, including the total number of pages, 
/// the current page, the size of each page, and whether there is a subsequent page available.
/// </remarks>
/// <param name="TotalPages">The total number of pages.</param>
/// <param name="Page">The page number.</param>
/// <param name="PageSize">The page size.</param>
/// <param name="TotalItems">The total number of items.</param>
/// <param name="HasNext">Determines if there is another page.</param>
public record SondorEnvelopeMetadata(
    int TotalPages,
    int Page,
    int PageSize,
    long TotalItems,
    bool HasNext);