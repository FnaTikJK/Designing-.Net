using System.Text;
using API.Infrasturcture;
using API.Modules.MarkdownModule.Adapters.MarkdownElements;
using API.Modules.MarkdownModule.Models;
using API.Modules.MarkdownModule.Ports;

namespace API.Modules.MarkdownModule.Adapters;

public class MarkdownService : IMarkdownService
{
    private MarkdownBuilder[] markdownBuilders;
    
    public MarkdownService()
    {
        markdownBuilders = new[]
        {
            new ItalicBuilder()
        };
    }

    public MarkdownService(IEnumerable<MarkdownBuilder> builders)
    {
        markdownBuilders = builders.ToArray();
    }
    
    public async Task<Result<string>> ProcessTextAsync(string sourceText)
    {
        sourceText += Environment.NewLine; // Нужно для работы лайновых тегов и закрытия остальных
        var tokens = new List<Token>();
        for (int i = 0; i < sourceText.Length; i++)
        {
            foreach (var builder in markdownBuilders)
            {
                char? previous = i == 0
                    ? null
                    : sourceText[i - 1];
                char? next = i == sourceText.Length - 1
                    ? null
                    : sourceText[i + 1];
                tokens = builder.ProcessChar(previous, sourceText[i], next, i, tokens);
            }
        }

        if (sourceText.EndsWith(Environment.NewLine))
            sourceText = sourceText.Substring(0, sourceText.Length - Environment.NewLine.Length); 
            // сохраняем исходный текст, если лайновый тег не понадобился
        
        return Result.Ok(BuildMarkdownString(sourceText, tokens));
    }

    public string BuildMarkdownString(string sourceText, List<Token> tokens)
    {
        if (tokens.Count == 0)
            return sourceText;

        var splitTokens = tokens
            .SelectMany(token => token.SplitToken())
            .OrderBy(split => split.Index)
            .ToArray();
        var builder = new StringBuilder();
        var curPos = 0;
        foreach (var point in splitTokens)
        {
            builder.Append(sourceText.Substring(curPos, point.Index - curPos));
            builder.Append(point.Tag);
            curPos += (point.Index - curPos) + point.MarkdownLength;
        }
        builder.Append(sourceText.Substring(curPos, sourceText.Length - curPos));

        return builder.ToString();
    }
}