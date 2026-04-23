namespace Sondor.HttpClient;

/// <summary>
/// Represents a collection of navigational links for an envelope in the Sondor Admin Hub domain.
/// </summary>
/// <param name="First">The first page query link.</param>
/// <param name="Last">The last page query link.</param>
/// <param name="Next">The next page query link.</param>
/// <param name="Previous">The previous page query link.</param>
/// <param name="Self">The current query link.</param>
public record SondorEnvelopeLinks(string First,
    string Last,
    string Next,
    string Previous,
    string Self);