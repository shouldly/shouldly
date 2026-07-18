namespace Shouldly.Tests.ShouldNotContain;

public class StringContainsCharScenario
{
    private const string Target = "Foo";

    [Fact]
    public void StringContainsCharScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            Target.ShouldNotContain('F', "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        Target.ShouldNotContain('B');
    }
}