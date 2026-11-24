using MetaphonePtBr.Rules;

namespace MetaphonePtBr.Tests.Rules;

public sealed class LetterContextTests
{
    [Fact]
    public void At_FirstLetter_HasNoPreviousLetters()
    {
        LetterContext context = LetterContext.At("CASA", 0);

        Assert.Equal(0, context.Index);
        Assert.Null(context.Previous2);
        Assert.Null(context.Previous);
        Assert.Equal('C', context.Current);
        Assert.Equal('A', context.Next);
        Assert.Equal('S', context.Next2);
    }

    [Fact]
    public void At_MiddleLetter_HasNeighboursOnBothSides()
    {
        LetterContext context = LetterContext.At("CASA", 2);

        Assert.Equal(2, context.Index);
        Assert.Equal('C', context.Previous2);
        Assert.Equal('A', context.Previous);
        Assert.Equal('S', context.Current);
        Assert.Equal('A', context.Next);
        Assert.Null(context.Next2);
    }

    [Fact]
    public void At_LastLetter_HasNoNextLetters()
    {
        LetterContext context = LetterContext.At("CASA", 3);

        Assert.Equal('A', context.Previous2);
        Assert.Equal('S', context.Previous);
        Assert.Equal('A', context.Current);
        Assert.Null(context.Next);
        Assert.Null(context.Next2);
    }

    [Fact]
    public void At_SingleLetterWord_HasNoNeighbours()
    {
        LetterContext context = LetterContext.At("H", 0);

        Assert.Null(context.Previous2);
        Assert.Null(context.Previous);
        Assert.Equal('H', context.Current);
        Assert.Null(context.Next);
        Assert.Null(context.Next2);
    }

    [Fact]
    public void At_ShortWord_ReturnsNullOutsideWord()
    {
        LetterContext context = LetterContext.At("AB", 1);

        Assert.Null(context.Next);
        Assert.Null(context.Next2);
        Assert.Equal('A', context.Previous);
        Assert.Null(context.Previous2);
    }

    [Theory]
    [InlineData('A')]
    [InlineData('E')]
    [InlineData('I')]
    [InlineData('O')]
    [InlineData('U')]
    public void IsVowel_Vowels_ReturnsTrue(char letter)
    {
        Assert.True(LetterContext.IsVowel(letter));
    }

    [Theory]
    [InlineData('B')]
    [InlineData('S')]
    [InlineData('Y')]
    [InlineData('W')]
    [InlineData('Ç')]
    [InlineData('Z')]
    public void IsVowel_Consonants_ReturnsFalse(char letter)
    {
        Assert.False(LetterContext.IsVowel(letter));
    }

    [Fact]
    public void IsVowel_Null_ReturnsFalse()
    {
        Assert.False(LetterContext.IsVowel(null));
    }
}