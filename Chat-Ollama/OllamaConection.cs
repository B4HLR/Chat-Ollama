using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class OllamaClient
{
    private readonly HttpClient _httpClient = new();
    private readonly string _baseUrl = "http://localhost:11434";

     public OllamaClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = System.TimeSpan.FromMinutes(60);
    }
    public async Task<string> EnviarPrompt(string prompt, string modelo = "llama3")
    {
        var body = new
        {
            model = modelo,
            prompt = prompt,
            stream = false
        };

        var content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync($"{_baseUrl}/api/generate", content);
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("response").GetString();
    }
}
