using System.Diagnostics.CodeAnalysis;

using MetaphonePtBr.Normalization;
using MetaphonePtBr.Rules;

namespace MetaphonePtBr;

/// <summary>
/// Encodes single Brazilian Portuguese words into phonetic tokens for fuzzy search.
/// </summary>
public static class Metaphone
{
    /// <summary>
    /// Encodes a single Brazilian Portuguese word into its phonetic token.
    /// </summary>
    /// <param name="word">
    /// A single word containing only Brazilian Portuguese letters (A-Z, a-z and the accented letters
    /// Á À Â Ã É Ê Í Ó Ô Õ Ú Ü Ç and their lowercase forms). Case is ignored.
    /// </param>
    /// <returns>
    /// The phonetic token. The token is empty when every letter of the word is bypassed by the rules (for example <c>H</c>, <c>RR</c> or <c>LH</c>).
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="word"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="word"/> is not a single word made only of supported letters.</exception>
    /// <example>
    /// <code>
    /// string token = Metaphone.Encode("Cristopher"); // "KRSTF"
    /// </code>
    /// </example>
    public static string Encode(string word)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(word);
#else
        if (word is null)
        {
            throw new ArgumentNullException(nameof(word));
        }
#endif

        if (!WordNormalizer.TryNormalize(word, out string? normalized))
        {
            throw new ArgumentException("Value must be a single word containing only supported Brazilian Portuguese letters.", nameof(word));
        }

        return RuleEngine.Encode(AccentFolder.Fold(normalized));
    }

    /// <summary>
    /// Tries to encode a single Brazilian Portuguese word into its phonetic token without throwing.
    /// </summary>
    /// <param name="word">
    /// A single word containing only Brazilian Portuguese letters. Case is ignored. <see langword="null"/> is accepted and returns <see langword="false"/>.
    /// </param>
    /// <param name="token">
    /// When this method returns <see langword="true"/>, the phonetic token (possibly empty); otherwise <see langword="null"/>.
    /// </param>
    /// <returns><see langword="true"/> when <paramref name="word"/> is valid and was encoded; otherwise <see langword="false"/>.</returns>
    /// <example>
    /// <code>
    /// if (Metaphone.TryEncode("Cristopher", out string? token))
    /// {
    ///     // token == "KRSTF"
    /// }
    /// </code>
    /// </example>
    public static bool TryEncode(string? word, [NotNullWhen(true)] out string? token)
    {
        if (!WordNormalizer.TryNormalize(word, out string? normalized))
        {
            token = null;
            return false;
        }

        token = RuleEngine.Encode(AccentFolder.Fold(normalized));
        return true;
    }
}