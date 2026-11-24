using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class TRuleTests
{
    [Theory]
    [InlineData('H', "T", 1)]
    [InlineData(null, "T", 0)]
    [InlineData('B', "T", 0)]
    public void Apply_ReturnsExpectedResult(char? next, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(0, null, null, 'T', next, null);

        RuleResult result = TRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}