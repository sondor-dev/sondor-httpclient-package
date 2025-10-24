using System;
using Sondor.HttpClient.Constants;

namespace Sondor.HttpClient.Exceptions;

/// <summary>
/// Failed to deserialize problem exception.
/// </summary>
/// <remarks>
/// Create a new instance of <see cref="FailedToDeserializeProblemException"/>.
/// </remarks>
/// <param name="json">The JSON.</param>
public class FailedToDeserializeProblemException(string json) :
    Exception(ExceptionConstants.FailedToDeserializeProblem)
{
    /// <summary>
    /// The JSON.
    /// </summary>
    public string Json => json;
}