namespace Oceanus.Application.DTOs;
 
 public class ChatResponse
 {
     public string Id { get; set; } = string.Empty;
     public string ConversationId { get; set; } = string.Empty;
     public string Message { get; set; } = string.Empty;
     public List<string> SourceDocuments { get; set; } = new();
     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 }