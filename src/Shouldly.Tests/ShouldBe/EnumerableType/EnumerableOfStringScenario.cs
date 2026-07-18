namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class EnumerableOfStringScenario
{
    [Fact]
    public void EnumerableOfStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { "foo" }.ShouldBe(["foo2"], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "foo" }.ShouldBe(["foo"]);
    }
}