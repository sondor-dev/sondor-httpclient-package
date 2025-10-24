using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Sondor.HttpClient.Extensions;
using Sondor.HttpClient.Tests.Args;

namespace Sondor.HttpClient.Tests.Extensions;

/// <summary>
/// Tests for <see cref="HostEnvironmentExtensions"/>.
/// </summary>
[TestFixture]
public class HostEnvironmentExtensionsTests
{
    /// <summary>
    /// Ensures that <see cref="HostEnvironmentExtensions.ToSondorEnvironment"/> works as expected.
    /// </summary>
    /// <param name="environment">The environment.</param>
    [TestCaseSource(typeof(MicrosoftEnvironmentsArgs))]
    public void ToSondorEnvironment(string environment)
    {
        // arrange
        IHostEnvironment hostEnvironment = new HostingEnvironment
        {
            EnvironmentName = environment
        };

        SondorEnvironments expected;

        if (environment.Equals(Environments.Development))
        {
            expected = SondorEnvironments.Development;
        }
        else if (environment.Equals(Environments.Production))
        {
            expected = SondorEnvironments.Production;
        }
        else
        {
            expected = SondorEnvironments.Unknown;
        }

        // act
        var actual = hostEnvironment.ToSondorEnvironment();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}