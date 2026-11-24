namespace MetaphonePtBr.Rules;

/// <summary>
/// Letter rule for C and its cedilla variant.
/// CH before R gives K, CH gives X, C before E or I gives S, C before Q or K is silent,
/// any other C gives K, and Ç always gives S.
/// </summary>
internal static class CRule
{
    /// <summary>
    /// Applies the C and Ç rules to the letter in <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token and skip for the letter.</returns>
    internal static RuleResult Apply(in LetterContext context) =>
        context.Current switch
        {
            'C' => ApplyC(context),
            'Ç' => RuleResult.WithToken("S"),
            _ => RuleResult.Empty,
        };

    private static RuleResult ApplyC(in LetterContext context) =>
        context.Next switch
        {
            'H' => context.Next2 is 'R' ? RuleResult.WithToken("K", 1) : RuleResult.WithToken("X", 1),
            'E' or 'I' => RuleResult.WithToken("S"),
            'Q' or 'K' => RuleResult.Empty,
            _ => RuleResult.WithToken("K"),
        };
}