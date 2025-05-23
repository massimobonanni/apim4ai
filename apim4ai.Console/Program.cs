using apim4ai.Console.Utilities;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;


ConsoleUtility.WriteApplicationBanner();

var rootCommand = new RootCommand("Apim4AI console");

//rootCommand.AddCommand(new AddImageToPersonCommand());

return await rootCommand.InvokeAsync(args);



var hostBuilder = Host.CreateApplicationBuilder(args);
hostBuilder.Configuration.AddUserSecrets<Program>();

IChatClient? chatClient = null;

string apiKey = hostBuilder.Configuration["AzureOpenAI:ApiKey"];
string deploymentName = hostBuilder.Configuration["AzureOpenAI:DeploymentName"];
string endpoint = hostBuilder.Configuration["AzureOpenAI:Endpoint"];

var azureOpenAIClient = new AzureOpenAIClient(
    new Uri(endpoint),
    new AzureKeyCredential(apiKey),
    new AzureOpenAIClientOptions(AzureOpenAIClientOptions.ServiceVersion.V2024_12_01_Preview)
    );
chatClient = azureOpenAIClient.GetChatClient(deploymentName).AsIChatClient();

// Setup DI services
hostBuilder.Services.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Trace));
hostBuilder.Services.AddChatClient(chatClient);

//Run the app
var app = hostBuilder.Build();

var client = app.Services.GetRequiredService<IChatClient>();

var response = await client.GetResponseAsync("What are the purpose of the Microsoft.Extensions");
Console.WriteLine(response.Text);
response = await client.GetResponseAsync("What are the purpose of the Microsoft.Extensions");
Console.WriteLine(response.Text);
response = await client.GetResponseAsync("What are the purpose of the Microsoft.Extensions");
Console.WriteLine(response.Text);