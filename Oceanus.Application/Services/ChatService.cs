using Oceanus.Application.DTOs;
using Oceanus.Domain.Abstractions;
using Microsoft.Extensions.Logging;
using Oceanus.Domain.Exceptions;


namespace Oceanus.Application.Services;

public interface IChatService
{
    Task<ChatResponse> ProcessMessageAsync(ChatRequest request, CancellationToken ct = default);
}
public class ChatService : IChatService
{
    private readonly ILlmService _llmService;
    private readonly IVectorService _vectorService;
    private readonly ILogger<ChatService> _logger;

    public ChatService(ILlmService llmservice, IVectorService vectorService, ILogger<ChatService> logger)
    {
        _llmService = llmservice;
        _vectorService = vectorService;
        _logger = logger;
    }

    public async Task<ChatResponse> ProcessMessageAsync(ChatRequest request, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation($"Processing message for conversation: {request.ConversationId}");
            string response;
            var sourceDocuments = new List<string>();

            if (request.UseRag)
            {
                try
                {
                    var searchResults = await _vectorService.SearchAsync(request.Message, topK: 5, ct);
                    sourceDocuments = searchResults.Select(r => r.DocumentId).Distinct().ToList();
                    
                    // Формируем контекст из найденных документов
                    var context = string.Join("\n---\n", searchResults.Select(r=>r.Text));

                    // Генерируем ответ с контекстом
                    response = await _llmService.GenerateResponseWithContextAsync(request.Message, context, ct);
                    _logger.LogInformation($"Generated response with RAG. Found {sourceDocuments.Count} documents");

                } catch (VectorServiceException ex)
                {
                    _logger.LogWarning($"RAG failed, falling back to regular LLM: {ex.Message}");
                     response = await _llmService.GenerateResponseAsync(request.Message, ct);
                }
            }
            else
            {
                // Просто генерируем ответ без контекста
                 response = await _llmService.GenerateResponseAsync(request.Message, ct);
            }

            return new ChatResponse
            {
                Id = Guid.NewGuid().ToString(),
                ConversationId = request.ConversationId,
                Message = response,
                SourceDocuments = sourceDocuments,
                CreatedAt = DateTime.UtcNow

            };
        }
        catch (LlmServiceException ex)
        {
            _logger.LogError($"LLM service error: {ex.Message}");
             throw;
        }
        catch (Exception ex)
         {
             _logger.LogError($"Unexpected error in ChatService: {ex.Message}");
             throw new DomainException($"Error processing message: {ex.Message}", ex);
         }
    }
}