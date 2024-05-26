using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion("gpt-3.5-turbo", Environment.GetEnvironmentVariable("OPEN_AI_KEY")!);

var kernel = builder.Build();

var prompt = @"
You are a Chatbot and a user can have a conversation with you about any topic.
It can give explicit information or say 'I don't know' if it doesn't have an answer.

{{$history}}
User: {{$userInput}}
ChatBot:";

var chatFunction = kernel.CreateFunctionFromPrompt(prompt,
    executionSettings: new OpenAIPromptExecutionSettings
    {
        MaxTokens = 2000,
        Temperature = 0.7,
        TopP = 0.5
    });

var arguments = new KernelArguments();
var history = "";

Console.WriteLine("Hi, I am a ChatBot ask me anything!");

while (true)
{
    var userInput = Console.ReadLine();
    if (userInput.Trim().ToLower() == "exit")
    {
        break;
    }
    Func<string, Task> Chat = async (string input) =>
    {
        arguments["userInput"] = userInput;

        var botAnswer = await chatFunction.InvokeAsync(kernel, arguments);

        var result = $"\nUser: {userInput}\nAI: {botAnswer}\n";
        history += result;
        arguments["history"] = history;

        Console.WriteLine(botAnswer);
    };

    await Chat(userInput!);
    
}