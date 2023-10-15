namespace API.Modules.MarkdownModule.Models.Tags;

public record struct ItalicTag : IMarkdownTag
{
    public string HtmlTag => htmlTag;
    public bool HasHtmlPair => true;
    public string OpenTag => tag;
    public string CloseTag => tag;
    
    private const string tag = "_";
    private const string htmlTag = "em";
}