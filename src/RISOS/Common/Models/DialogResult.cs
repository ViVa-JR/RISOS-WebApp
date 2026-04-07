using RISOS.Enums;

namespace RISOS.Common.Models;

public class DialogResult
{
    private DialogOutcome Outcome { get; }

    public bool IsSuccess => Outcome == DialogOutcome.Success;
    public bool IsFailure => Outcome == DialogOutcome.Failure;
    public bool IsCancelled => Outcome == DialogOutcome.Cancelled;

    public Error Error
    {
        get => IsFailure ? field : throw new InvalidOperationException("Cannot access Error when the dialog did not fail.");
    }

    protected DialogResult(DialogOutcome outcome, Error error)
    {
        Outcome = outcome;
        Error = error;
    }

    public static DialogResult Success()
        => new(DialogOutcome.Success, Error.None);

    public static DialogResult Failure(Error error)
        => new(DialogOutcome.Failure, error);

    public static DialogResult Cancelled()
        => new(DialogOutcome.Cancelled, Error.None);
}

public class DialogResult<T> : DialogResult
{
    public T Value => IsSuccess ? field! : throw new InvalidOperationException("Cannot access Value when the dialog was not successful.");

    private DialogResult(DialogOutcome outcome, T? value, Error error)
        : base(outcome, error)
    {
        Value = value!;
    }

    public static DialogResult<T> Success(T value)
        => new(DialogOutcome.Success, value, Error.None);

    public new static DialogResult<T> Failure(Error error)
        => new(DialogOutcome.Failure, default, error);

    public new static DialogResult<T> Cancelled()
        => new(DialogOutcome.Cancelled, default, Error.None);
}