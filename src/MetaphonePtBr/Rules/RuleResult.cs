namespace MetaphonePtBr.Rules;

/// <summary>
/// The outcome of applying a letter rule: the phonetic token to emit and the number of extra letters consumed.
/// </summary>
internal readonly struct RuleResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RuleResult"/> struct.
    /// </summary>
    /// <param name="token">The phonetic token to emit; empty when the letter emits nothing.</param>
    /// <param name="skip">The number of letters consumed beyond the current one.</param>
    private RuleResult(string token, int skip)
    {
        Token = token;
        Skip = skip;
    }

    /// <summary>
    /// Gets the phonetic token to emit; empty when the letter emits nothing.
    /// </summary>
    internal string Token { get; }

    /// <summary>
    /// Gets the number of letters consumed beyond the current one. The engine advances by <c>Skip + 1</c>.
    /// </summary>
    internal int Skip { get; }

    /// <summary>
    /// Gets a result that emits no token and consumes only the current letter.
    /// </summary>
    internal static RuleResult Empty => new RuleResult(string.Empty, 0);

    /// <summary>
    /// Creates a result that emits no token and consumes <paramref name="skip"/> extra letters.
    /// </summary>
    /// <param name="skip">The number of extra letters to consume.</param>
    /// <returns>A result with an empty token and the given skip.</returns>
    internal static RuleResult Increment(int skip) => new RuleResult(string.Empty, skip);

    /// <summary>
    /// Creates a result that emits <paramref name="token"/> and consumes only the current letter.
    /// </summary>
    /// <param name="token">The phonetic token to emit.</param>
    /// <returns>A result with the given token and a skip of 0.</returns>
    internal static RuleResult WithToken(string token) => new RuleResult(token, 0);

    /// <summary>
    /// Creates a result that emits <paramref name="token"/> and consumes <paramref name="skip"/> extra letters.
    /// </summary>
    /// <param name="token">The phonetic token to emit.</param>
    /// <param name="skip">The number of extra letters to consume.</param>
    /// <returns>A result with the given token and skip.</returns>
    internal static RuleResult WithToken(string token, int skip) => new RuleResult(token, skip);
}