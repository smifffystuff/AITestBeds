using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion("gpt-3.5-turbo", Environment.GetEnvironmentVariable("OPEN_AI_KEY")!);

var kernel = builder.Build();

var prompt = @"{{$input}}

One line TLDR with the fewest words.";

var summarize = kernel.CreateFunctionFromPrompt(prompt,
    executionSettings: new OpenAIPromptExecutionSettings { MaxTokens = 100 }, functionName: "summarise");

string text1 = @"Newton’s first law states that every object will remain at rest or in uniform motion in a straight line unless compelled to change its state by the action of an external force. This tendency to resist changes in a state of motion is inertia. If all the external forces cancel each other out, then there is no net force acting on the object.  If there is no net force acting on the object, then the object will maintain a constant velocity.";
string text2 = @"The first law of thermodynamics is a formulation of the law of conservation of energy in the context of thermodynamic processes. The law distinguishes two principal forms of energy transfer, heat and thermodynamic work, that modify a thermodynamic system containing a constant amount of matter. The law also defines the internal energy of a system, an extensive property for taking account of the balance of heat and work in the system. Energy cannot be created or destroyed, but it can be transformed from one form to another. In an isolated system the sum of all forms of energy is constant.";

Console.WriteLine(await kernel.InvokeAsync(summarize, new KernelArguments() { ["input"] = text1 }));

Console.WriteLine(await kernel.InvokeAsync(summarize, new KernelArguments() { ["input"] = text2 }));


