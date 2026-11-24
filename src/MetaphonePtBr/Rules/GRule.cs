namespace MetaphonePtBr.Rules;

/// <summary>
/// Letter rule for G.
/// GH before a vowel gives J (the H is then bypassed), GH before a consonant gives GJ,
/// G before E or I gives J, and any other G gives G.
/// </summary>
internal static class GRule
{
    /// <summary>
    /// Applies the G rules to the letter in <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The token and skip for the letter.</returns>
    internal static RuleResult Apply(in LetterContext context) =>
        context.Next switch
        {
            'H' => LetterContext.IsVowel(context.Next2) ? RuleResult.WithToken("J") : RuleResult.WithToken("GJ"),
            'E' or 'I' => RuleResult.WithToken("J"),
            _ => RuleResult.WithToken("G"),
        };
}