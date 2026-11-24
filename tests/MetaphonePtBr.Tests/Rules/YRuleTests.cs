using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class YRuleTests
{
    [Theory]
    [InlineData(null, null, null, "I", 0)]
    [InlineData('A', 'A', 'Y', "I", 0)]
    public void Apply_ReturnsExpectedResult(char? previous2, char? previous, char? next, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(0, previous2, previous, 'Y', next, null);

        RuleResult result = YRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}