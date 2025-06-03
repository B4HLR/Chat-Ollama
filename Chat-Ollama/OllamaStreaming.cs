using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;

public class OllamaStreamingClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "http://localhost:11434";

    public OllamaStreamingClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = System.TimeSpan.FromMinutes(10);
    }

    public async Task<string> EnviarPromptStream(string prompt, string modelo = "llama3")
    {
        var body = new
        {
            model = modelo,
            prompt = prompt,
            stream = true
        };

        var content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json"
        );

        var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/api/generate")
        {
            Content = content
        };

        var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();

        using var reader = new StreamReader(stream);

        StringBuilder resultado = new();

        while (!reader.EndOfStream)
        {
            var linha = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(linha))
                continue;

            try
            {
                using JsonDocument doc = JsonDocument.Parse(linha);
                var incremental = doc.RootElement.GetProperty("response").GetString();
                resultado.Append(incremental);
            }
            catch
            {
            
            }
        }

        return resultado.ToString();
    }
}
