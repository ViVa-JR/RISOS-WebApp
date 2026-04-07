using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace RISOS.Helpers;

public static class AppStateEncoder
{
    public static string Encode<T>(T state)
    {
        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(state);
        
        using var outputStream = new MemoryStream();
        using (var gzipStream = new GZipStream(outputStream, CompressionLevel.Optimal))
        {
            gzipStream.Write(jsonBytes, 0, jsonBytes.Length);
        }
        
        return Convert.ToBase64String(outputStream.ToArray())
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
    }

    public static T? Decode<T>(string encoded)
    {
        encoded = encoded.Replace("-", "+").Replace("_", "/");
        switch (encoded.Length % 4)
        {
            case 2: encoded += "=="; break;
            case 3: encoded += "="; break;
        }

        var compressedBytes = Convert.FromBase64String(encoded);
        
        using var inputStream = new MemoryStream(compressedBytes);
        using var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);
        using var reader = new StreamReader(gzipStream, Encoding.UTF8);
        
        var json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<T>(json);
    }
}