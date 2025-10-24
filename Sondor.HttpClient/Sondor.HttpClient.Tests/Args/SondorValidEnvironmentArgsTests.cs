using Sondor.HttpClient.Args;

namespace Sondor.HttpClient.Tests.Args;

/// <summary>
/// Tests for <see cref="SondorValidEnvironmentArgs"/>.
/// </summary>
[TestFixture]
public class SondorValidEnvironmentArgsTests
{
    /// <summary>
    /// Ensures that <see cref="SondorValidEnvironmentArgs"/> only contains valid environments.
    /// </summary>
    [Test]
    public void SondorValidEnvironmentArgs()
    {
        // arrange
        var expected = new[]
        {
            SondorEnvironments.Development,
            SondorEnvironments.Production
        };

        // act
        var environments = new SondorValidEnvironmentArgs();

        // assert
        Assert.That(environments, Is.EqualTo(expected));
    }
}