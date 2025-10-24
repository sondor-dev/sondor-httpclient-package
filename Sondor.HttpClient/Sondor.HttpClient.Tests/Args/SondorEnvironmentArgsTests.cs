using Sondor.HttpClient.Args;

namespace Sondor.HttpClient.Tests.Args;

/// <summary>
/// Tests for <see cref="SondorEnvironmentArgs"/>.
/// </summary>
[TestFixture]
public class SondorEnvironmentArgsTests
{
    /// <summary>
    /// Ensures the arguments contains all environments.
    /// </summary>
    [Test]
    public void EnsureAllExist()
    {
        // arrange
        var environments = Enum.GetValues<SondorEnvironments>();
        
        // act
        var args = new SondorEnvironmentArgs();

        // assert
        Assert.Multiple(() =>
        {
            foreach (SondorEnvironments environment in args)
            {
                Assert.That(environments.Contains(environment), Is.True);
            }
        });
    }
}