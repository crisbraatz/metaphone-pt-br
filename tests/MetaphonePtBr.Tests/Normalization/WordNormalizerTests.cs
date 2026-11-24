using MetaphonePtBr.Normalization;

namespace MetaphonePtBr.Tests.Normalization;

public sealed class WordNormalizerTests
{
    [Theory]
    [InlineData("casa", "CASA")]
    [InlineData("CASA", "CASA")]
    [InlineData("CaSa", "CASA")]
    [InlineData("a", "A")]
    [InlineData("z", "Z")]
    [InlineData("Z", "Z")]
    [InlineData("ação", "AÇÃO")]
    [InlineData("pão", "PÃO")]
    [InlineData("àáâãéêíóôõúüç", "ÀÁÂÃÉÊÍÓÔÕÚÜÇ")]
    [InlineData("ÀÁÂÃÉÊÍÓÔÕÚÜÇ", "ÀÁÂÃÉÊÍÓÔÕÚÜÇ")]
    public void TryNormalize_ValidWord_ReturnsUppercase(string input, string expected)
    {
        bool result = WordNormalizer.TryNormalize(input, out string? normalized);

        Assert.True(result);
        Assert.Equal(expected, normalized);
    }

    [Theory]
    [InlineData("á", "Á")]
    [InlineData("à", "À")]
    [InlineData("â", "Â")]
    [InlineData("ã", "Ã")]
    [InlineData("é", "É")]
    [InlineData("ê", "Ê")]
    [InlineData("í", "Í")]
    [InlineData("ó", "Ó")]
    [InlineData("ô", "Ô")]
    [InlineData("õ", "Õ")]
    [InlineData("ú", "Ú")]
    [InlineData("ü", "Ü")]
    [InlineData("ç", "Ç")]
    [InlineData("ação", "AÇÃO")]
    public void TryNormalize_CombiningMarks_ReturnsPrecomposedUppercase(string input, string expected)
    {
        bool result = WordNormalizer.TryNormalize(input, out string? normalized);

        Assert.True(result);
        Assert.Equal(expected, normalized);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("duas palavras")]
    [InlineData("casa1")]
    [InlineData("guarda-chuva")]
    [InlineData("d'água")]
    [InlineData("1º")]
    [InlineData("1ª")]
    [InlineData("º")]
    [InlineData("ª")]
    [InlineData("ﬁm")]
    [InlineData("æ")]
    [InlineData("ñ")]
    [InlineData("ö")]
    [InlineData("è")]
    [InlineData("ı")]
    [InlineData("ſ")]
    [InlineData("ẽ")]
    [InlineData("́")]
    [InlineData("@")]
    [InlineData("[")]
    [InlineData("`")]
    [InlineData("{")]
    [InlineData("😀")]
    [InlineData("a\uD800")]
    public void TryNormalize_InvalidInput_ReturnsFalse(string? input)
    {
        bool result = WordNormalizer.TryNormalize(input, out string? normalized);

        Assert.False(result);
        Assert.Null(normalized);
    }

    [Theory]
    [InlineData('A', true)]
    [InlineData('Z', true)]
    [InlineData('a', true)]
    [InlineData('z', true)]
    [InlineData('Ç', true)]
    [InlineData('ü', true)]
    [InlineData('@', false)]
    [InlineData('[', false)]
    [InlineData('`', false)]
    [InlineData('{', false)]
    [InlineData('0', false)]
    [InlineData('Ñ', false)]
    public void IsSupportedLetter_ReturnsExpected(char letter, bool expected) =>
        Assert.Equal(expected, WordNormalizer.IsSupportedLetter(letter));
}