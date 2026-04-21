namespace RISOS.Common.Models;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new ArgumentException(@"A successful result cannot have an error", nameof(error));
            case false when error == Error.None:
                throw new ArgumentException(@"A failure result must have an error", nameof(error));
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result<T> Success<T>(T value) => new(true, Error.None, value);
    public static Result<T> Failure<T>(Error error) => new(false, error, default);
}

public class Result<T> : Result
{
    protected internal Result(bool isSuccess, Error error, T? value)
        : base(isSuccess, error)
        => Value = value;

    public T Value => IsSuccess
        ? field!
        : throw new InvalidOperationException($"Cannot access the value of a failure result. Error: {Error}");

    public static implicit operator Result<T>(T value) => Success(value);
}
