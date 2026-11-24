using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class HRuleTests
{
    [Theory]
    [InlineData(0, 'A', "A", 1)]
    [InlineData(0, 'E', "E", 1)]
    [InlineData(0, 'I', "I", 1)]
    [InlineData(0, 'O', "O", 1)]
    [InlineData(0, 'U', "U", 1)]
    [InlineData(0, null, "", 0)]
    [InlineData(0, 'B', "", 0)]
    [InlineData(1, null, "", 0)]
    [InlineData(1, 'A', "", 0)]
    [InlineData(1, 'B', "", 0)]
    public void Apply_ReturnsTokenAndSkip(int index, char? next, string token, int skip)
    {
        LetterContext context = new LetterContext(index, null, null, 'H', next, null);

        RuleResult result = HRule.Apply(context);

        Assert.Equal(token, result.Token);
        Assert.Equal(skip, result.Skip);
    }
}