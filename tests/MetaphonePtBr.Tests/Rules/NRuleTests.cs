using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class NRuleTests
{
    [Theory]
    [InlineData(1, 'B', null, "M", 0)]
    [InlineData(1, 'N', null, "M", 0)]
    [InlineData(1, 'B', 'H', "", 1)]
    [InlineData(2, 'N', 'H', "", 1)]
    [InlineData(2, 'N', 'A', "", 0)]
    [InlineData(1, 'B', 'A', "N", 0)]
    [InlineData(0, null, 'A', "N", 0)]
    public void Apply_ReturnsExpectedResult(int index, char? previous, char? next, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(index, null, previous, 'N', next, null);

        RuleResult result = NRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}