namespace Oceanus.Domain.Abstractions;
 
 public record SearchResult(string DocumentId, string Text, float Similarity);
 
 public interface IVectorService
 {
     /// <summary>
     /// Индексирует куски документа в векторную БД
     /// </summary>
     Task IndexChunksAsync(string documentId, List<string> chunks, CancellationToken ct = default);
 
     /// <summary>
     /// Ищет релевантные куски по запросу
     /// </summary>
     Task<List<SearchResult>> SearchAsync(string query, int topK = 5, CancellationToken ct = default);
 
     /// <summary>
     /// Удаляет документ из индекса
     /// </summary>
     Task DeleteDocumentAsync(string documentId, CancellationToken ct = default);
 }
