namespace Shouldly.Tests.Strings.ShouldStartWith;

public class ShouldBeCaseSensitiveByDefault
{
    [Fact]
    public void ShouldBeCaseSensitiveByDefaultShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldStartWith("ch"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldStartWith("Ch");
        "Cheese".ShouldStartWith("CH", Case.Insensitive);
    }
}