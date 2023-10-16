using API.Modules.MarkdownModule.Models.Tags;

namespace API.Modules.MarkdownModule.Models;

public class Token
{
    public TokenBorder Start { get; set; }
    public TokenBorder End { get; set; }
    public bool IsOpened => Start.Index != null;
    public bool IsClosed => Start.Index != null && End.Index != null;
    public IMarkdownTag MarkdownTag { get; set; }

    public Token(IMarkdownTag markdownTag)
    {
        MarkdownTag = markdownTag;
        Start = new TokenBorder();
        End = new TokenBorder();
    }

    public IEnumerable<(int Index, string Tag, int MarkdownLength)> SplitToken()
    {
        if (Start.Index == null || End.Index == null)
            throw new AggregateException("Token is not built");
        
        yield return (Start.Index.Value, $"<{MarkdownTag.HtmlTag}>", MarkdownTag.OpenTag.Length);
        yield return (End.Index.Value, $"</{MarkdownTag.HtmlTag}>", MarkdownTag.CloseTag.Length);
    }

    public void ShiftStartToEnd()
    {
        Start = End;
        End = new TokenBorder();
    }
}
