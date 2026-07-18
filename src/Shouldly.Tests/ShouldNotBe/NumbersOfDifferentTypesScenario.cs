namespace Shouldly.Tests.ShouldNotBe;

public class NumbersOfDifferentTypesScenario
{
    [Fact]
    public void NumbersOfDifferentTypesScenarioShouldFail()
    {
        const long aLong = 1L;
        Verify.ShouldFail(() =>
            aLong.ShouldNotBe(1));
    }

    [Fact]
    public void ShouldPass()
    {
        1L.ShouldNotBe(2);
    }
}