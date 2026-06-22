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
}
