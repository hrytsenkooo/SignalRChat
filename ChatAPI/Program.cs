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
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("https://localhost:7049");
    });
});

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

builder.Services.AddSignalR().AddAzureSignalR(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("AzureSignalR");
});
//builder.Services.AddSignalR();
builder.Services.AddTransient<ISentimentAnalysisService, SentimentAnalysisService>(provider =>
    new SentimentAnalysisService(Environment.GetEnvironmentVariable("ENDPOINT_TEXT"), Environment.GetEnvironmentVariable("API_KEY_TEXT")));
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddAutoMapper(typeof(ChatMappingProfile));


var app = builder.Build();

app.UseCors();

app.MapHub<ChatHub>("/chatHub");

app.Run();
