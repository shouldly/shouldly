namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class EnumerableOfStringScenario
{
    [Fact]
    public void EnumerableOfStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { "foo" }.ShouldBe(new[] { "foo2" }, "Some additional context"),

            errorWithSource:
            @"new[] { ""foo"" }
    should be
[""foo2""]
    but was (case sensitive comparison)
[""foo""]
    difference
[*""foo""*]

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[""foo""]
    should be
[""foo2""]
    but was not (case sensitive comparison)
    difference
[*""foo""*]

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "foo" }.ShouldBe(new[] { "foo" });
    }
}