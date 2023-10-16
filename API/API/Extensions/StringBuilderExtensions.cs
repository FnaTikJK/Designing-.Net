using System.Text;

namespace API.Extensions;

public static class StringBuilderExtensions
{
    public static bool TryAddCharOrClear(this StringBuilder builder, 
        string pattern, 
        char charToAdd,
        out bool isStartedNow,
        out bool isEnded)
    {
        var isCorrectChar = charToAdd == pattern[builder.Length];
        if (isCorrectChar)
            builder.Append(charToAdd);
        else
            builder.Clear();

        isStartedNow = builder.Length == 1;
        isEnded = builder.Length == pattern.Length;
        return isCorrectChar;
    }
}