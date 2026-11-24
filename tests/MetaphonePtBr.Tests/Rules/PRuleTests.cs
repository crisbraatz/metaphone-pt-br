using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class PRuleTests
{
    [Theory]
    [InlineData(0, null, 'H', "F", 1)]
    [InlineData(1, 'B', 'H', "F", 1)]
    [InlineData(0, null, null, "P", 0)]
    [InlineData(1, 'B', 'B', "P", 0)]
    public void Apply_ReturnsExpectedResult(int index, char? previous, char? next, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(index, null, previous, 'P', next, null);

        RuleResult result = PRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}