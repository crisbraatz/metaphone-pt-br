namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter S.
/// </summary>
/// <remarks>
/// Rules ordered by priority: SS = S (consumes the second S); SH = X (consumes the H); vSv = Z;
/// SC[EI] = S (consumes the C); SC[AOU] = SK (consumes the C); SCH = X (consumes the C and the H);
/// SC[.] = S (consumes the C); S = S.
/// </remarks>
internal static class SRule
{
    /// <summary>
    /// Applies the S rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context)
    {
        switch (context.Next)
        {
            case 'S':
                return RuleResult.WithToken("S", 1);
            case 'H':
                return RuleResult.WithToken("X", 1);
        }

        if (LetterContext.IsVowel(context.Previous) && LetterContext.IsVowel(context.Next))
            return RuleResult.WithToken("Z");

        if (context.Next is not 'C')
            return RuleResult.WithToken("S");

        switch (context.Next2)
        {
            case 'E':
            case 'I':
                return RuleResult.WithToken("S", 2);
            case 'A':
            case 'O':
            case 'U':
                return RuleResult.WithToken("SK", 2);
            case 'H':
                return RuleResult.WithToken("X", 2);
            default:
                return RuleResult.WithToken("S", 1);
        }
    }
}