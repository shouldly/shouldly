namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnsafeStringBackslashAScenario
{
    [Fact]
    public void UnsafeStringBackslashAScenarioShouldFail()
    {
        var str = "StringOne\aBackslashA";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone BackslashA"));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\aBackslashA".ShouldBe("StringOne\aBackslashA");
    }
}