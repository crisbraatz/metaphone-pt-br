using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class RuleEngineTests
{
    [Theory]
    [InlineData("CRISTOPHER")]
    [InlineData("CHRISTOFER")]
    [InlineData("CHRISTOFR")]
    [InlineData("CHRISTOPHER")]
    [InlineData("CHRISTOPHR")]
    [InlineData("CRISTOFER")]
    [InlineData("CRISTOFR")]
    [InlineData("CRISTOPHR")]
    public void Encode_CristopherSpellings_ReturnsKrstf(string word)
    {
        string token = RuleEngine.Encode(word);

        Assert.Equal("KRSTF", token);
    }

    [Theory]
    [InlineData("H")]
    [InlineData("RR")]
    [InlineData("LH")]
    public void Encode_AllBypassWords_ReturnsEmpty(string word)
    {
        string token = RuleEngine.Encode(word);

        Assert.Equal(string.Empty, token);
    }

    [Theory]
    [InlineData("AMIGO", "AMG")]
    [InlineData("BOLA", "BL")]
    [InlineData("CASA", "KZ")]
    [InlineData("ÇA", "S")]
    [InlineData("GATO", "GT")]
    [InlineData("HORA", "OR")]
    [InlineData("LATA", "LT")]
    [InlineData("NAVE", "NV")]
    [InlineData("PATO", "PT")]
    [InlineData("QUILO", "KL")]
    [InlineData("RATO", "T")]
    [InlineData("SAPO", "SP")]
    [InlineData("TATU", "TT")]
    [InlineData("WALTER", "VT")]
    [InlineData("XUXA", "XX")]
    [InlineData("YARA", "IR")]
    [InlineData("ZEBRA", "ZBR")]
    public void Encode_EveryDispatchBranch_ReturnsExpectedToken(string word, string expectedToken)
    {
        string token = RuleEngine.Encode(word);

        Assert.Equal(expectedToken, token);
    }

    [Fact]
    public void Encode_EmptyWord_ReturnsEmpty()
    {
        string token = RuleEngine.Encode(string.Empty);

        Assert.Equal(string.Empty, token);
    }
}