using Oceanus.Infrastructure.Configuration;
using Oceanus.Domain.Abstractions;
using Oceanus.Infrastructure.Services;
using Oceanus.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
 builder.Services.AddControllers();
 builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen();


var ollamaSettings = new OllamaSettings
{
    BaseUrl = builder.Configuration["Ollama:BaseUrl"] ?? "http://localhost:11434",
    Model = builder.Configuration["Ollama:Model"] ?? "qwen3:8b",
    Timeout = int.Parse(builder.Configuration["Ollama:Timeout"] ?? "300")
};
builder.Services.AddSingleton(ollamaSettings);

// Register Infrastructure Services
 builder.Services.AddHttpClient<OllamaService>();
 builder.Services.AddScoped<ILlmService, OllamaService>();

// Register Application Services
 builder.Services.AddScoped<IChatService, ChatService>();
 
 // Logging
 builder.Services.AddLogging();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();


app.Run();
