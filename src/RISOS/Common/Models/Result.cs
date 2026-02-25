namespace RISOS.Common.Models;

using System;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    protected Result(bool isSuccess, string error)
    {
        switch (isSuccess)
        {
            case true when !string.IsNullOrWhiteSpace(error):
                throw new ArgumentException("A successful result cannot have an error message.", nameof(error));
            case false when string.IsNullOrWhiteSpace(error):
                throw new ArgumentException("A failure result must have an error message.", nameof(error));
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public static Result Success() => new Result(true, string.Empty);
    public static Result Failure(string error) => new Result(false, error);

    public static Result<T> Success<T>(T value) => new Result<T>(true, string.Empty, value);
    public static Result<T> Failure<T>(string error) => new Result<T>(false, error, default);
}

public class Result<T> : Result
{
    public T Value => IsSuccess
        ? field!
        : throw new InvalidOperationException($"Cannot access the value of a failure result. Error: {Error}");

    protected internal Result(bool isSuccess, string error, T? value)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}