using FluentValidation.TestHelper;
using Sondor.HttpClient.Args;
using Sondor.HttpClient.Options;
using Sondor.HttpClient.Options.Validators;
using Sondor.Tests.Args;

namespace Sondor.HttpClient.Tests.Options.Validators;

/// <summary>
/// Tests for <see cref="SondorHttpClientOptionsValidator"/>.
/// </summary>
[TestFixture]
public class SondorHttpClientOptionsValidatorTests
{
    /// <summary>
    /// The validator.
    /// </summary>
    private readonly SondorHttpClientOptionsValidator _validator = new ();

    /// <summary>
    /// Ensures that the <see cref="SondorHttpClientOptions.Domain"/> is correctly validated.
    /// </summary>
    /// <param name="domain">The domain.</param>
    [TestCaseSource(typeof(StringArgs))]
    public void Validate_Domain(string? domain)
    {
        // arrange
        var options = new SondorHttpClientOptions("test-agent")
        {
            Domain = domain!
        };
        
        // act
        var result = _validator.TestValidate(options);
        
        // assert
        if (string.IsNullOrWhiteSpace(domain))
        {
            result.ShouldHaveValidationErrorFor(prop => prop.Domain);
            
            return;
        }

        result.ShouldNotHaveValidationErrorFor(prop => prop.Domain);
    }

    /// <summary>
    /// Ensures that the <see cref="SondorHttpClientOptions.Environment"/> is correctly validated.
    /// </summary>
    /// <param name="environment">The environment.</param>
    [TestCaseSource(typeof(SondorEnvironmentArgs))]
    public void Validate_Environment(SondorEnvironments environment)
    {
        // arrange
        var options = new SondorHttpClientOptions("test-agent")
        {
            Environment = environment
        };

        // act
        var result = _validator.TestValidate(options);

        // assert
        if (environment is SondorEnvironments.Unknown)
        {
            result.ShouldHaveValidationErrorFor(prop => prop.Environment);

            return;
        }

        result.ShouldNotHaveValidationErrorFor(prop => prop.Environment);
    }

    /// <summary>
    /// Ensures that the <see cref="SondorHttpClientOptions.Service"/> is correctly validated.
    /// </summary>
    /// <param name="service">The service.</param>
    [TestCaseSource(typeof(StringArgs))]
    public void Validate_Service(string? service)
    {
        // arrange
        var options = new SondorHttpClientOptions("test-agent")
        {
            Service = service!
        };

        // act
        var result = _validator.TestValidate(options);

        // assert
        if (string.IsNullOrWhiteSpace(service))
        {
            result.ShouldHaveValidationErrorFor(prop => prop.Service);

            return;
        }

        result.ShouldNotHaveValidationErrorFor(prop => prop.Service);
    }
}