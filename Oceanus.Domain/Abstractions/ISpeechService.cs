namespace Oceanus.Domain.Abstractions;
 
 public interface ISpeechService
 {
     /// <summary>
     /// Транскрибирует аудио в текст (STT)
     /// </summary>
     Task<string> TranscribeAudioAsync(byte[] audioData, CancellationToken ct = default);
 
     /// <summary>
     /// Синтезирует речь из текста (TTS)
     /// </summary>
     Task<byte[]> SynthesizeSpeechAsync(string text, CancellationToken ct = default);
 }
