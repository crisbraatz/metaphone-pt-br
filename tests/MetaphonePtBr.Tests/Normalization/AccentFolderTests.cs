using MetaphonePtBr.Normalization;

namespace MetaphonePtBr.Tests.Normalization;

public sealed class AccentFolderTests
{
    [Theory]
    [InlineData("Á", "A")]
    [InlineData("À", "A")]
    [InlineData("Â", "A")]
    [InlineData("Ã", "A")]
    [InlineData("É", "E")]
    [InlineData("Ê", "E")]
    [InlineData("Í", "I")]
    [InlineData("Ó", "O")]
    [InlineData("Ô", "O")]
    [InlineData("Õ", "O")]
    [InlineData("Ú", "U")]
    [InlineData("Ü", "U")]
    public void Fold_AccentedLetter_ReturnsBaseLetter(string input, string expected)
    {
        Assert.Equal(expected, AccentFolder.Fold(input));
    }

    [Fact]
    public void Fold_Cedilla_IsKept()
    {
        Assert.Equal("Ç", AccentFolder.Fold("Ç"));
    }

    [Theory]
    [InlineData("CASA", "CASA")]
    [InlineData("AEIOUBCDFGHJKLMNPQRSTVWXYZ", "AEIOUBCDFGHJKLMNPQRSTVWXYZ")]
    public void Fold_PlainWord_IsUnchanged(string input, string expected)
    {
        Assert.Equal(expected, AccentFolder.Fold(input));
    }

    [Fact]
    public void Fold_AccentedWord_FoldsAccentedLettersAndKeepsCedilla()
    {
        // ÇÃO -> ÇAO: the cedilla stays, the tilde folds to A.
        Assert.Equal("ÇAO", AccentFolder.Fold("ÇÃO"));
    }
}