namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnsafeStringNewLineScenario
{
    [Fact]
    public void UnsafeStringNewLineScenarioShouldFail()
    {
        var str = "StringOneNoNewLine";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone\r\nNewLine"));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\r\nNewLine".ShouldBe("StringOne\r\nNewLine");
    }
}