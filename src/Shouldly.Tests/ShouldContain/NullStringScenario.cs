namespace Shouldly.Tests.ShouldContain;

public class NullStringScenario
{
    [Fact]
    public void NullStringScenarioShouldFail()
    {
        string actual = null!;

        Verify.ShouldFail(() =>
            actual.ShouldContain("legendary"));
    }

    [Fact]
    public void NullStringShouldNotContainShouldPass()
    {
        string actual = null!;

        actual.ShouldNotContain("legendary");
    }
}
