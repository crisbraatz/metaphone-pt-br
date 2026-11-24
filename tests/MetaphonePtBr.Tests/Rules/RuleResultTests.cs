using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class RuleResultTests
{
    [Fact]
    public void Empty_HasEmptyTokenAndZeroSkip()
    {
        Assert.Equal(string.Empty, RuleResult.Empty.Token);
        Assert.Equal(0, RuleResult.Empty.Skip);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Increment_EmitsNothingAndSkipsGivenCount(int skip)
    {
        RuleResult result = RuleResult.Increment(skip);

        Assert.Equal(string.Empty, result.Token);
        Assert.Equal(skip, result.Skip);
    }

    [Theory]
    [InlineData("K")]
    [InlineData("")]
    [InlineData("KS")]
    public void WithToken_KeepsTokenAndSkipsNothing(string token)
    {
        RuleResult result = RuleResult.WithToken(token);

        Assert.Equal(token, result.Token);
        Assert.Equal(0, result.Skip);
    }

    [Theory]
    [InlineData("S", 1)]
    [InlineData("X", 2)]
    [InlineData("", 1)]
    [InlineData("SK", 0)]
    public void WithTokenAndSkip_KeepsTokenAndSkip(string token, int skip)
    {
        RuleResult result = RuleResult.WithToken(token, skip);

        Assert.Equal(token, result.Token);
        Assert.Equal(skip, result.Skip);
    }
}