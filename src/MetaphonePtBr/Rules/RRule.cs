namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter R.
/// </summary>
/// <remarks>
/// Rules ordered by priority: ^R = bypass; R$ = bypass; RR = bypass (consumes the second R); R = R.
/// </remarks>
internal static class RRule
{
    /// <summary>
    /// Applies the R rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context)
    {
        if (context.Previous is null || context.Next is null)
            return RuleResult.Empty;

        return context.Next is 'R' ? RuleResult.Increment(1) : RuleResult.WithToken("R");
    }
}