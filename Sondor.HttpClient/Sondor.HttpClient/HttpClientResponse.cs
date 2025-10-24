using Newtonsoft.Json;
using Sondor.ProblemResults;

namespace Sondor.HttpClient;

/// <summary>
/// Http client response.
/// </summary>
public readonly struct HttpClientResponse
{
    /// <summary>
    /// The problem.
    /// </summary>
    public SondorProblemDetails? Problem { get; }

    /// <summary>
    /// Create a new instance of <see cref="HttpClientResponse"/> with a problem.
    /// </summary>
    /// <param name="problem">The problem.</param>
    public HttpClientResponse(SondorProblemDetails problem)
    {
        Problem = problem;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Problem is not null ?
            JsonConvert.SerializeObject(Problem, Formatting.Indented) :
            string.Empty;
    }
}

/// <summary>
/// Http client response.
/// </summary>
/// <typeparam name="TValue">The result value type.</typeparam>
public readonly struct HttpClientResponse<TValue>
{
    /// <summary>
    /// The value.
    /// </summary>
    public TValue? Value { get; }

    /// <summary>
    /// The problem.
    /// </summary>
    public SondorProblemDetails? Problem { get; }

    /// <summary>
    /// Create a new instance of <see cref="HttpClientResponse{TValue}"/> with a problem.
    /// </summary>
    /// <param name="value">The value.</param>
    public HttpClientResponse(TValue value)
    {
        Value = value;
    }

    /// <summary>
    /// Create a new instance of <see cref="HttpClientResponse{TValue}"/> with a problem.
    /// </summary>
    /// <param name="problem">The problem.</param>
    public HttpClientResponse(SondorProblemDetails problem)
    {
        Problem = problem;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (Problem is not null)
        {
            return JsonConvert.SerializeObject(Problem, Formatting.Indented);
        }

        if (Value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
        {
            return string.Empty;
        }
        
        if (Value is not null)
        {
            return Value.ToString() ?? string.Empty;
        }

        return string.Empty;
    }
}