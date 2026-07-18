namespace Shouldly.Tests.Strings.ShouldBeEmpty;

public class ShouldBeEmptyBasicScenario
{
    [Fact]
    public void ShouldBeEmptyBasicScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            "a".ShouldBeEmpty("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "".ShouldBeEmpty();
    }
}