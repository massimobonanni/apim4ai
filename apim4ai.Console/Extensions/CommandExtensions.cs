using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace System.CommandLine
{
    internal record ChatClientOptions(
        Option<string> EndpointOption,
        Option<string> ApiKeyOption,
        Option<string> DeploymentNameOption);

    internal static class CommandExtensions
    {
        public static ChatClientOptions AddChatClientOptions(this Command command)
        {
            var endpointOption = new Option<string>("--endpoint", ["-e"])
            {
                Description = "The endpoint of Azure APIM resource.",
                Required = false
            };
            command.Add(endpointOption);

            var apiKeyOption = new Option<string>("--api-key", ["-k"])
            {
                Description = "The subscription key of Azure APIM resource.",
                Required = false
            };
            command.Add(apiKeyOption);

            var deploymentNameOption = new Option<string>("--deployment-name", ["-dn"])
            {
                Description = "The name of the deployment in Azure OpenAI.",
                Required = true
            };
            command.Add(deploymentNameOption);

            return new ChatClientOptions(endpointOption, apiKeyOption, deploymentNameOption);
        }

        public static IChatClient CreateChatClient(this ParseResult parseResult, ChatClientOptions options)
        {
            var endpoint = parseResult.GetValue(options.EndpointOption);
            var apiKey = parseResult.GetValue(options.ApiKeyOption);
            var deploymentName = parseResult.GetValue(options.DeploymentNameOption);

            if (string.IsNullOrEmpty(endpoint))
                throw new InvalidOperationException("Endpoint is required. Provide it via --endpoint option or configuration.");
            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("API Key is required. Provide it via --api-key option or configuration.");
            if (string.IsNullOrEmpty(deploymentName))
                throw new InvalidOperationException("Deployment name is required. Provide it via --deployment-name option.");

            var azureOpenAIClient = new AzureOpenAIClient(
                new Uri(endpoint),
                new AzureKeyCredential(apiKey),
                new AzureOpenAIClientOptions(AzureOpenAIClientOptions.ServiceVersion.V2024_12_01_Preview)
                );

            return azureOpenAIClient.GetChatClient(deploymentName).AsIChatClient();
        }
    }
}
