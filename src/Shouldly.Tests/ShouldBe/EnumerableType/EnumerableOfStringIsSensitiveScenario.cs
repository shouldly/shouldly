namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class EnumerableOfStringIsSensitiveScenario
{
    [Fact]
    public void EnumerableOfStringIsSensitiveScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { "foo" }.ShouldBe(["FoO"], Case.Sensitive, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "foo" }.ShouldBe(["foo"], Case.Sensitive);
    }
}