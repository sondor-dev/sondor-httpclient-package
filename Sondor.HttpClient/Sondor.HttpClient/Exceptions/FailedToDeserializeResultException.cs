using System;
using Sondor.HttpClient.Constants;

namespace Sondor.HttpClient.Exceptions;

/// <summary>
/// Failed to deserialize result exception.
/// </summary>
/// <typeparam name="TResult">The result type.</typeparam>
public class FailedToDeserializeResultException<TResult>() :
    Exception(string.Format(ExceptionConstants.FailedToDeserializeResult, typeof(TResult)));