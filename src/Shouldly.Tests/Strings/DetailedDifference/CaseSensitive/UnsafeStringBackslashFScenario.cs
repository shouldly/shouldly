namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnsafeStringBackslashFScenario
{
    [Fact]
    public void UnsafeStringBackslashFScenarioShouldFail()
    {
        var str = "StringOne\fBackslashF";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone BackslashF"));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\fBackslashF".ShouldBe("StringOne\fBackslashF");
    }
}
