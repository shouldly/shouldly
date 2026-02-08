namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnsafeStringControlCharacterScenario
{
    [Fact]
    public void UnsafeStringControlCharacterScenarioShouldFail()
    {
        var str = "StringOne\u0000ControlChar";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone\u0001ControlChar"));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\u0000ControlChar".ShouldBe("StringOne\u0000ControlChar");
    }
}
