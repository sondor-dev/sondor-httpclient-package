using Sondor.Queries;

namespace Sondor.HttpClient;

/// <summary>
/// Represents a query interface for handling envelope-based data retrieval operations.
/// </summary>
/// <remarks>
/// This interface defines properties for pagination, sorting, filtering, field selection, and search functionality,
/// enabling flexible and efficient querying of data envelopes.
/// </remarks>
public interface IEnvelopeQuery
{
    /// <summary>
    /// The page number.
    /// </summary>
    int Page { get; }

    /// <summary>
    /// The page size.
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// The sorts query.
    /// </summary>
    SortQuery? Sorts { get; }

    /// <summary>
    /// The filters query.
    /// </summary>
    FilterQuery? Filters { get; }

    /// <summary>
    /// The fields query.
    /// </summary>
    FieldsQuery? Fields { get; }

    /// <summary>
    /// The search query.
    /// </summary>
    SearchQuery? Search { get; }
}