using System.Text.Json;
using Microsoft.Extensions.Options;
using RISOS.Options;

namespace RISOS.Services;

public class DataUpdateService(IOptions<ApiOptions> options, HttpClient httpClient)
{
    public async Task<string> GetLastUpdateDate()
    {
        var response = await httpClient.GetAsync(options.Value.GitUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to load last update date");
        }

        var json = await response.Content.ReadAsStringAsync();
        
        using var document = JsonDocument.Parse(json);
        var pushedAt = document.RootElement.GetProperty("pushed_at").GetString();
        
        return pushedAt ?? string.Empty;
    }
}
