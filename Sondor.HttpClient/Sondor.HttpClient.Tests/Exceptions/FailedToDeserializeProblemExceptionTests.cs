using Sondor.HttpClient.Constants;
using Sondor.HttpClient.Exceptions;

namespace Sondor.HttpClient.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="FailedToDeserializeProblemException"/>.
/// </summary>
[TestFixture]
public class FailedToDeserializeProblemExceptionTests
{
    /// <summary>
    /// Ensures that <see cref="FailedToDeserializeProblemException"/> constructor sets properties correctly.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        const string json = "test";

        // act
        var exception = new FailedToDeserializeProblemException(json);

        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception.Json, Is.EqualTo(json));
            Assert.That(exception.Message, Is.EqualTo(ExceptionConstants.FailedToDeserializeProblem));
        }
    }
}