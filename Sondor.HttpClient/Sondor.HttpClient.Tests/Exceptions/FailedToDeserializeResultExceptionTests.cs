using Sondor.HttpClient.Constants;
using Sondor.HttpClient.Exceptions;

namespace Sondor.HttpClient.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="FailedToDeserializeResultException{TResult}"/>.
/// </summary>
[TestFixture]
public class FailedToDeserializeResultExceptionTests
{
    /// <summary>
    /// Ensures that <see cref="FailedToDeserializeResultException{TResult}"/> constructor sets properties correctly.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        var type = typeof(long);

        // act
        var exception = new FailedToDeserializeResultException<long>();

        // assert
        Assert.That(exception.Message, Is.EqualTo(string.Format(ExceptionConstants.FailedToDeserializeResult, type)));
    }
}