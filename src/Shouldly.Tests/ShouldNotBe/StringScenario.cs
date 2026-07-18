namespace Shouldly.Tests.ShouldNotBe;

public class StringScenario
{
    private const string ThisOtherString = "this other string";
    private const string ThisString = "this string";

    [Fact]
    public void StringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            ThisString.ShouldNotBe(ThisString));
    }

    [Fact]
    public void ShouldPass()
    {
        ThisString.ShouldNotBe(ThisOtherString);
    }
}