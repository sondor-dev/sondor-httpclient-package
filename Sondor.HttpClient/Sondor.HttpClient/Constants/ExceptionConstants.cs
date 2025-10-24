namespace Sondor.HttpClient.Constants;

/// <summary>
/// Collection of internal exception constants.
/// </summary>
internal class ExceptionConstants
{
    /// <summary>
    /// The failed to deserialize result exception format.
    /// </summary>
    public const string FailedToDeserializeResult =
        "Failed to deserialize the result of type '{0}'.";

    /// <summary>
    /// The failed to deserialize problem exception format.
    /// </summary>
    public const string FailedToDeserializeProblem =
        "Failed to deserialize problem details from JSON.";

    /// <summary>
    /// The unsupported environment exception format.
    /// </summary>
    public const string UnsupportedEnvironmentException =
        "Unfortunately, the provided environment of '{0}' is unsupported.";

    /// <summary>
    /// The sondor problem details exception message.
    /// </summary>
    public const string SondorProblemDetailsException =
        "Unfortunately, a problem has occurred attempting to process a request.";

    /// <summary>
    /// The resource request is invalid.
    /// </summary>
    public const string ResourceRequestInvalid = "Unfortunately, the request payload is not valid. {0} {1}";
}