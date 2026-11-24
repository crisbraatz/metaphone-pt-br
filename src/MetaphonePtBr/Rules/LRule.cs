namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter L.
/// </summary>
/// <remarks>
/// Rules ordered by priority: LH = bypass (consumes the H); ^L = L; L[v] = L; L = bypass.
/// </remarks>
internal static class LRule
{
    /// <summary>
    /// Applies the L rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context)
    {
        if (context.Next is 'H')
            return RuleResult.Increment(1);

        return context.Previous is null || LetterContext.IsVowel(context.Next)
            ? RuleResult.WithToken("L")
            : RuleResult.Empty;
    }
}