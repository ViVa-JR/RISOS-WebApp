using System.Text.Json;
using MudBlazor;
using RISOS.Common.Models;

namespace RISOS.Services;

public class ApiService(HttpClient httpClient, ISnackbar snackbar)
{
    public async Task<Result<T>> TryRequestAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> handler)
    {
        try
        {
            var httpResponse = await handler(httpClient);
            var json = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json) ?? Result.Failure<T>(Error.ParseError);
        }
        catch (HttpRequestException e)
        {
            snackbar.Add("Failed to load data", Severity.Error);
            return Result.Failure<T>(new Error(e.HttpRequestError.ToString(), e));
        }
        catch (Exception e)
        {
            snackbar.Add("Failed to load data", Severity.Error);
            return Result.Failure<T>(new Error("Unknown exception", e));
        }
    }

    public async Task<Result> TryRequestAsync(Func<HttpClient, Task> handler)
    {
        try
        {
            await handler(httpClient);
            return Result.Success();
        }
        catch (HttpRequestException e)
        {
            snackbar.Add("Failed to load data", Severity.Error);
            return Result.Failure(new Error(e.HttpRequestError.ToString(), e));
        }
        catch (Exception e)
        {
            snackbar.Add("Failed to load data", Severity.Error);
            return Result.Failure(new Error("Unknown exception", e));
        }
    }
}