using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion("gpt-3.5-turbo", Environment.GetEnvironmentVariable("OPEN_AI_KEY")!);

var kernel = builder.Build();

