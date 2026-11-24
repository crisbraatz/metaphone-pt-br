namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter N.
/// </summary>
/// <remarks>
/// Rules ordered by priority: N$ = M; NH = bypass (consumes the H); NN = bypass; N = N.
/// </remarks>
internal static class NRule
{
    /// <summary>
    /// Applies the N rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context)
    {
        if (context.Next is null)
            return RuleResult.WithToken("M");

        if (context.Next is 'H')
            return RuleResult.Increment(1);

        return context.Previous is 'N' ? RuleResult.Empty : RuleResult.WithToken("N");
    }
}