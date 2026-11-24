using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class ZRuleTests
{
    [Theory]
    [InlineData(null, "S", 0)]
    [InlineData('A', "Z", 0)]
    public void Apply_ReturnsExpectedResult(char? next, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(0, null, null, 'Z', next, null);

        RuleResult result = ZRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}