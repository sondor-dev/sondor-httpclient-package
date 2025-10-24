using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sondor.Errors.Extensions;
using Sondor.HttpClient.Exceptions;

namespace Sondor.HttpClient.Binders;

/// <summary>
/// Request binder.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="RequestBinder{TRequest, TValidator}"/>.
/// </remarks>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TValidator">The validator type.</typeparam>
[ExcludeFromCodeCoverage]
public abstract class RequestBinder<TRequest, TValidator> : IModelBinder
    where TRequest : class
    where TValidator : IValidator<TRequest>
{
    /// <summary>
    /// The validator for the request type.
    /// </summary>
    protected abstract TValidator Validator { get; }

    /// <inheritdoc />
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var resource = await bindingContext.HttpContext.Request.ReadFromJsonAsync<TRequest>();

        if (resource is null ||
            resource.IsEmpty())
        {
            throw new ResourceRequestInvalidException(bindingContext.HttpContext.Request.Method,
                bindingContext.HttpContext.Request.Path);
        }

        var validation = await Validator.ValidateAsync(resource);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }

        bindingContext.Result = ModelBindingResult.Success(resource);
    }
}