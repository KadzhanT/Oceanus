namespace Oceanus.Domain.Entities;
 
 public class Document
 {
     public string Id { get; set; } = Guid.NewGuid().ToString();
     public string FileName { get; set; } = string.Empty;
     public string FilePath { get; set; } = string.Empty;
     public string ContentType { get; set; } = string.Empty;
     public long FileSize { get; set; }
     public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
     public bool IsIndexed { get; set; } = false;
 }