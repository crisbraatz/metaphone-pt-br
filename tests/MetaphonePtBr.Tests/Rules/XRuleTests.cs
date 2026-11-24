using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class XRuleTests
{
    [Theory]
    [InlineData(null, null, null, "X", 0)]
    [InlineData(null, 'A', null, "X", 0)]
    [InlineData('D', 'A', null, "X", 0)]
    [InlineData(null, 'E', 'A', "Z", 0)]
    [InlineData(null, 'E', 'E', "Z", 0)]
    [InlineData(null, 'E', 'I', "Z", 0)]
    [InlineData(null, 'E', 'O', "Z", 0)]
    [InlineData(null, 'E', 'U', "Z", 0)]
    [InlineData('B', 'E', 'E', "X", 1)]
    [InlineData('B', 'E', 'I', "X", 1)]
    [InlineData('B', 'E', 'A', "KS", 1)]
    [InlineData('B', 'E', 'O', "KS", 1)]
    [InlineData('B', 'E', 'U', "KS", 1)]
    [InlineData('B', 'E', 'C', "S", 1)]
    [InlineData('B', 'E', 'P', "S", 0)]
    [InlineData('B', 'E', 'T', "S", 0)]
    [InlineData('B', 'E', 'B', "KS", 0)]
    [InlineData('A', 'A', 'B', "X", 0)]
    [InlineData('C', 'A', 'B', "X", 0)]
    [InlineData('E', 'I', 'B', "X", 0)]
    [InlineData('G', 'O', 'B', "X", 0)]
    [InlineData('K', 'U', 'B', "X", 0)]
    [InlineData('L', 'A', 'B', "X", 0)]
    [InlineData('R', 'I', 'B', "X", 0)]
    [InlineData('X', 'O', 'B', "X", 0)]
    [InlineData('D', 'A', 'B', "KS", 0)]
    [InlineData('F', 'I', 'B', "KS", 0)]
    [InlineData('M', 'O', 'B', "KS", 0)]
    [InlineData('N', 'U', 'B', "KS", 0)]
    [InlineData('P', 'A', 'B', "KS", 0)]
    [InlineData('Q', 'I', 'B', "KS", 0)]
    [InlineData('S', 'O', 'B', "KS", 0)]
    [InlineData('T', 'U', 'B', "KS", 0)]
    [InlineData('V', 'A', 'B', "KS", 0)]
    [InlineData('Z', 'I', 'B', "KS", 0)]
    [InlineData('B', 'B', 'A', "X", 0)]
    public void Apply_ReturnsExpectedResult(char? previous2, char? previous, char? next, string expectedToken, int expectedSkip)
    {
        var context = new LetterContext(0, previous2, previous, 'X', next, null);

        RuleResult result = XRule.Apply(context);

        Assert.Equal(expectedToken, result.Token);
        Assert.Equal(expectedSkip, result.Skip);
    }
}