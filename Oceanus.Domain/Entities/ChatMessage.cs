namespace Oceanus.Domain.Entities;
 
 public enum MessageRole
 {
     User,
     Assistant,
     System
 }
 
 public class ChatMessage
 {
     public string Id { get; set; } = Guid.NewGuid().ToString();
     public string ConversationId { get; set; } = string.Empty;
     public MessageRole Role { get; set; }
     public string Content { get; set; } = string.Empty;
     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
     public string? SourceDocuments { get; set; }
 }