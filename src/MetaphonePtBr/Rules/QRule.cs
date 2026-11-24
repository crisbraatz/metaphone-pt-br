namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the letter Q.
/// </summary>
/// <remarks>
/// Rules ordered by priority: Q = K.
/// </remarks>
internal static class QRule
{
    /// <summary>
    /// Applies the Q rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token K, consuming only the current letter.</returns>
    internal static RuleResult Apply(in LetterContext context) => RuleResult.WithToken("K");
}