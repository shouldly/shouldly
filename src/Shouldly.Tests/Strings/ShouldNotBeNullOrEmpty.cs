namespace Shouldly.Tests.Strings;

public class ShouldNotBeNullOrEmpty
{
    [Fact]
    public void SingleLetterShouldFail()
    {
        Verify.ShouldFail(() =>
            "".ShouldNotBeNullOrEmpty("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        " ".ShouldNotBeNullOrEmpty();
    }
}