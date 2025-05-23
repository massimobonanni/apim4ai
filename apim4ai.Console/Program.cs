using apim4ai.Console.Commands.ContentSafety;
using apim4ai.Console.Commands.SemanticCache;
using apim4ai.Console.Commands.TokenLimit;
using apim4ai.Console.Utilities;
using System.CommandLine;


ConsoleUtility.WriteApplicationBanner();

var rootCommand = new RootCommand("Apim4AI console");

rootCommand.AddCommand(new TokenLimitCommand());
rootCommand.AddCommand(new SemanticCacheCommand());
rootCommand.AddCommand(new ContentSafetyCommand());

return await rootCommand.InvokeAsync(args);