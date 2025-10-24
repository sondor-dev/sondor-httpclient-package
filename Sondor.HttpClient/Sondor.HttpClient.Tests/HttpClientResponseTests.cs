using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sondor.ProblemResults;
using Sondor.Tests.Args;

namespace Sondor.HttpClient.Tests;

/// <summary>
/// Tests for <see cref="HttpClientResponse{TResult}"/>.
/// </summary>
[TestFixture]
public class HttpClientResponseTests
{
    /// <summary>
    /// Ensures the <see cref="HttpClientResponse{TValue}"/> constructor sets properties correctly.
    /// </summary>
    [Test]
    public void ConstructorTyped_Result()
    {
        // arrange
        const int result = 42;

        // act
        var actual = new HttpClientResponse<int>(result);

        // assert
        Assert.That(actual.Value, Is.EqualTo(result));
    }

    /// <summary>
    /// Ensures the <see cref="HttpClientResponse{TValue}"/> constructor sets properties correctly.
    /// </summary>
    [Test]
    public void ConstructorTyped_Problem()
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
        var actual = new HttpClientResponse<int>(problem);

        // assert
        Assert.That(actual.Problem, Is.EqualTo(problem));
    }

    /// <summary>
    /// Ensures the <see cref="HttpClientResponse{TValue}"/> constructor sets properties correctly.
    /// </summary>
    [Test]
    public void Constructor_Result()
    {
        // act
        var actual = new HttpClientResponse();

        // assert
        Assert.That(actual.Problem, Is.Null);
    }

    /// <summary>
    /// Ensures the <see cref="HttpClientResponse{TValue}.ToString"/> returns the correct string.
    /// </summary>
    [TestCaseSource(typeof(IntArgs))]
    public void ToString_Typed(int value)
    {
        // arrange
        var expected = value.ToString();
        var response = new HttpClientResponse<int>(value);

        // act
        var actual = response.ToString();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures the <see cref="HttpClientResponse{TValue}.ToString"/> returns the correct string.
    /// </summary>
    [TestCaseSource(typeof(StringArgs))]
    public void ToString_Typed_String(string? value)
    {
        // arrange
        var expected = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        var response = new HttpClientResponse<string>(value!);

        // act
        var actual = response.ToString();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures the <see cref="HttpClientResponse{TValue}.ToString"/> returns the correct string.
    /// </summary>
    [Test]
    public void ToString_NonTyped()
    {
        // arrange
        var expected = string.Empty;
        var response = new HttpClientResponse();

        // act
        var actual = response.ToString();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures the <see cref="HttpClientResponse{TValue}.ToString"/> returns the correct string.
    /// </summary>
    [TestCaseSource(typeof(BoolArgs))]
    public void ToString_Problem(bool typed)
    {
        // arrange
        const string type = "type";
        const string title = "title";
        const string detail = "detail";
        const int status = 500;
        const string instance = "instance";

        var problem = new SondorProblemDetails
        {
            Type = type,
            Title = title,
            Detail = detail,
            Status = status,
            Instance = instance
        };
        var expected = JsonConvert.SerializeObject(problem, Formatting.Indented);

        // act

        var actual = typed ?
            new HttpClientResponse<int>(problem).ToString() :
            new HttpClientResponse(problem).ToString();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}