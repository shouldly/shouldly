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
    public void CaseMismatchBeyondTheExcerptShouldFail()
    {
        // The excerpt is centred on the match that differs only in case, rather than cut from the start, where
        // it would show none of it.
        var log = new string('a', 300) + "Warning: x" + new string('b', 300);

        Verify.ShouldFail(() =>
            log.ShouldContain("warning:"));
    }

    [Fact]
    public void CaseMismatchAtTheEndShowsTheLastOfTheString()
    {
        var log = new string('a', 300) + "Warning: x";

        Verify.ShouldFail(() =>
                log.ShouldContain("warning:"),
            $"""
             log
                 should contain
             "warning:"
                 but was actually
             "...{new string('a', 90)}Warning: x"
                 found differing only in case at index 300
             "Warning:"
             """);
    }

    [Fact]
    public void ShouldPass()
    {
        _target.ShouldContain("zzzz");
    }
}