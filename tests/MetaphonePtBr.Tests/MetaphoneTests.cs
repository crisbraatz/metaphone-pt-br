namespace MetaphonePtBr.Tests;

public sealed class MetaphoneTests
{
    [Theory]
    [InlineData("Cristopher")]
    [InlineData("Christofer")]
    [InlineData("Christofr")]
    [InlineData("Christopher")]
    [InlineData("Christophr")]
    [InlineData("Cristofer")]
    [InlineData("Cristofr")]
    [InlineData("Cristophr")]
    public void Encode_CristopherSpellings_ReturnsKrstf(string word)
    {
        Assert.Equal("KRSTF", Metaphone.Encode(word));
    }

    [Theory]
    [InlineData("cristopher", "KRSTF")]
    [InlineData("CRISTOPHER", "KRSTF")]
    [InlineData("CrIsToPhEr", "KRSTF")]
    public void Encode_DifferentCase_ReturnsSameToken(string word, string expected)
    {
        Assert.Equal(expected, Metaphone.Encode(word));
    }

    [Fact]
    public void Encode_PrecomposedAndCombiningForms_ReturnSameToken()
    {
        string precomposed = "ação";
        string combining = "ação";

        Assert.Equal(Metaphone.Encode(precomposed), Metaphone.Encode(combining));
    }

    [Fact]
    public void Encode_Null_ThrowsArgumentNullExceptionWithParamName()
    {
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => Metaphone.Encode(null!));

        Assert.Equal("word", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("casa azul")]
    [InlineData("abc1")]
    [InlineData("a-b")]
    [InlineData("d'agua")]
    [InlineData("æ")]
    [InlineData("º")]
    [InlineData("ñ")]
    [InlineData("\U0001F600")]
    public void Encode_InvalidWord_ThrowsArgumentExceptionWithParamName(string word)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => Metaphone.Encode(word));

        Assert.Equal("word", exception.ParamName);
        Assert.Contains("Value must be a single word", exception.Message);
    }

    [Fact]
    public void Encode_SingleBypassedLetter_ReturnsEmptyToken()
    {
        Assert.Equal(string.Empty, Metaphone.Encode("H"));
    }

    [Fact]
    public void TryEncode_Null_ReturnsFalseWithNullToken()
    {
        bool success = Metaphone.TryEncode(null, out string? token);

        Assert.False(success);
        Assert.Null(token);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("casa azul")]
    [InlineData("abc1")]
    [InlineData("a-b")]
    [InlineData("d'agua")]
    [InlineData("æ")]
    [InlineData("º")]
    [InlineData("ñ")]
    [InlineData("\U0001F600")]
    public void TryEncode_InvalidWord_ReturnsFalseWithNullTokenWithoutThrowing(string word)
    {
        bool success = Metaphone.TryEncode(word, out string? token);

        Assert.False(success);
        Assert.Null(token);
    }

    [Theory]
    [InlineData("Cristopher")]
    [InlineData("cristopher")]
    [InlineData("Christofer")]
    [InlineData("H")]
    [InlineData("RR")]
    [InlineData("LH")]
    [InlineData("ação")]
    [InlineData("ação")]
    public void TryEncode_ValidWord_ReturnsTrueWithSameTokenAsEncode(string word)
    {
        bool success = Metaphone.TryEncode(word, out string? token);

        Assert.True(success);
        Assert.Equal(Metaphone.Encode(word), token);
    }
}