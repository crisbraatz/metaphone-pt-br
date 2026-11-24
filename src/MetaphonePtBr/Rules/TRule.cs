namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter T.
/// </summary>
/// <remarks>
/// Rules ordered by priority: TH = T (consumes the H); T = T.
/// </remarks>
internal static class TRule
{
    /// <summary>
    /// Applies the T rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context) =>
        context.Next is 'H' ? RuleResult.WithToken("T", 1) : RuleResult.WithToken("T");
}