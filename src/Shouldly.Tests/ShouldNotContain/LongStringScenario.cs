namespace Shouldly.Tests.ShouldNotContain;

public class LongStringScenario
{
    private readonly string _target = new string('a', 110) + "zzzz";

    [Fact]
    public void LongStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            _target.ShouldNotContain("zzzz"));
    }

    [Fact]
    public void ShouldPass()
    {
        _target.ShouldNotContain("fff");
    }
}