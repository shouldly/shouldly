namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnicodeScenarios
{
    [Fact]
    public void Emoji()
    {
        var str = "hello 🎉 world";
        Verify.ShouldFail(() =>
            str.ShouldBe("hello 🎊 world"));
    }

    [Fact]
    public void MultipleEmoji()
    {
        var str = "I ❤️ cats 🐱";
        Verify.ShouldFail(() =>
            str.ShouldBe("I 💙 dogs 🐶"));
    }

    [Fact]
    public void CjkCharacters()
    {
        var str = "hello 世界";
        Verify.ShouldFail(() =>
            str.ShouldBe("hello 世间"));
    }

    [Fact]
    public void CombiningCharacters()
    {
        // e + combining acute accent (U+0301) vs precomposed é (U+00E9)
        var str = "cafe\u0301";
        Verify.ShouldFail(() =>
            str.ShouldBe("caf\u00e9"));
    }

    [Fact]
    public void ZeroWidthCharacters()
    {
        // String with zero-width space (U+200B) that's invisible
        var str = "foo\u200Bbar";
        Verify.ShouldFail(() =>
            str.ShouldBe("foobar"));
    }

    [Fact]
    public void SurrogatePairEmoji()
    {
        // Emoji with skin tone modifier (multi-codepoint)
        var str = "wave 👋🏽 hi";
        Verify.ShouldFail(() =>
            str.ShouldBe("wave 👋🏻 hi"));
    }

    [Fact]
    public void MixedScripts()
    {
        var str = "hello мир 世界";
        Verify.ShouldFail(() =>
            str.ShouldBe("hello мир 世间"));
    }

    [Fact]
    public void FlagEmoji()
    {
        // Flag emoji are regional indicator pairs
        var str = "Visit 🇫🇷 France";
        Verify.ShouldFail(() =>
            str.ShouldBe("Visit 🇩🇪 Germany"));
    }

    [Fact]
    public void ArabicText()
    {
        var str = "hello مرحبا world";
        Verify.ShouldFail(() =>
            str.ShouldBe("hello مرحبآ world"));
    }

    [Fact]
    public void ActualNewlineVsLiteralBackslashRN()
    {
        // Actual \r\n characters vs the literal text "\r\n"
        var str = "line1\r\nline2";
        Verify.ShouldFail(() =>
            str.ShouldBe(@"line1\r\nline2"));
    }

    [Fact]
    public void ControlPicturesEscapeStyle()
    {
        var previous = ShouldlyConfiguration.EscapeStyle;
        try
        {
            ShouldlyConfiguration.EscapeStyle = EscapeStyle.ControlPictures;
            var str = "line1\r\nline2";
            Verify.ShouldFail(() =>
                str.ShouldBe(@"line1\r\nline2"));
        }
        finally
        {
            ShouldlyConfiguration.EscapeStyle = previous;
        }
    }
}
