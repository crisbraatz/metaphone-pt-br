namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter Z.
/// </summary>
/// <remarks>
/// Rules ordered by priority: Z$ = S; Z = Z.
/// </remarks>
internal static class ZRule
{
    /// <summary>
    /// Applies the Z rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context) =>
        context.Next is null ? RuleResult.WithToken("S") : RuleResult.WithToken("Z");
}