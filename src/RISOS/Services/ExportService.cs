using Microsoft.JSInterop;
using RISOS.Services;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

public class ExportService(IJSRuntime js, LocalStorageService localStorageService)
{
    public async Task DownloadJsonAsync<T>(T data, string fileName)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        var json = JsonSerializer.Serialize(data, options);

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


        if (exportState != null)
        {
            string fileName = $"risos_backup_{DateTime.Now:yyyyMMdd}.json";
            await this.DownloadJsonAsync(exportState, fileName);
        }
        else
        {
            var defaultState = new AppState();
            await this.DownloadJsonAsync(defaultState, "risos_default.json");
        }
    }
}
