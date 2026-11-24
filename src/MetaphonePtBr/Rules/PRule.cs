namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter P.
/// </summary>
/// <remarks>
/// Rules ordered by priority: PH = F (consumes the H); P = P.
/// </remarks>
internal static class PRule
{
    /// <summary>
    /// Applies the P rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context) =>
        context.Next is 'H' ? RuleResult.WithToken("F", 1) : RuleResult.WithToken("P");
}