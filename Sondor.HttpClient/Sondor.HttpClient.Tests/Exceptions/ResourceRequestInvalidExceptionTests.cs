using Sondor.HttpClient.Constants;
using Sondor.HttpClient.Exceptions;

namespace Sondor.HttpClient.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="ResourceRequestInvalidException"/>.
/// </summary>
[TestFixture]
public class ResourceRequestInvalidExceptionTests
{
    /// <summary>
    /// Ensures that <see cref="ResourceRequestInvalidException"/> is constructed correctly.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // Arrange
        const string method = "POST";
        const string path = "/api/resource";
        var expected = string.Format(ExceptionConstants.ResourceRequestInvalid,
            method,
            path);

        // Act
        var exception = new ResourceRequestInvalidException(method, path);

        // Assert
        Assert.That(exception.Message, Is.EqualTo(expected));
    }
}