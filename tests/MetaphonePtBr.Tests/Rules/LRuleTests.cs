using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class LRuleTests
{
    [Theory]
    [InlineData(1, 'B', 'H', "", 1)]
    [InlineData(0, null, 'H', "", 1)]
    [InlineData(0, null, 'B', "L", 0)]
    [InlineData(0, null, 'C', "L", 0)]
    [InlineData(1, 'B', 'A', "L", 0)]
    [InlineData(1, 'B', 'E', "L", 0)]
    [InlineData(1, 'B', 'I', "L", 0)]
    [InlineData(1, 'B', 'O', "L", 0)]
    [InlineData(1, 'B', 'U', "L", 0)]
    [InlineData(1, 'B', 'C', "", 0)]
    [InlineData(1, 'B', null, "", 0)]
    public void Apply_ReturnsExpectedResult(int index, char? previous, char? next, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(index, null, previous, 'L', next, null);

        RuleResult result = LRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}