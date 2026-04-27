using Oceanus.Application.DTOs;
using Oceanus.Domain.Abstractions;
using Microsoft.Extensions.Logging;


namespace Oceanus.Application.Services;

public interface IChatService
{
    Task<ChatResponse> ProcessMessageAsync(ChatRequest request, CancellationToken ct = default);
}
public class ChatService : IChatService
{
    private readonly ILlmService _llmservice;
    private readonly IVectorService _vectorService;
    private readonly ILogger<ChatService> _logger;

    public ChatService(ILlmService llmservice, IVectorService vectorService, ILogger<ChatService> logger)
    {
        _llmservice = llmservice;
        _vectorService = vectorService;
        _logger = logger;
    }
}