namespace Oceanus.Domain.Abstractions;
 
 public interface IDocumentService
 {
     /// <summary>
     /// Парсит PDF файл в список текстовых строк
     /// </summary>
     Task<List<string>> ParsePdfAsync(string filePath, CancellationToken ct = default);
 
     /// <summary>
     /// Читает текстовый файл
     /// </summary>
     Task<List<string>> ParseTextAsync(string filePath, CancellationToken ct = default);
 
     /// <summary>
     /// Разбивает текст на куски фиксированного размера
     /// </summary>
     Task<List<string>> ChunkTextAsync(string text, int chunkSize = 512, int overlap = 50, CancellationToken ct 
= default);
 }