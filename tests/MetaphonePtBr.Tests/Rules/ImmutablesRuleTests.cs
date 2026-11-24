using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class ImmutablesRuleTests
{
    [Theory]
    [InlineData('A', "")]
    [InlineData('B', "B")]
    [InlineData('C', "")]
    [InlineData('D', "D")]
    [InlineData('E', "")]
    [InlineData('F', "F")]
    [InlineData('G', "")]
    [InlineData('H', "")]
    [InlineData('I', "")]
    [InlineData('J', "J")]
    [InlineData('K', "K")]
    [InlineData('L', "")]
    [InlineData('M', "M")]
    [InlineData('N', "")]
    [InlineData('O', "")]
    [InlineData('P', "")]
    [InlineData('Q', "")]
    [InlineData('R', "")]
    [InlineData('S', "")]
    [InlineData('T', "")]
    [InlineData('U', "")]
    [InlineData('V', "V")]
    [InlineData('W', "")]
    [InlineData('X', "")]
    [InlineData('Y', "")]
    [InlineData('Z', "")]
    public void Apply_ReturnsTokenAndSkipZero(char current, string token)
    {
        LetterContext context = new LetterContext(0, null, null, current, null, null);

        RuleResult result = ImmutablesRule.Apply(context);

        Assert.Equal(token, result.Token);
        Assert.Equal(0, result.Skip);
    }
}