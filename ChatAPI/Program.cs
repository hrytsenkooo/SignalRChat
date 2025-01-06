using ChatAPI.Data;
using ChatAPI.Hubs;
using ChatAPI.Mappings;
using ChatAPI.Repositories.Implementations;
using ChatAPI.Repositories.Interfaces;
using ChatAPI.Services.Implementations;
using ChatAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("https://chatsappui.azurewebsites.net");
    });
});

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Debug);
});

var azureSignalRConnectionString = Environment.GetEnvironmentVariable("AzureSignalR");
if (string.IsNullOrEmpty(azureSignalRConnectionString))
{
    throw new Exception("Azure SignalR connection string is missing.");
}

builder.Services.AddSignalR().AddAzureSignalR(options =>
{
    options.ConnectionString = azureSignalRConnectionString;
});

builder.Services.AddTransient<ISentimentAnalysisService, SentimentAnalysisService>(provider =>
    new SentimentAnalysisService(Environment.GetEnvironmentVariable("ENDPOINT_TEXT"), Environment.GetEnvironmentVariable("API_KEY_TEXT")));
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddAutoMapper(typeof(ChatMappingProfile));


var app = builder.Build();

app.UseCors();

app.MapHub<ChatHub>("/chatHub");

app.Run();
