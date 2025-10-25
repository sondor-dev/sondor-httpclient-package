using FluentValidation;
using Sondor.HttpClient.Options.Validators;

namespace Sondor.HttpClient.Tests.Examples.Validators;

/// <summary>
/// Validator for <see cref="TestSondorHttpClientOptions"/>.
/// </summary>
public class TestSondorHttpClientOptionsValidator :
    AbstractValidator<TestSondorHttpClientOptions>
{
    /// <summary>
    /// Create a new instance of <see cref="TestSondorHttpClientOptionsValidator"/>.
    /// </summary>
    public TestSondorHttpClientOptionsValidator()
    {
        Include(new SondorHttpClientOptionsValidator());
    }
}