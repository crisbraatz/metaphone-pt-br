namespace MetaphonePtBr.Rules;

/// <summary>
/// Applies the phonetic rules to a folded word, letter by letter, and joins the resulting tokens.
/// </summary>
internal static class RuleEngine
{
    /// <summary>
    /// Encodes an uppercase, accent-folded word into its phonetic token.
    /// </summary>
    /// <param name="folded">An uppercase word with accents already folded by <see cref="Normalization.AccentFolder"/>.</param>
    /// <returns>The concatenated phonetic token; empty when every letter is bypassed.</returns>
    internal static string Encode(string folded)
    {
        // Buffer sizing invariant: a rule applied at index i consumes Skip + 1 letters and emits a token of
        // at most 2 * (Skip + 1) characters. Summed over the whole word, the emitted length never exceeds
        // 2 * folded.Length, so the buffer cannot overflow. RuleTokenLengthTests enforces this for every rule.
        char[] buffer = new char[folded.Length * 2];
        int length = 0;
        int index = 0;

        while (index < folded.Length)
        {
            LetterContext context = LetterContext.At(folded, index);

            RuleResult result = Dispatch(context);

            string token = result.Token;
            for (int i = 0; i < token.Length; i++)
            {
                buffer[length++] = token[i];
            }

            index += result.Skip + 1;
        }

        return length == 0 ? string.Empty : new string(buffer, 0, length);
    }

    internal static RuleResult Dispatch(in LetterContext context)
    {
        switch (context.Current)
        {
            case 'A':
            case 'E':
            case 'I':
            case 'O':
            case 'U':
                return VowelsRule.Apply(context);
            case 'B':
            case 'D':
            case 'F':
            case 'J':
            case 'K':
            case 'M':
            case 'V':
                return ImmutablesRule.Apply(context);
            case 'C':
            case 'Ç':
                return CRule.Apply(context);
            case 'G':
                return GRule.Apply(context);
            case 'H':
                return HRule.Apply(context);
            case 'L':
                return LRule.Apply(context);
            case 'N':
                return NRule.Apply(context);
            case 'P':
                return PRule.Apply(context);
            case 'Q':
                return QRule.Apply(context);
            case 'R':
                return RRule.Apply(context);
            case 'S':
                return SRule.Apply(context);
            case 'T':
                return TRule.Apply(context);
            case 'W':
                return WRule.Apply(context);
            case 'X':
                return XRule.Apply(context);
            case 'Y':
                return YRule.Apply(context);
            case 'Z':
                return ZRule.Apply(context);
            default:
                return RuleResult.Empty;
        }
    }
}