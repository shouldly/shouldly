namespace Shouldly.Tests.Strings.ShouldNotEndWith;

public class BasicScenarioCaseInSensitive
{
    [Fact]
    public void BasicScenarioCaseInSensitiveShouldFail()
    {
        var str = "Cheese";
        Verify.ShouldFail(() =>
            str.ShouldNotEndWith("SE", "Some additional context", Case.Insensitive));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotEndWith("ze", Case.Insensitive);
    }
}