using System;
using Sondor.HttpClient.Constants;
using Sondor.ProblemResults;

namespace Sondor.HttpClient.Exceptions;

/// <summary>
/// Sondor problem details exception.
/// </summary>
/// <remarks>
/// Create a new instance of <see cref="SondorProblemDetailsException"/>.
/// </remarks>
/// <param name="problem">the problem details.</param>
public class SondorProblemDetailsException(SondorProblemDetails problem) :
    Exception(ExceptionConstants.SondorProblemDetailsException)
{
    /// <summary>
    /// The problem.
    /// </summary>
    public SondorProblemDetails Problem => problem;
}