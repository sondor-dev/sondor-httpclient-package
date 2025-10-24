using Microsoft.AspNetCore.Http;
using Sondor.HttpClient.Constants;
using Sondor.HttpClient.Exceptions;

namespace Sondor.HttpClient.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="SondorProblemDetailsException"/>.
/// </summary>
[TestFixture]
public class SondorProblemDetailsExceptionTests
{
    /// <summary>
    /// Ensures the <see cref="SondorProblemDetailsException"/> constructor sets properties correctly.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        var problem = new ProblemResults.SondorProblemDetails
        {
            Type = "type",
            Title = "title",
            Status = StatusCodes.Status400BadRequest,
            Detail = "detail",
            Instance = "instance"
        };
        
        // act
        var actual = new SondorProblemDetailsException(problem);
        
        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Message, Is.EqualTo(ExceptionConstants.SondorProblemDetailsException));
            Assert.That(actual.Problem, Is.Not.Null);
            Assert.That(actual.Problem.Type, Is.EqualTo(problem.Type));
            Assert.That(actual.Problem.Title, Is.EqualTo(problem.Title));
            Assert.That(actual.Problem.Status, Is.EqualTo(problem.Status));
            Assert.That(actual.Problem.Detail, Is.EqualTo(problem.Detail));
            Assert.That(actual.Problem.Instance, Is.EqualTo(problem.Instance));
        }
    }
}