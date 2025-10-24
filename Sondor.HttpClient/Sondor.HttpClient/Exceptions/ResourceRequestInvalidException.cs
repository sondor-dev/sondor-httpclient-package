using Sondor.Errors.Exceptions;
using Sondor.HttpClient.Constants;

namespace Sondor.HttpClient.Exceptions;

/// <summary>
/// Resource request invalid exception.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="ResourceRequestInvalidException"/>.
/// </remarks>
/// <param name="method">The method.</param>
/// <param name="path">The path/</param>
public sealed class ResourceRequestInvalidException(string method,
    string path) :
    ResourceInvalidException(string.Format(ExceptionConstants.ResourceRequestInvalid,
        method,
        path));