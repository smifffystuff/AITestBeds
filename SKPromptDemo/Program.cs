using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion("gpt-3.5-turbo", Environment.GetEnvironmentVariable("OPEN_AI_KEY")!);

var kernel  = builder.Build();

var funPluginDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Plugins", "FunPlugin");

var funPluginFunctions = kernel.ImportPluginFromPromptDirectory(funPluginDirectoryPath);

var arguments = new KernelArguments() { 
    ["input"] = "A man walking his dog in the park"
};

var result = await kernel.InvokeAsync(funPluginFunctions["Joke"], arguments);

Console.WriteLine(result);