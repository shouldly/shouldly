namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class EnumerableOfStringIsInsensitiveScenario
{
    [Fact]
    public void EnumerableOfStringIsInsensitiveScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { "foo" }.ShouldBe(["different"], Case.Insensitive, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "foo" }.ShouldBe(["FOo"], Case.Insensitive);
    }
}