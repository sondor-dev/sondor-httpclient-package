using Sondor.Queries;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sondor.HttpClient;

/// <summary>
/// Represents a generic envelope structure for paginated data in the Sondor Admin Hub domain.
/// </summary>
/// <typeparam name="TData">The type of the data items contained within the envelope.</typeparam>
/// <remarks>
/// This record encapsulates a collection of data items, metadata about pagination, 
/// and navigational links for accessing related pages.
/// </remarks>
/// <param name="Items">The data items.</param>
/// <param name="Meta">The envelope metadata.</param>
/// <param name="Links">The envelope links.</param>
public record SondorEnvelope<TData>(
    TData[] Items,
    SondorEnvelopeMetadata Meta,
    SondorEnvelopeLinks Links)
{
    /// <summary>
    /// Builds a new instance of <see cref="SondorEnvelope{TData}"/> with the specified items, metadata, and navigational links.
    /// </summary>
    /// <param name="items">The array of data items to include in the envelope.</param>
    /// <param name="totalItems">The total number of items available across all pages.</param>
    /// <param name="path">The base path of the resource.</param>
    /// <param name="query">
    /// The query parameters defining pagination, sorting, filtering, and other envelope-related settings.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="SondorEnvelope{TData}"/> containing the specified items, metadata, and links.
    /// </returns>
    /// <remarks>
    /// This method calculates pagination metadata and constructs navigational links based on the provided query parameters.
    /// </remarks>
    public static SondorEnvelope<TData> BuildEnvelope(TData[] items, long totalItems,
        string path,
        IEnvelopeQuery query)
    {
        var totalPages = (int)Math.Ceiling((double)totalItems / query.PageSize);

        var metadata = new SondorEnvelopeMetadata(totalPages,
            query.Page,
            query.PageSize,
            totalItems,
            query.Page < totalPages);

        var nextPage = query.Page + 1 < totalPages ? query.Page : totalPages;
        var previousPage = query.Page > 1 ? query.Page : 1;

        var links = new SondorEnvelopeLinks(
            First: BuildLink(1, path, query),
            Last: BuildLink(totalPages, path, query),
            Next: BuildLink(nextPage, path, query),
            Previous: BuildLink(previousPage, path, query),
            Self: BuildLink(query.Page, path, query));

        var envelope = new SondorEnvelope<TData>(items,
            metadata,
            links);

        return envelope;
    }
    
    /// <summary>
    /// Constructs a navigational link based on the provided page number, path, and query parameters.
    /// </summary>
    /// <param name="page">The page number for which the link is being built.</param>
    /// <param name="path">The base path of the resource.</param>
    /// <param name="query">The query parameters containing pagination, sorting, filtering, and other options.</param>
    /// <returns>A string representing the constructed link with the appropriate query parameters.</returns>
    /// <remarks>
    /// This method generates a URL with query parameters for pagination, sorting, filtering, 
    /// searching, and field selection. It ensures that the link reflects the current state of the query.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    private static string BuildLink(
        int page,
        string path,
        IEnvelopeQuery query
        )
    {
        var link = $"{path}?page={page}&pageSize={query.PageSize}";

        if (query.Sorts.HasValue && query.Sorts != SortQuery.Empty)
        {
            link += $"&sorts={query.Sorts}";
        }

        if (query.Filters.HasValue && query.Filters != FilterQuery.Empty)
        {
            link += $"&filters={query.Filters}";
        }

        if (query.Search.HasValue && query.Search != SearchQuery.Empty)
        {
            link += $"&search={query.Search}";
        }

        if (query.Fields.HasValue && query.Fields != FieldsQuery.All)
        {
            link += $"&fields={query.Fields}";
        }

        return link;
    }
}