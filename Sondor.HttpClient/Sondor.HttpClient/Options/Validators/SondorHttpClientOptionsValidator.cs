using FluentValidation;

namespace Sondor.HttpClient.Options.Validators;

/// <summary>
/// Validator for <see cref="SondorHttpClientOptions"/>.
/// </summary>
public class SondorHttpClientOptionsValidator :
    AbstractValidator<SondorHttpClientOptions>
{
    /// <summary>
    /// Create a new instance of <see cref="SondorHttpClientOptionsValidator"/>.
    /// </summary>
    public SondorHttpClientOptionsValidator()
    {
        RuleFor(prop => prop.Domain)
            .NotNull()
            .NotEmpty();

        RuleFor(prop => prop.Service)
            .NotNull()
            .NotEmpty();

        RuleFor(prop => prop.UriFormat)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Environment)
            .NotNull()
            .NotEmpty()
            .IsInEnum();
    }
}