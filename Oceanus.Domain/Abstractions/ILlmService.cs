namespace Oceanus.Domain.Abstractions;
 
 public interface ILlmService
 {
     /// <summary>
     /// Генерирует ответ LLM на основе промпта
     /// </summary>
     Task<string> GenerateResponseAsync(string prompt, CancellationToken ct = default);
 
     /// <summary>
     /// Генерирует ответ LLM с контекстом (для RAG)
     /// </summary>
     Task<string> GenerateResponseWithContextAsync(string prompt, string context, CancellationToken ct = 
default);
 }