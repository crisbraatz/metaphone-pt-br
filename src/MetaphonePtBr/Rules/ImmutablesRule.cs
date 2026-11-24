namespace MetaphonePtBr.Rules;

/// <summary>
/// Letter rule for the consonants that always map to themselves: B, D, F, J, K, M and V.
/// </summary>
internal static class ImmutablesRule
{
    /// <summary>
    /// Applies the immutable consonant rule to the letter in <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The letter itself as token for B, D, F, J, K, M and V; otherwise an empty result.</returns>
    internal static RuleResult Apply(in LetterContext context) =>
        context.Current switch
        {
            'B' => RuleResult.WithToken("B"),
            'D' => RuleResult.WithToken("D"),
            'F' => RuleResult.WithToken("F"),
            'J' => RuleResult.WithToken("J"),
            'K' => RuleResult.WithToken("K"),
            'M' => RuleResult.WithToken("M"),
            'V' => RuleResult.WithToken("V"),
            _ => RuleResult.Empty,
        };
}