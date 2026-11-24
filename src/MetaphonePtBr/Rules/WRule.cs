namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter W.
/// </summary>
/// <remarks>
/// Rules ordered by priority: W[v] = V; W[LR] = V; W[c] = bypass; W$ = bypass.
/// </remarks>
internal static class WRule
{
    /// <summary>
    /// Applies the W rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context) =>
        LetterContext.IsVowel(context.Next) || (context.Next is 'L' or 'R')
            ? RuleResult.WithToken("V")
            : RuleResult.Empty;
}