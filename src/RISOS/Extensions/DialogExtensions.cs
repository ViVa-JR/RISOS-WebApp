using MudBlazor;
using RISOS.Common.Models;
using DialogResult = RISOS.Common.Models.DialogResult;

namespace RISOS.Extensions;

public static class DialogExtensions
{
    extension(IDialogReference dialog)
    {
        public async Task<DialogResult<T>> GetResultAsync<T>()
        {
            var result = await dialog.Result;

            if (result is null)
            {
                return DialogResult<T>.Failure(Error.NullValue);
            }

            if (result.Canceled)
            {
                return DialogResult<T>.Cancelled();
            }

            if (result.Data is T data)
            {
                return DialogResult<T>.Success(data);
            }

            return DialogResult<T>.Failure(new Error($"Invalid return type. Expected {typeof(T).Name}, but got {result.Data?.GetType().Name ?? "null"}."));
        }

        public async Task<DialogResult> GetResultAsync()
        {
            var result = await dialog.Result;

            if (result is null)
            {
                return DialogResult.Failure(Error.NullValue);
            }

            return result.Canceled
                ? DialogResult.Cancelled()
                : DialogResult.Success();
        }
    }
}