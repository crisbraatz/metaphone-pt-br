using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class WRuleTests
{
    [Theory]
    [InlineData('A', "V", 0)]
    [InlineData('E', "V", 0)]
    [InlineData('I', "V", 0)]
    [InlineData('O', "V", 0)]
    [InlineData('U', "V", 0)]
    [InlineData('L', "V", 0)]
    [InlineData('R', "V", 0)]
    [InlineData('B', "", 0)]
    [InlineData(null, "", 0)]
    public void Apply_ReturnsExpectedResult(char? next, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(0, null, null, 'W', next, null);

        RuleResult result = WRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}