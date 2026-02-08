namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public class UnsafeStringNewLineScenario
{
    [Fact]
    public void UnsafeStringNewLineScenarioShouldFail()
    {
        var str = "StringOneNoNewLine";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone\r\nNewLine", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne\r\nNewline".ShouldBe("Stringone\r\nNewline", StringCompareShould.IgnoreCase);
    }
}