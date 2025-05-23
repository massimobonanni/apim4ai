using apim4ai.Console.Binders;
using System.CommandLine;

namespace System.CommandLine
{
    internal static class CommandExtensions
    {
        public static ChatClientBinder CreateChatClientBinder(this Command command)
        {
            var endpointOption = new Option<string>(
                name: "--endpoint",
                description: "The endpoint of Azure APIM resource.")
            {
                IsRequired = false,
            };
            endpointOption.AddAlias("-e");
            command.AddOption(endpointOption);

            var apiKeyOption = new Option<string>(
                name: "--api-key",
                description: "The subscription key of Azure APIM resource.")
            {
                IsRequired = false,
            };
            apiKeyOption.AddAlias("-k");
            command.AddOption(apiKeyOption);

            var deploymentNameOption = new Option<string>(
                name: "--deployment-name",
                description: "The name of the deploymtn in Azure OpenAI.")
            {
                IsRequired = true,
            };
            deploymentNameOption.AddAlias("-dn");
            command.AddOption(deploymentNameOption);

            return new ChatClientBinder(endpointOption, apiKeyOption, deploymentNameOption);
        }
    }
}
