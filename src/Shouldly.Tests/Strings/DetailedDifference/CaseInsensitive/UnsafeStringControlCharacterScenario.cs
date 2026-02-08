namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public class UnsafeStringControlCharacterScenario
{
    [Fact]
    public void UnsafeStringControlCharacterScenarioShouldFail()
    {
        var str = "StringOne\0ControlChar";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone\u0001ControlChar", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\u0000ControlChar".ShouldBe("Stringone\u0000ControlChar", StringCompareShould.IgnoreCase);
    }
}
