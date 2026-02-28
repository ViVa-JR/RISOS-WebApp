using RISOS.Enums;

namespace RISOS.Models;

public class DialogResult<T>
{
    public DialogOutcome Outcome { get; }

    public bool IsSuccess => Outcome == DialogOutcome.Success;
    public bool IsFailure => Outcome == DialogOutcome.Failure;
    public bool IsCancelled => Outcome == DialogOutcome.Cancelled;

    public T Value => IsSuccess 
        ? field! 
        : throw new InvalidOperationException("Cannot access Value when the dialog was not successful.");

    public string Error => IsFailure 
        ? field! 
        : throw new InvalidOperationException("Cannot access Error when the dialog did not fail.");

    private DialogResult(DialogOutcome outcome, T? value, string? error)
    {
        Outcome = outcome;
        Value = value;
        Error = error;
    }
    
    public static DialogResult<T> Success(T value) 
        => new(DialogOutcome.Success, value, null);

    public static DialogResult<T> Failure(string error) 
        => new(DialogOutcome.Failure, default, error);

    public static DialogResult<T> Cancelled() 
        => new(DialogOutcome.Cancelled, default, null);
}