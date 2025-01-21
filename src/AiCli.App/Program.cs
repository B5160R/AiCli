using System.Net.Http.Headers;
using AiCli;
using AiCli.Interfaces;
using AiCli.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
var config = builder.Build();

var services = new ServiceCollection();

services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});
services.AddMemoryCache();
services.AddHttpClient<IMistralApiCaller, MistralApiCaller>(client =>
{
    client.BaseAddress = new Uri("https://api.mistral.ai/v1/chat/completions");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Bearer",
        config["ApiKeys:MistralAiApiKey"]
    );
});
services.AddHttpClient<IAiStudioApiCaller, AiStudioApiCaller>(client =>
{
    client.BaseAddress = new Uri(
        $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={config["ApiKeys:AiStudioApiKey"]}"
    );
    // client.BaseAddress = new Uri(
    //     "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?"
    // );
    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    //     "Bearer",
    //     config["ApiKeys:AiStudioApiKey"]
    // );
});
services.AddScoped<IRateLimiter, RateLimiter>();
services.AddScoped<IConsoleGui, ConsoleGui>();

IServiceProvider serviceProvider = services.BuildServiceProvider();
var consoleGui = serviceProvider.GetService<IConsoleGui>() ?? throw new ArgumentNullException();

await consoleGui.Run();
