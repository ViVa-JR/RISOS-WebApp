using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.JSInterop;

namespace RISOS.Services;

public class ExportService(IJSRuntime js, LocalStorageService localStorageService)
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    private async Task DownloadJsonAsync<T>(T data, string fileName)
    {
        var json = JsonSerializer.Serialize(data, Options);

        await js.InvokeVoidAsync("downloadFile", fileName, json);
    }

    public async Task HandleExportAsync()
    {
        var exportState = await localStorageService.GetExportStateAsync();
        var fileName = $"risos_backup_{DateTime.Now:yyyyMMdd}.json";
        await DownloadJsonAsync(exportState, fileName);
    }
}
