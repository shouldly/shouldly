namespace Shouldly.Tests.Strings.ShouldNotEndWith;

public class BasicScenarioCaseInSensitive
{
    [Fact]
    public void BasicScenarioCaseInSensitiveShouldFail()
    {
        var str = "Cheese";
        Verify.ShouldFail(() =>
            str.ShouldNotEndWith("SE", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotEndWith("ze", Case.Insensitive);
    }
}