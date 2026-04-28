using Microsoft.AspNetCore.Mvc;
using Oceanus.Application.Services;
using Oceanus.Application.DTOs;
using System.Security.Cryptography.X509Certificates;
namespace Oceanus.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly ILogger<ChatController> _logger;

    public ChatController(IChatService chatService, ILogger<ChatController> logger)
    {
        _chatService = chatService;
        _logger = logger;
    }
    /// <summary>
     /// Обрабатывает сообщение пользователя и возвращает ответ ИИ
     /// </summary>
     /// <param name="request">ChatRequest содержащий сообщение</param>
     /// <param name="ct">Токен отмены</param>
     /// <returns>ChatResponse с ответом ИИ</returns>
     
     [HttpPost("mesage")]
     public async Task<ActionResult<ChatResponse>> SendMessage(
        [FromBody] ChatRequest request,
        CancellationToken ct
     )
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Message cannot be empty");
            }

            var response = await _chatService.ProcessMessageAsync(request, ct);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in ChatController: {ex.Message}");
             return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
         
        
    }
    /// <summary>
    /// Health check для проверки доступности сервиса
    /// </summary>
    [HttpGet("health")]
     public ActionResult<object> HealthCheck()
     {
         return Ok(new { status = "ok", timestamp = DateTime.UtcNow });
     }
     
}
