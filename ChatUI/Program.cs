using ChatUI;
using ChatUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://chatappweb-g6gjcnh5aeg5f5bd.northeurope-01.azurewebsites.net") });
builder.Services.AddScoped<IChatService, ChatService>();

await builder.Build().RunAsync();