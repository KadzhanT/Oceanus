 namespace Oceanus.Infrastructure.Configuration;
 
 public class OllamaSettings
 {
     public string BaseUrl { get; set; } = "http://localhost:11434";
     public string Model { get; set; } = "qwen3:8b";
     public int Timeout { get; set; } = 300; // 5 минут для долгих запросов
 }