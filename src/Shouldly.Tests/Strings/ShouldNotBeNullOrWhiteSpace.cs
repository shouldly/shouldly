namespace Shouldly.Tests.Strings;

public class ShouldNotBeNullOrWhiteSpace
{
    [Fact]
    public void WhiteSpaceShouldFailShowingTheValue()
    {
        var blank = "  ";

        Should.Throw<ShouldAssertException>(() => blank.ShouldNotBeNullOrWhiteSpace("Some additional context")).Message
            .ShouldBe(
                """
                blank ("  ")
                    should not be null or white space

                Additional Info:
                    Some additional context
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void NullShouldFail()
    {
        string? missing = null;

        Should.Throw<ShouldAssertException>(() => missing.ShouldNotBeNullOrWhiteSpace()).Message
            .ShouldBe(
                """
                missing (null)
                    should not be null or white space
                """, StringCompareShould.IgnoreLineEndings);
    }

    [Fact]
    public void TextShouldPass()
    {
        "a".ShouldNotBeNullOrWhiteSpace();
    }
}
