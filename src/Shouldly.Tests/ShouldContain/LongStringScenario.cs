namespace Shouldly.Tests.ShouldContain;

public class LongStringScenario
{
    private readonly string _target = new string('a', 110) + "zzzz";

    [Fact]
    public void LongStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            _target.ShouldContain("fff"));
    }

    [Fact]
    public void ShouldPass()
    {
        _target.ShouldContain("zzzz");
    }
}