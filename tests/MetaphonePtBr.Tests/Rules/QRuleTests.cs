using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class QRuleTests
{
    [Theory]
    [InlineData(0, null, null)]
    [InlineData(1, 'A', 'U')]
    [InlineData(2, 'U', 'A')]
    [InlineData(1, 'B', 'R')]
    public void Apply_AlwaysReturnsK(int index, char? previous, char? next)
    {
        var context = new LetterContext(index, null, previous, 'Q', next, null);

        RuleResult result = QRule.Apply(context);

        Assert.Equal("K", result.Token);
        Assert.Equal(0, result.Skip);
    }
}