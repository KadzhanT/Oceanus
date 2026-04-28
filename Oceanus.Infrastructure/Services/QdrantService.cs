using Oceanus.Domain.Abstractions;
 using Oceanus.Infrastructure.Configuration;
 using Microsoft.Extensions.Logging;
 
 namespace Oceanus.Infrastructure.Services;
 
 public class QdrantService : IVectorService
 {
     private readonly HttpClient _httpClient;
     private readonly QdrantSettings _settings;
     private readonly ILogger<QdrantService> _logger;
 
     public QdrantService(HttpClient httpClient, QdrantSettings settings, ILogger<QdrantService> logger)
     {
         _httpClient = httpClient;
         _settings = settings;
         _logger = logger;
         _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
     }
 
     public async Task IndexChunksAsync(string documentId, List<string> chunks, CancellationToken ct = default)
     {
         _logger.LogInformation($"Indexing {chunks.Count} chunks for document {documentId}");
         // TODO: Реализуем позже
         await Task.CompletedTask;
     }
 
     public async Task<List<SearchResult>> SearchAsync(string query, int topK = 5, CancellationToken ct = 
default)
     {
         _logger.LogInformation($"Searching: {query}");
         // TODO: Реализуем позже
         return new List<SearchResult>();
     }
 
     public async Task DeleteDocumentAsync(string documentId, CancellationToken ct = default)
     {
         _logger.LogInformation($"Deleting document {documentId}");
         // TODO: Реализуем позже
         await Task.CompletedTask;
     }
 }
