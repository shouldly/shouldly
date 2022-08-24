namespace Shouldly.Tests.Strings.ShouldBeEmpty;

public class ShouldBeEmptyBasicScenario
{
    [Fact]
    public void ShouldBeEmptyBasicScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                "a".ShouldBeEmpty("Some additional context"),

            errorWithSource:
            @"""a""
    should be empty but was
""a""

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"""a""
    should be empty but was not empty

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        "".ShouldBeEmpty();
    }
}