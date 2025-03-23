namespace Shouldly.Tests.ShouldContain;

public class LongStringScenario
{
    private readonly string _target = new string('a', 110) + "zzzz";

    [Fact]
    public void LongStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                _target.ShouldContain("fff"),

            errorWithSource:
            """
            _target
                should contain (case insensitive comparison)
            "fff"
                but was actually
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa..."
            """,

            errorWithoutSource:
            """
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa..."
                should contain (case insensitive comparison)
            "fff"
                but did not
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        _target.ShouldContain("zzzz");
    }
}