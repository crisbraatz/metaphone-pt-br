using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class VowelsRuleTests
{
    [Theory]
    [InlineData(0, 'A', "A")]
    [InlineData(0, 'E', "E")]
    [InlineData(0, 'I', "I")]
    [InlineData(0, 'O', "O")]
    [InlineData(0, 'U', "U")]
    [InlineData(1, 'A', "")]
    [InlineData(1, 'E', "")]
    [InlineData(1, 'I', "")]
    [InlineData(1, 'O', "")]
    [InlineData(1, 'U', "")]
    [InlineData(0, 'B', "")]
    public void Apply_ReturnsExpectedResult(int index, char current, string expectedToken)
    {
        var context = new LetterContext(index, null, null, current, null, null);

        RuleResult result = VowelsRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(0, result.Skip);
    }
}