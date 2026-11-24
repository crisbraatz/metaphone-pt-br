namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter X.
/// </summary>
/// <remarks>
/// Rules ordered by priority: X$ = X; ^EX[v] = Z; .EX[EI] = X (consumes the vowel); .EX[AOU] = KS (consumes the vowel);
/// EX[C] = S (consumes the C); EX[PT] = S; EX[.] = KS; [vCKGLRX][AIOU]X = X; [DFMNPQSTVZ][AIOU]X = KS; X = X.
/// </remarks>
internal static class XRule
{
    /// <summary>
    /// Applies the X rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context)
    {
        if (context.Next is null)
            return RuleResult.WithToken("X");

        if (context.Previous is 'E')
            return ApplyAfterE(context);

        if (LetterContext.IsVowel(context.Previous) && !IsXPrecedingLetter(context.Previous2))
            return RuleResult.WithToken("KS");

        return RuleResult.WithToken("X");
    }

    private static RuleResult ApplyAfterE(in LetterContext context)
    {
        if (LetterContext.IsVowel(context.Next))
        {
            if (context.Previous2 is null)
                return RuleResult.WithToken("Z");

            switch (context.Next)
            {
                case 'E':
                case 'I':
                    return RuleResult.WithToken("X", 1);
                default:
                    return RuleResult.WithToken("KS", 1);
            }
        }

        switch (context.Next)
        {
            case 'C':
                return RuleResult.WithToken("S", 1);
            case 'P':
            case 'T':
                return RuleResult.WithToken("S");
            default:
                return RuleResult.WithToken("KS");
        }
    }

    private static bool IsXPrecedingLetter(char? letter) =>
        letter is 'A' or 'C' or 'E' or 'G' or 'I' or 'K' or 'L' or 'O' or 'R' or 'U' or 'X';
}