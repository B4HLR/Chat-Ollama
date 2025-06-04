using Microsoft.AspNetCore.Mvc;

public class ExplicadorDeTermos
{
    private readonly OllamaClient _ollama;

    public ExplicadorDeTermos(OllamaClient cliente)
    {
        _ollama = cliente;
    }

    public async Task<string> Explica(string termo)
    {
        string prompt = $"Explique o termo '{termo}' de forma simples e objetiva, se limite a 100 palavras para explicar.";
        return await _ollama.EnviarPrompt(prompt);
    }
}
public class ResumidorDeDocumentos
{
    private readonly OllamaClient _ollama;

    public ResumidorDeDocumentos(OllamaClient cliente)
    {
        _ollama = cliente;
    }

    public async Task<string> Resumir(string texto)
    {
        string prompt = $"Resuma em português brasileiro o seguinte conteúdo de forma clara e concisa em até 200 palavras:\n\n{texto}";
        return await _ollama.EnviarPrompt(prompt);
    }
}

public class AnaliseDeSentimento
{
    private readonly OllamaClient _ollama;

    public AnaliseDeSentimento(OllamaClient cliente)
    {
        _ollama = cliente;
    }

    public async Task<string> Sentido(string duvida)
    {
        string prompt = $"Analise o sentimento do texto e responda apenas com “positiva”, “negativa” ou “neutra”:'{duvida}'";
        return await _ollama.EnviarPrompt(prompt);
    }
}

