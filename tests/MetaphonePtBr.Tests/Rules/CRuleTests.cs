using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class CRuleTests
{
    [Theory]
    [InlineData('C', 'H', 'R', "K", 1)]
    [InlineData('C', 'H', 'B', "X", 1)]
    [InlineData('C', 'H', null, "X", 1)]
    [InlineData('C', 'E', null, "S", 0)]
    [InlineData('C', 'I', 'B', "S", 0)]
    [InlineData('C', 'Q', null, "", 0)]
    [InlineData('C', 'K', null, "", 0)]
    [InlineData('C', 'A', null, "K", 0)]
    [InlineData('C', 'O', 'B', "K", 0)]
    [InlineData('C', 'U', null, "K", 0)]
    [InlineData('C', 'B', null, "K", 0)]
    [InlineData('C', null, null, "K", 0)]
    [InlineData('Ç', null, null, "S", 0)]
    [InlineData('Ç', 'B', null, "S", 0)]
    [InlineData('B', null, null, "", 0)]
    public void Apply_ReturnsTokenAndSkip(char current, char? next, char? next2, string token, int skip)
    {
        LetterContext context = new LetterContext(0, null, null, current, next, next2);

        RuleResult result = CRule.Apply(context);

        Assert.Equal(token, result.Token);
        Assert.Equal(skip, result.Skip);
    }
}