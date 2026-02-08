namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public class UnsafeStringTabScenario
{
    [Fact]
    public void UnsafeStringTabScenarioShouldFail()
    {
        var str = "StringOne\tTab";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone Tab", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\tTab".ShouldBe("Stringone\tTab", StringCompareShould.IgnoreCase);
    }
}
