using System.Text;
using API.Extensions;
using API.Modules.MarkdownModule.Models;
using API.Modules.MarkdownModule.Models.Tags;

namespace API.Modules.MarkdownModule.Adapters.MarkdownElements;

public class ItalicBuilder : MarkdownBuilder
{
    public IMarkdownTag MarkdownTag => tag;
    
    public ItalicBuilder()
    {
        tag = new ItalicTag(); 
        token = new Token(tag);
        tagBuilder = new StringBuilder(Math.Max(tag.OpenTag.Length, tag.CloseTag?.Length ?? 0));
    }

    private bool hasLetters;
    private bool hasSpaces;

    private bool isPartial => !token.Start.Neighbors.Before.IsWhiteSpaceOrNull()
                              || !token.End.Neighbors.After.IsWhiteSpaceOrNull();
    private bool isCorrectStart => !char.IsWhiteSpace(token.Start.Neighbors.After.Value);
    private bool isCorrectEnd => !char.IsWhiteSpace(token.End.Neighbors.Before.Value);

    protected override void ProcessCustomChar(char? last, char current)
    {
        if (char.IsLetter(current))
            hasLetters = true;
        else if (char.IsWhiteSpace(current))
            hasSpaces = true;
    }

    protected override bool IsTokenCorrect()
    {
        return hasLetters
            && isCorrectStart
            && isCorrectEnd
            && !(isPartial && hasSpaces);
    }

    protected override bool IsTokenFitsWithOthers(List<Token> tokens, out IEnumerable<Token> tokensToRemove)
    {
        tokensToRemove = new List<Token>();
        return true;
    }
}