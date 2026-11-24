using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class GRuleTests
{
    [Theory]
    [InlineData('H', 'A', "J", 0)]
    [InlineData('H', 'E', "J", 0)]
    [InlineData('H', 'I', "J", 0)]
    [InlineData('H', 'B', "GJ", 0)]
    [InlineData('H', null, "GJ", 0)]
    [InlineData('E', null, "J", 0)]
    [InlineData('I', 'B', "J", 0)]
    [InlineData('A', null, "G", 0)]
    [InlineData('O', 'B', "G", 0)]
    [InlineData('U', null, "G", 0)]
    [InlineData('B', null, "G", 0)]
    [InlineData(null, null, "G", 0)]
    public void Apply_ReturnsTokenAndSkip(char? next, char? next2, string token, int skip)
    {
        LetterContext context = new LetterContext(0, null, null, 'G', next, next2);

        RuleResult result = GRule.Apply(context);

        Assert.Equal(token, result.Token);
        Assert.Equal(skip, result.Skip);
    }
}