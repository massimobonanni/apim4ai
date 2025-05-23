using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Binding;

namespace apim4ai.Console.Binders
{
    internal class ChatClientBinder : BinderBase<IChatClient>
    {
        private readonly Option<string> _endpointOption;
        private readonly Option<string> _apiKeyOption;
        private readonly Option<string> _deploymentNameOption;

        public ChatClientBinder(Option<string> endpointOption, 
            Option<string> apiKeyOption, Option<string> deploymentNameOption)
        {
            _endpointOption = endpointOption;
            _apiKeyOption = apiKeyOption;
            _deploymentNameOption = deploymentNameOption;
        }

        protected override IChatClient GetBoundValue(BindingContext bindingContext)
        {
            var endpoint = bindingContext.ParseResult.GetValueForOption(_endpointOption);
            var apiKey = bindingContext.ParseResult.GetValueForOption(_apiKeyOption);
            var deploymentName = bindingContext.ParseResult.GetValueForOption(_deploymentNameOption);

            var azureOpenAIClient = new AzureOpenAIClient(
                new Uri(endpoint),
                new AzureKeyCredential(apiKey),
                new AzureOpenAIClientOptions(AzureOpenAIClientOptions.ServiceVersion.V2024_12_01_Preview)
                );

            var chatClient = azureOpenAIClient.GetChatClient(deploymentName).AsIChatClient();
          
            return chatClient;
        }
    }
}
