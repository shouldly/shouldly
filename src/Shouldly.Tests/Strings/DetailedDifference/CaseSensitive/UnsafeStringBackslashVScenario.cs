namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnsafeStringBackslashVScenario
{
    [Fact]
    public void UnsafeStringBackslashVScenarioShouldFail()
    {
        var str = "StringOne\vBackslashV";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone BackslashV"));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\vBackslashV".ShouldBe("StringOne\vBackslashV");
    }
}
