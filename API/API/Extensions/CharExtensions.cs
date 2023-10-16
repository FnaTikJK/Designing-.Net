namespace API.Extensions;

public static class CharExtensions
{
    public static bool IsWhiteSpaceOrNull(this char? ch)
    {
        return ch == null
               || char.IsWhiteSpace(ch.Value);
    }
}