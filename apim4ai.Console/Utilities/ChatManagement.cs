using Microsoft.Extensions.AI;

namespace apim4ai.Console.Utilities
{
    internal class ChatManagement
    {
        private readonly IChatClient _chatClient;

        public ChatManagement(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task RunStreamChatAsync(
            Action<ChatResponseUpdate> onResponse,
            Action<Exception> onException)
        {
            while (true)
            {
                ConsoleUtility.Write("You: ", ConsoleColor.Green);
                var input = ConsoleUtility.ReadLine(ConsoleColor.Green);

                if (string.IsNullOrWhiteSpace(input))
                    break;

                ConsoleUtility.Write("LLM: ", ConsoleColor.Yellow);
                try
                {
                    await foreach (var token in _chatClient.GetStreamingResponseAsync(input))
                    {
                        onResponse?.Invoke(token);
                    }
                    ConsoleUtility.WriteLine(string.Empty, ConsoleColor.Yellow);
                }
                catch (Exception ex)
                {
                    onException?.Invoke(ex);
                }
            }
        }

        public async Task RunChatAsync(
            Action<ChatResponse> onResponse,
            Action<Exception> onException)
        {
            while (true)
            {
                ConsoleUtility.Write("You: ", ConsoleColor.Green);
                var input = ConsoleUtility.ReadLine(ConsoleColor.Green); ;

                if (string.IsNullOrWhiteSpace(input))
                    break;

                ConsoleUtility.Write("LLM: ", ConsoleColor.Yellow);
                try
                {
                    var response = await _chatClient.GetResponseAsync(input);
                    onResponse?.Invoke(response);
                    ConsoleUtility.WriteLine(string.Empty, ConsoleColor.Yellow);
                }
                catch (Exception ex)
                {
                    onException?.Invoke(ex);
                }
            }
        }
    }
}