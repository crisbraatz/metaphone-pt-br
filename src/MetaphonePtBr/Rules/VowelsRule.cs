namespace MetaphonePtBr.Rules;

/// <summary>
/// Phonetic rules for the vowels A, E, I, O and U.
/// </summary>
/// <remarks>
/// Rules ordered by priority: ^[v] = v (only at the beginning of the word); otherwise bypass.
/// </remarks>
internal static class VowelsRule
{
    /// <summary>
    /// Applies the vowel rules to the letter described by <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The letter under evaluation and its neighbours.</param>
    /// <returns>The vowel as token when it starts the word; otherwise an empty result.</returns>
    internal static RuleResult Apply(in LetterContext context)
    {
        if (context.Index != 0)
            return RuleResult.Empty;

        return context.Current switch
        {
            'A' => RuleResult.WithToken("A"),
            'E' => RuleResult.WithToken("E"),
            'I' => RuleResult.WithToken("I"),
            'O' => RuleResult.WithToken("O"),
            'U' => RuleResult.WithToken("U"),
            _ => RuleResult.Empty,
        };
    }
}