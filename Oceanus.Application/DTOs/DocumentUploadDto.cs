namespace Oceanus.Application.DTOs;
 
 public class DocumentUploadDto
 {
     public string FileName { get; set; } = string.Empty;
     public string ContentType { get; set; } = string.Empty;
     public byte[] FileContent { get; set; } = Array.Empty<byte>();
 }