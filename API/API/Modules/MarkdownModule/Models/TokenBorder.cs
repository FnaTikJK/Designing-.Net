namespace API.Modules.MarkdownModule.Models;

public class TokenBorder
{
    public int? Index { get; set; }
    public (char? Before, char? After) Neighbors { get; set; }
}