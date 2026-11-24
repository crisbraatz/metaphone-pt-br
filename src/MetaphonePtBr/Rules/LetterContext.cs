namespace MetaphonePtBr.Rules;

/// <summary>
/// The letter under evaluation together with its neighbours in a folded word.
/// Neighbours outside the word are <see langword="null"/>.
/// </summary>
internal readonly struct LetterContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LetterContext"/> struct.
    /// </summary>
    /// <param name="index">The zero-based position of the current letter.</param>
    /// <param name="previous2">The letter two positions before the current one, or <see langword="null"/>.</param>
    /// <param name="previous">The letter immediately before the current one, or <see langword="null"/>.</param>
    /// <param name="current">The current letter.</param>
    /// <param name="next">The letter immediately after the current one, or <see langword="null"/>.</param>
    /// <param name="next2">The letter two positions after the current one, or <see langword="null"/>.</param>
    internal LetterContext(int index, char? previous2, char? previous, char current, char? next, char? next2)
    {
        Index = index;
        Previous2 = previous2;
        Previous = previous;
        Current = current;
        Next = next;
        Next2 = next2;
    }

    /// <summary>
    /// Gets the zero-based position of the current letter.
    /// </summary>
    internal int Index { get; }

    /// <summary>
    /// Gets the letter two positions before the current one, or <see langword="null"/>.
    /// </summary>
    internal char? Previous2 { get; }

    /// <summary>
    /// Gets the letter immediately before the current one, or <see langword="null"/>.
    /// </summary>
    internal char? Previous { get; }

    /// <summary>
    /// Gets the current letter.
    /// </summary>
    internal char Current { get; }

    /// <summary>
    /// Gets the letter immediately after the current one, or <see langword="null"/>.
    /// </summary>
    internal char? Next { get; }

    /// <summary>
    /// Gets the letter two positions after the current one, or <see langword="null"/>.
    /// </summary>
    internal char? Next2 { get; }

    /// <summary>
    /// Builds the context for the letter at <paramref name="index"/> of <paramref name="word"/>.
    /// Positions outside the word yield <see langword="null"/>.
    /// </summary>
    /// <param name="word">The folded word.</param>
    /// <param name="index">The zero-based position of the current letter.</param>
    /// <returns>The letter context at <paramref name="index"/>.</returns>
    internal static LetterContext At(string word, int index) =>
        new LetterContext(
            index,
            LetterAt(word, index - 2),
            LetterAt(word, index - 1),
            word[index],
            LetterAt(word, index + 1),
            LetterAt(word, index + 2));

    /// <summary>
    /// Returns whether <paramref name="letter"/> is one of the vowels A, E, I, O or U.
    /// </summary>
    /// <param name="letter">The letter to test; may be <see langword="null"/>.</param>
    /// <returns><see langword="true"/> only for A, E, I, O and U.</returns>
    internal static bool IsVowel(char? letter) =>
        letter is 'A' or 'E' or 'I' or 'O' or 'U';

    private static char? LetterAt(string word, int position) =>
        position >= 0 && position < word.Length ? word[position] : (char?)null;
}