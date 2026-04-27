 namespace Oceanus.Domain.Entities;
 
 public class VectorChunk
 {
     public string Id { get; set; } = Guid.NewGuid().ToString();
     public string DocumentId { get; set; } = string.Empty;
     public string Text { get; set; } = string.Empty;
     public int ChunkIndex { get; set; }
     public List<float> Embedding { get; set; } = new();
     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 }