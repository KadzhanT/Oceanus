using Oceanus.Domain.Abstractions;
using Oceanus.Domain.Exceptions;
using Oceanus.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Oceanus.Infrastructure.Services;

public class OllamaService : ILlmService
{
    private readonly HttpClient _httpClient;
    private readonly OllamaSettings _settings;
    private readonly ILogger<OllamaService> _logger;

    public OllamaService(HttpClient httpClient, OllamaSettings settings, ILogger<OllamaService> logger)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
        _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
    }

    public async Task<string> GenerateResponseAsync(string prompt, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation($"Generating response with model: {_settings.Model}");
             var request = new OllamaGenerateRequest
             {
                 Model = _settings.Model,
                 Prompt = prompt,
                 Stream = false
             };

             var content = JsonContent.Create(request);
             var response = await _httpClient.PostAsync("/api/generate", content, ct);
             response.EnsureSuccessStatusCode();

             var jsonResponse = await response.Content.ReadAsStringAsync(ct);
             var ollamaResponse = System.Text.Json.JsonSerializer.Deserialize<OllamaGenerateResponse>(jsonResponse);
            
             if (ollamaResponse?.Response == null)
                 throw new LlmServiceException("Empty response from Ollama");


            _logger.LogInformation("Response generated successfully");
             return ollamaResponse.Response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"HTTP error connecting to Ollama: {ex.Message}");
             throw new LlmServiceException($"Failed to connect to Ollama: {ex.Message}", ex);
        }catch (Exception ex)
        {
            _logger.LogError($"Error generating response: {ex.Message}");
             throw new LlmServiceException($"Error generating response: {ex.Message}", ex);
        }
    }
    public async Task<String> GenerateResponseWithContextAsync(string prompt, string context, CancellationToken ct = default)
    {
        // Комбинируем промпт с контекстом для RAG
        var enhancedPrompt = $"Context:\n{context}\n\nQuestion:\n{prompt}";
        return await GenerateResponseAsync(enhancedPrompt, ct);
    }
    // Внутренние классы для сериализации
     private class OllamaGenerateRequest
     {
         public required string Model { get; set; }
         public required string Prompt { get; set; }
         public bool Stream { get; set; }
     }
     private class OllamaGenerateResponse
     {
         public required string Model { get; set; }
         public DateTime CreatedAt { get; set; }
         public required string Response { get; set; }
         public bool Done { get; set; }
     }
}