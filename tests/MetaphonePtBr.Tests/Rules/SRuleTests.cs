using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class SRuleTests
{
    [Theory]
    [InlineData('A', 'S', null, "S", 1)]
    [InlineData('A', 'H', null, "X", 1)]
    [InlineData('A', 'A', null, "Z", 0)]
    [InlineData('E', 'E', null, "Z", 0)]
    [InlineData('I', 'I', null, "Z", 0)]
    [InlineData('O', 'O', null, "Z", 0)]
    [InlineData('U', 'U', null, "Z", 0)]
    [InlineData('A', 'C', 'E', "S", 2)]
    [InlineData('A', 'C', 'I', "S", 2)]
    [InlineData('A', 'C', 'A', "SK", 2)]
    [InlineData('A', 'C', 'O', "SK", 2)]
    [InlineData('A', 'C', 'U', "SK", 2)]
    [InlineData('A', 'C', 'H', "X", 2)]
    [InlineData('A', 'C', 'B', "S", 1)]
    [InlineData(null, 'B', null, "S", 0)]
    [InlineData('A', 'B', null, "S", 0)]
    [InlineData('B', 'A', null, "S", 0)]
    [InlineData('B', null, null, "S", 0)]
    public void Apply_ReturnsExpectedResult(char? previous, char? next, char? next2, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(1, null, previous, 'S', next, next2);

        RuleResult result = SRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}