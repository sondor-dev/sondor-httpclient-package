using System.Text.Json;
using Sondor.HttpClient.Extensions;
using Sondor.Tests.Args;

namespace Sondor.HttpClient.Tests.Extensions;

/// <summary>
/// Tests for <see cref="StringExtensions"/>.
/// </summary>
[TestFixture]
public class StringExtensionsTests
{
    /// <summary>
    /// Ensures that <see cref="StringExtensions.IsValidJson"/> correctly identifies invalid JSON strings.
    /// </summary>
    /// <param name="value">The value.</param>
    [TestCaseSource(typeof(StringArgs))]
    public void IsValidJson_invalid(string? value)
    {
        // arrange
        const bool expected = false;

        // act
        var actual = value.IsValidJson();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures that <see cref="StringExtensions.IsValidJson"/> correctly identified valid JSON object strings.
    /// </summary>
    [Test]
    public void IsValidJson_object()
    {
        // arrange
        const bool expected = true;
        var instance = new { Name = "test" };
        var json = JsonSerializer.Serialize(instance);

        // act
        var actual = json.IsValidJson();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures that <see cref="StringExtensions.IsValidJson"/> correctly identified valid JSON array strings.
    /// </summary>
    [Test]
    public void IsValidJson_array()
    {
        // arrange
        const bool expected = true;
        dynamic[] instance = [new { Name = "test" }];
        var json = JsonSerializer.Serialize(instance);

        // act
        var actual = json.IsValidJson();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
