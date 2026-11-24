using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class RRuleTests
{
    [Theory]
    [InlineData(0, null, 'A', "", 0)]
    [InlineData(1, 'A', null, "", 0)]
    [InlineData(1, 'A', 'R', "", 1)]
    [InlineData(1, 'A', 'A', "R", 0)]
    [InlineData(1, 'B', 'C', "R", 0)]
    [InlineData(1, 'B', 'A', "R", 0)]
    public void Apply_ReturnsExpectedResult(int index, char? previous, char? next, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(index, null, previous, 'R', next, null);

        RuleResult result = RRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}