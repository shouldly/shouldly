namespace Shouldly.Tests.ShouldContain;

public class StringContainsCharScenario
{
    private const string Target = "Foo";

    [Fact]
    public void StringContainsCharScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            Target.ShouldContain('B', "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        Target.ShouldContain('F');
    }
}