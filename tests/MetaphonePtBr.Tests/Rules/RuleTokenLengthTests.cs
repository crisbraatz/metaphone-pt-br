using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class RuleTokenLengthTests
{
    private static readonly char?[] Neighbours =
    [
        null,
        'A', 'E', 'I', 'O', 'U',
        'B', 'L', 'N', 'T', 'Z',
        'H', 'R', 'S', 'C',
    ];

    private static readonly char[] Letters =
    [
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
        'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Ç',
    ];

    [Fact]
    public void Dispatch_EveryLetterInEveryContext_TokenFitsWithinSkipBudget()
    {
        int checkedContexts = 0;

        foreach (char letter in Letters)
        {
            foreach (char? previous2 in Neighbours)
            {
                foreach (char? previous in Neighbours)
                {
                    foreach (char? next in Neighbours)
                    {
                        foreach (char? next2 in Neighbours)
                        {
                            var context = new LetterContext(2, previous2, previous, letter, next, next2);

                            RuleResult result = RuleEngine.Dispatch(context);

                            Assert.True(
                                result.Token.Length <= 2 * (result.Skip + 1),
                                $"Letter '{letter}' with context [{previous2}, {previous}, {next}, {next2}] " +
                                $"emitted '{result.Token}' (length {result.Token.Length}) with Skip {result.Skip}.");
                            checkedContexts++;
                        }
                    }
                }
            }
        }

        Assert.Equal(Letters.Length * Neighbours.Length * Neighbours.Length * Neighbours.Length * Neighbours.Length, checkedContexts);
    }
}