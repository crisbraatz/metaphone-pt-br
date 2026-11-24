namespace MetaphonePtBr.Normalization;

/// <summary>
/// Folds the accented letters produced by <see cref="WordNormalizer"/> onto the legacy alphabet (A-Z and Ç).
/// </summary>
internal static class AccentFolder
{
    /// <summary>
    /// Replaces every accented uppercase letter with its unaccented base letter.
    /// Ç and the ASCII letters are kept unchanged.
    /// </summary>
    /// <param name="normalized">An uppercase, NFC-normalized word as returned by <see cref="WordNormalizer"/>.</param>
    /// <returns>The folded word; the same instance when it contains no accented letter.</returns>
    internal static string Fold(string normalized)
    {
        int first = 0;
        while (first < normalized.Length && FoldLetter(normalized[first]) == normalized[first])
        {
            first++;
        }

        if (first == normalized.Length)
        {
            return normalized;
        }

        char[] buffer = new char[normalized.Length];

        normalized.CopyTo(0, buffer, 0, first);

        for (int i = first; i < normalized.Length; i++)
        {
            buffer[i] = FoldLetter(normalized[i]);
        }

        return new string(buffer);
    }

    private static char FoldLetter(char letter)
    {
        switch (letter)
        {
            case 'Á':
            case 'À':
            case 'Â':
            case 'Ã':
                return 'A';
            case 'É':
            case 'Ê':
                return 'E';
            case 'Í':
                return 'I';
            case 'Ó':
            case 'Ô':
            case 'Õ':
                return 'O';
            case 'Ú':
            case 'Ü':
                return 'U';
            default:
                return letter;
        }
    }
}