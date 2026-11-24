namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter Y.
/// </summary>
/// <remarks>
/// Rules ordered by priority: Y = I.
/// </remarks>
internal static class YRule
{
    /// <summary>
    /// Applies the Y rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token to emit and the number of extra letters to consume.</returns>
    internal static RuleResult Apply(in LetterContext context) => RuleResult.WithToken("I");
}