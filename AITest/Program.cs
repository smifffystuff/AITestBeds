var builder = Kernel.CreateBuilder();

builder.Services.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Trace));

Kernel kernel = builder
    .AddOpenAIChatCompletion("gpt-3.5-turbo", Environment.GetEnvironmentVariable("OPEN_AI_KEY")!)
    .Build();

kernel.ImportPluginFromType<Demographics>();

var settings = new OpenAIPromptExecutionSettings
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};
var chatService = kernel.GetRequiredService<IChatCompletionService>();
ChatHistory chat = new();

while (true)
{
    Console.Write("Q: ");
    chat.AddUserMessage(Console.ReadLine());

    var r = await chatService.GetChatMessageContentAsync(chat, settings, kernel);
    Console.WriteLine(r);
    chat.Add(r);
}


