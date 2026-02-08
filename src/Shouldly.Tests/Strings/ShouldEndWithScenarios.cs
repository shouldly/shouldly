namespace Shouldly.Tests.Strings;

public class ShouldEndWithScenarios
{
    [Fact]
    public void ShouldEndWithCaseInsensitiveShouldFail()
    {
        // ReSharper disable once RedundantArgumentDefaultValue
        var cheeseVar = "Cheese";
        Verify.ShouldFail(() =>
            cheeseVar.ShouldEndWith("ze", Case.Insensitive, "Some additional context"));
    }

    [Fact]
    public void ShouldEndWithCaseSensitiveShouldFail()
    {
        var cheeseVar = "Cheese";
        Verify.ShouldFail(() =>
            cheeseVar.ShouldEndWith("Se", Case.Sensitive, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldEndWith("se");
        "Cheese".ShouldEndWith("SE");
        "Cheese".ShouldEndWith("Se");
        "Cheese".ShouldEndWith("se", Case.Sensitive);
    }
}