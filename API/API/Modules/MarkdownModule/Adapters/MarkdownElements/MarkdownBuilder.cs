using API.Extensions;
using API.Modules.MarkdownModule.Models;
using API.Modules.MarkdownModule.Models.Tags;
using System.Text;

namespace API.Modules.MarkdownModule.Adapters.MarkdownElements;

public abstract class MarkdownBuilder
{
    public IMarkdownTag MarkdownTag => tag;

    protected IMarkdownTag tag;
    protected Token token;
    protected StringBuilder tagBuilder;
    protected char? last;
    protected bool isShielded;

    public List<Token> ProcessChar(char? previous, char current, char? next, int index, List<Token> tokens)
    {
        if (token.IsClosed)
        {
            if (IsValid(tokens))
            {
                tokens.Add(token);
                token = new Token(tag);
            }
            else
            {
                token.ShiftStartToEnd();
            }

            return tokens;
        }
        
        if (isShielded)
        {
            last = current;
            isShielded = false;
            return tokens;
        }

        if (current == tag.Shield)
        {
            isShielded = true;
            return tokens;
        }

        if (!token.IsOpened)
        {
            TryBuildBorder(previous, current, next, index, tag.OpenTag, token.Start);
            return tokens;
        }

        if (!token.IsClosed)
        {
            TryBuildBorder(previous, current, next, index, tag.CloseTag, token.End);
        }

        if (!token.IsClosed)
            ProcessCustomChar(last, current);
        
        return tokens;
    }

    protected abstract void ProcessCustomChar(char? last, char current);

    protected abstract bool IsTokenCorrect();

    protected abstract bool IsTokenFitsWithOthers(List<Token> tokens, out IEnumerable<Token> tokensToRemove);

    protected void TryBuildBorder(char? previous, char current, char? next, int index, string pattern, TokenBorder border)
    {
        if (!tagBuilder.TryAddCharOrClear(pattern, current, out var isStartedNow, out var isEnded))
            return;

        if (isStartedNow)
            border.Neighbors = border.Neighbors with {Before = previous};
        if (isEnded)
            border.Neighbors = border.Neighbors with {After = next};
        else
            return;

        border.Index = index - tagBuilder.Length + 1;
        tagBuilder.Clear();  
    }

    protected bool IsValid(List<Token> tokens)
    {
        var isCorrect = IsTokenCorrect();
        var isFit = IsTokenFitsWithOthers(tokens, out var tokensToRemove);
        foreach (var token in tokensToRemove)
            tokens.Remove(token);

        return isCorrect && isFit;
    }
}