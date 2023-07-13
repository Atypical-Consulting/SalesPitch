using OpenAI.ObjectModels.ResponseModels;

namespace SalesPitch.Extensions;

/// <summary>
///     Extensions for <see cref="ChatCompletionCreateResponse"/>
/// </summary>
public static class ChatCompletionCreateResponseExtensions
{
    /// <summary>
    ///     Get the content of the first choice
    /// </summary>
    /// <param name="completion">
    ///     The <see cref="ChatCompletionCreateResponse"/> to get the content from
    /// </param>
    /// <returns>
    ///     The content of the first choice
    /// </returns>
    public static string GetContent(this ChatCompletionCreateResponse completion)
        => completion.Choices.FirstOrDefault()?.Message.Content
           ?? string.Empty;

    /// <summary>
    ///     Get the error message from the <see cref="ChatCompletionCreateResponse"/>
    /// </summary>
    /// <param name="completion">
    ///     The <see cref="ChatCompletionCreateResponse"/> to get the error from
    /// </param>
    /// <returns>
    ///     The error message
    /// </returns>
    public static string GetError(this ChatCompletionCreateResponse completion)
        => completion.Error == null
            ? "Unknown Error"
            : $"{completion.Error.Code}: {completion.Error.Message}";
}