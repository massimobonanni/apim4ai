using apim4ai.Console.Commands.ContentSafety;
using apim4ai.Console.Commands.SemanticCache;
using apim4ai.Console.Commands.TokenLimit;
using apim4ai.Console.Utilities;
using System.CommandLine;


ConsoleUtility.WriteApplicationBanner();

var rootCommand = new RootCommand();
rootCommand.Description = "Apim4AI console";

rootCommand.Add(new TokenLimitCommand());
rootCommand.Add(new SemanticCacheCommand());
rootCommand.Add(new ContentSafetyCommand());

return await rootCommand.Parse(args).InvokeAsync();