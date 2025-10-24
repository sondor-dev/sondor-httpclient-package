using Sondor.HttpClient.Constants;
using System;

namespace Sondor.HttpClient.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an unsupported Sondor environment is encountered.
/// </summary>
public class UnsupportedSondorEnvironmentException : Exception
{
    /// <summary>
    /// Create a new instance of <see cref="UnsupportedSondorEnvironmentException"/>.
    /// </summary>
    /// <param name="environment">The environment.</param>
    public UnsupportedSondorEnvironmentException(SondorEnvironments environment) :
        base(string.Format(ExceptionConstants.UnsupportedEnvironmentException, environment.ToString()))
    {
        Environment = environment.ToString();
    }

    /// <summary>
    /// Create a new instance of <see cref="UnsupportedSondorEnvironmentException"/>.
    /// </summary>
    /// <param name="environment">The environment.</param>
    public UnsupportedSondorEnvironmentException(string environment) :
        base(string.Format(ExceptionConstants.UnsupportedEnvironmentException, environment))
    {
        Environment = environment;
    }

    /// <summary>
    /// The environment.
    /// </summary>
    public string Environment { get; }
}