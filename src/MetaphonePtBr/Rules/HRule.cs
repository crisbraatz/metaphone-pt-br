namespace MetaphonePtBr.Rules;

/// <summary>
/// Letter rule for H.
/// An H at the start of the word followed by a vowel emits that vowel and consumes it;
/// every other H is silent.
/// </summary>
internal static class HRule
{
    /// <summary>
    /// Applies the H rule to the letter in <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token and skip for the letter.</returns>
    internal static RuleResult Apply(in LetterContext context)
    {
        if (context.Index != 0)
        {
            return RuleResult.Empty;
        }

        return context.Next switch
        {
            'A' => RuleResult.WithToken("A", 1),
            'E' => RuleResult.WithToken("E", 1),
            'I' => RuleResult.WithToken("I", 1),
            'O' => RuleResult.WithToken("O", 1),
            'U' => RuleResult.WithToken("U", 1),
            _ => RuleResult.Empty,
        };
    }
}