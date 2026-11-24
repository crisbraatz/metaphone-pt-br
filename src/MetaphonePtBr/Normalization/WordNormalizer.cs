using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MetaphonePtBr.Normalization;

/// <summary>
/// Validates a word against the input contract and converts it into its canonical form:
/// precomposed (NFC) and uppercase, containing only Brazilian Portuguese letters.
/// </summary>
internal static class WordNormalizer
{
    /// <summary>
    /// Tries to normalize <paramref name="input"/>.
    /// </summary>
    /// <param name="input">A single Brazilian Portuguese word.</param>
    /// <param name="normalized">The canonical uppercase word when the input is valid; otherwise <see langword="null"/>.</param>
    /// <returns><see langword="true"/> when the input satisfies the input contract.</returns>
    internal static bool TryNormalize(string? input, [NotNullWhen(true)] out string? normalized)
    {
        normalized = null;

        if (string.IsNullOrEmpty(input) || ContainsSurrogate(input!))
        {
            return false;
        }

        string composed = input!.Normalize(NormalizationForm.FormC);
        char[] buffer = new char[composed.Length];

        for (int i = 0; i < composed.Length; i++)
        {
            char letter = composed[i];
            if (!IsSupportedLetter(letter))
            {
                return false;
            }

            buffer[i] = char.ToUpperInvariant(letter);
        }

        normalized = new string(buffer);
        return true;
    }

    /// <summary>
    /// Returns whether <paramref name="letter"/> is an ASCII letter or a supported accented Brazilian Portuguese letter.
    /// </summary>
    internal static bool IsSupportedLetter(char letter) =>
        letter is (>= 'A' and <= 'Z') or (>= 'a' and <= 'z')
            or 'Á' or 'À' or 'Â' or 'Ã' or 'É' or 'Ê' or 'Í' or 'Ó' or 'Ô' or 'Õ' or 'Ú' or 'Ü' or 'Ç'
            or 'á' or 'à' or 'â' or 'ã' or 'é' or 'ê' or 'í' or 'ó' or 'ô' or 'õ' or 'ú' or 'ü' or 'ç';

    // string.Normalize throws on ill-formed UTF-16 on some runtimes (e.g. .NET Framework), so surrogates
    // are rejected before normalizing. On .NET 10 the letter check rejects them anyway, so a Boolean mutant on the
    // "return true" below survives there as an equivalent mutant.
    private static bool ContainsSurrogate(string input)
    {
        foreach (char c in input)
        {
            if (char.IsSurrogate(c))
            {
                return true;
            }
        }

        return false;
    }
}