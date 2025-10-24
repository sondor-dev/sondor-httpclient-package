using Sondor.HttpClient.Options;
using Sondor.HttpClient.Constants;
using Sondor.HttpClient.Extensions;

namespace Sondor.HttpClient.Tests.Options;

/// <summary>
/// Tests for <see cref="SondorHttpClientOptions"/>.
/// </summary>
[TestFixture]
public class SondorHttpClientOptionsTests
{
    /// <summary>
    /// Ensures the <see cref="SondorHttpClientOptions"/> constructor sets properties correctly.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        const string domain = "domain";
        const string service = "service";
        const SondorEnvironments environment = SondorEnvironments.Production;
        const string uriFormat = "uri-format";
        const bool useHttps = false;
        const string userAgent = "test-agent";
        
        // act
        var options = new SondorHttpClientOptions(userAgent)
        {
            UriFormat = uriFormat,
            Domain = domain,
            Environment = environment,
            Service = service,
            UseHttps = useHttps
        };
        
        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(options.Domain, Is.EqualTo(domain));
            Assert.That(options.Service, Is.EqualTo(service));
            Assert.That(options.UseHttps, Is.EqualTo(useHttps));
            Assert.That(options.Environment, Is.EqualTo(environment));
            Assert.That(options.UriFormat, Is.EqualTo(uriFormat));
            Assert.That(options.UserAgent, Is.EqualTo(userAgent));
        }
    }
    
    /// <summary>
    /// Ensures that <see cref="SondorHttpClientOptions.Uri"/> returns the correct URI.
    /// </summary>
    [Test]
    public void Uri_ReturnsCorrectUri()
    {
        // arrange
        const string service = "service";
        const string domain = "domain";
        const SondorEnvironments environment = SondorEnvironments.Production;
        const bool useHttps = true;
        const string uriFormat = OptionsConstants.UriFormat;
        const string protocol = "https";
        const string userAgent = "test-agent";
        var options = new SondorHttpClientOptions(userAgent)
        {
            UriFormat = uriFormat,
            Domain = domain,
            Environment = environment,
            Service = service,
            UseHttps = useHttps
        };
        var expected = string.Format(uriFormat, protocol, service, domain, environment.ToUriFragment()) + "/";

        // act

        // act
        var uri = options.Uri();

        // assert
        Assert.That(uri.ToString(), Is.EqualTo(expected));
    }
}