namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnsafeStringTabScenario
{
    [Fact]
    public void UnsafeStringTabScenarioShouldFail()
    {
        var str = "StringOne\tTab";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone Tab"));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\tTab".ShouldBe("StringOne\tTab");
    }
}
