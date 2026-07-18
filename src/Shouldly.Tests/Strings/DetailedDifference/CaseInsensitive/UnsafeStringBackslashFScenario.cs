namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public class UnsafeStringBackslashFScenario
{
    [Fact]
    public void UnsafeStringBackslashFScenarioShouldFail()
    {
        var str = "StringOne\fBackslashF";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone BackslashF", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\fBackslashF".ShouldBe("Stringone\fBackslashF", StringCompareShould.IgnoreCase);
    }
}
