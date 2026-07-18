namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public class UnsafeStringBackslashVScenario
{
    [Fact]
    public void UnsafeStringBackslashVScenarioShouldFail()
    {
        var str = "StringOne\vBackslashV";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone BackslashV", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\vBackslashV".ShouldBe("Stringone\vBackslashV", StringCompareShould.IgnoreCase);
    }
}
