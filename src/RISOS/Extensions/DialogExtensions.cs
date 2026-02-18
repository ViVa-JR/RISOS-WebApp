using MudBlazor;
using RISOS.Models;

namespace RISOS.Extensions;

public static class DialogExtensions
{
    public static async Task<DialogResult<T>> GetResultAsync<T>(this IDialogReference dialog)
    {
        var result = await dialog.Result;

        if (result is null)
        {
            return DialogResult<T>.Failure("Dialog result was null.");
        }

        if (result.Canceled)
        {
            return DialogResult<T>.Cancelled();
        }

        if (result.Data is T data)
        {
            return DialogResult<T>.Success(data);
        }

        return DialogResult<T>.Failure($"Invalid return type. Expected {typeof(T).Name}, but got {result.Data?.GetType().Name ?? "null"}.");
    }
}