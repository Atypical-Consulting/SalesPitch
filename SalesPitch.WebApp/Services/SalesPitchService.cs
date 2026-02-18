using Betalgo.Ranul.OpenAI.Interfaces;
using Betalgo.Ranul.OpenAI.ObjectModels;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using Betalgo.Ranul.OpenAI.ObjectModels.ResponseModels;
using SalesPitch.WebApp.Models;

namespace SalesPitch.WebApp.Services;

public class SalesPitchService : ISalesPitchService
{
    private readonly IOpenAIService _openAiService;

    public SalesPitchService(IOpenAIService openAiService)
    {
        _openAiService = openAiService;
    }

    public async Task<string> GenerateSalesPitchAsync(SalesPitchRequest request, CancellationToken cancellationToken = default)
    {
        var systemMessage = GetSystemMessage();
        var userPrompt = GetUserPrompt(request);

        var chatRequest = new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(systemMessage),
                ChatMessage.FromUser(userPrompt)
            }
        };
        
        var completionResult = await _openAiService.ChatCompletion.CreateCompletion(chatRequest, "gpt-4o");

        if (!completionResult.Successful)
        {
            throw new InvalidOperationException($"OpenAI API error: {completionResult.Error?.Message}");
        }

        return completionResult.Choices.FirstOrDefault()?.Message.Content ?? "No response generated.";
    }

    public async Task<IAsyncEnumerable<string>> GenerateSalesPitchStreamAsync(SalesPitchRequest request, CancellationToken cancellationToken = default)
    {
        var systemMessage = GetSystemMessage();
        var userPrompt = GetUserPrompt(request);

        var chatRequest = new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(systemMessage),
                ChatMessage.FromUser(userPrompt)
            }
        };
        
        var completionResult = _openAiService.ChatCompletion.CreateCompletionAsStream(chatRequest, "gpt-4o");

        return await Task.FromResult(StreamResponse(completionResult));
    }

    private static async IAsyncEnumerable<string> StreamResponse(IAsyncEnumerable<ChatCompletionCreateResponse> stream)
    {
        await foreach (var response in stream)
        {
            if (response.Successful && response.Choices?.Count > 0)
            {
                var content = response.Choices[0].Delta?.Content;
                if (!string.IsNullOrEmpty(content))
                {
                    yield return content;
                }
            }
        }
    }

    private static string GetSystemMessage()
    {
        return "As an expert copywriter, your task is to develop an enticing sales pitch for the product mentioned below.";
    }

    private static string GetUserPrompt(SalesPitchRequest request)
    {
        return $"""
            The structure should be based on the {request.Framework} framework. 
            Your focus should predominantly be on highlighting the product's challenges and advantages, 
            using compelling headlines to draw in the audience.
            The output must be print-ready without any instructions.

            Product Details:
            Name : {request.Product}
            Price : {request.Price}
            Features : {request.Features}
            Benefits : {request.Benefits}

            Your expected output should be a detailed sales pitch, ideally between 500 and 750 words,
            which meets the following criteria:

            Uses the {request.Framework} framework effectively.
            Draws attention to both problems the product solves and its benefits.
            Incorporates catchy headlines at relevant intervals throughout the pitch.
            Is formatted and ready for print, devoid of any further instructions.
            Please emulate the style of a seasoned advertising executive in your writing,
            and keep the tone professional yet engaging.

            Remember, the objective is to convince potential customers to invest in the product, 
            not just provide information about it. Your pitch should make them feel that the product is a solution 
            to their problems and a significant enhancement to their lives.
            """;
    }
}