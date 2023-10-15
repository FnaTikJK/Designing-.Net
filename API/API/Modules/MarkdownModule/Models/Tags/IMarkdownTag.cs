namespace API.Modules.MarkdownModule.Models.Tags;

public interface IMarkdownTag
{
    public string HtmlTag { get; }
    public bool HasHtmlPair { get; }
    public string OpenTag { get; }
    public string CloseTag { get; }
    public char Shield => '\\';
}