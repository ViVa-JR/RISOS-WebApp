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

        await js.InvokeVoidAsync("eval", $@"
            (function(name, content) {{
                const blob = new Blob([content], {{type: 'application/json'}});
                const url = URL.createObjectURL(blob);
                const a = document.createElement('a');
                a.href = url;
                a.download = name;
                a.click();
                URL.revokeObjectURL(url);
            }})('{fileName}', `{json.Replace("`", "\\`")}`)
        ");
    }

    public async Task HandleExportAsync()
    {
        var exportState = await localStorageService.GetExportStateAsync();
        var fileName = $"risos_backup_{DateTime.Now:yyyyMMdd}.json";
        await this.DownloadJsonAsync(exportState, fileName);
    }
}