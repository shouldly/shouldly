namespace Shouldly.Tests.ShouldNotContain;

public class LongStringScenario
{
    private readonly string _target = new string('a', 110) + "zzzz";

    [Fact]
    public void LongStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                _target.ShouldNotContain("zzzz"),

            errorWithSource:
            @"_target
    should not contain (case insensitive comparison)
""zzzz""
    but was actually
""aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa...""",

            errorWithoutSource:
            @"""aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa...""
    should not contain (case insensitive comparison)
""zzzz""
    but did");
    }

    [Fact]
    public void ShouldPass()
    {
        _target.ShouldNotContain("fff");
    }
}