namespace Oceanus.Application.DTOs;
 
public class SearchResultDto
{
     public string DocumentId { get; set; } = string.Empty;
     public string Text { get; set; } = string.Empty;
     public float Similarity { get; set; }
}