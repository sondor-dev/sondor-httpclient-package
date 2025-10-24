using Sondor.HttpClient.Constants;
using Sondor.HttpClient.Exceptions;

namespace Sondor.HttpClient.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="UnsupportedSondorEnvironmentException"/>.
/// </summary>
[TestFixture]
public class UnsupportedSondorEnvironmentExceptionTests
{
    /// <summary>
    /// Ensures that the <see cref="UnsupportedSondorEnvironmentException"/> constructor enum overload, sets the correct message.
    /// </summary>
    [Test]
    public void Constructor_Enum()
    {
        // arrange
        const SondorEnvironments environment = SondorEnvironments.Development;
        var expected = string.Format(ExceptionConstants.UnsupportedEnvironmentException, environment);
        
        // act
        var exception = new UnsupportedSondorEnvironmentException(environment);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.Message, Is.EqualTo(expected));
            Assert.That(exception.Environment, Is.EqualTo(environment.ToString()));
        });
    }

    /// <summary>
    /// Ensures that the <see cref="UnsupportedSondorEnvironmentException"/> constructor string overload, sets the correct message.
    /// </summary>
    [Test]
    public void Constructor_String()
    {
        // arrange
        const string environment = nameof(SondorEnvironments.Development);
        var expected = string.Format(ExceptionConstants.UnsupportedEnvironmentException, environment);

        // act
        var exception = new UnsupportedSondorEnvironmentException(environment);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.Message, Is.EqualTo(expected));
            Assert.That(exception.Environment, Is.EqualTo(environment));
        });
    }
}