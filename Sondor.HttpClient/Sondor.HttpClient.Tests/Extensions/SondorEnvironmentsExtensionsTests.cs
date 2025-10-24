using Sondor.HttpClient.Args;
using Sondor.HttpClient.Exceptions;
using Sondor.HttpClient.Extensions;

namespace Sondor.HttpClient.Tests.Extensions;

/// <summary>
/// Tests for <see cref="SondorEnvironmentsExtensions"/>.
/// </summary>
[TestFixture]
public class SondorEnvironmentsExtensionsTests
{
    /// <summary>
    /// Ensures that <see cref="SondorEnvironmentsExtensions.ToUriFragment"/> works correctly.
    /// </summary>
    /// <param name="environment">The environment.</param>
    [TestCaseSource(typeof(SondorEnvironmentArgs))]
    public void ToUriFragment(SondorEnvironments environment)
    {
        // arrange
        if (environment.Equals(SondorEnvironments.Unknown))
        {
            Assert.Throws<UnsupportedSondorEnvironmentException>(() => environment.ToUriFragment());
            
            return;
        }
        
        var expected = environment switch
        {
            SondorEnvironments.Production => "co.uk",
            _ => "dev"
        };

        // act
        var actual = environment.ToUriFragment();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}