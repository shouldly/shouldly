namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public class UnsafeStringBackslashAScenario
{
    [Fact]
    public void UnsafeStringBackslashAScenarioShouldFail()
    {
        var str = "StringOne\aBackslashA";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone BackslashA", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\aBackslashA".ShouldBe("Stringone\aBackslashA", StringCompareShould.IgnoreCase);
    }
}