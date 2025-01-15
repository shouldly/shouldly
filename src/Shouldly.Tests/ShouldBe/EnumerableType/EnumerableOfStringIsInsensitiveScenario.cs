namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class EnumerableOfStringIsInsensitiveScenario
{
    [Fact]
    public void EnumerableOfStringIsInsensitiveScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { "foo" }.ShouldBe(["different"], Case.Insensitive, "Some additional context"),

            errorWithSource:
            """
            new[] { "foo" }
                should be
            ["different"]
                but was (case insensitive comparison)
            ["foo"]
                difference
            [*"foo"*]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            ["foo"]
                should be
            ["different"]
                but was not (case insensitive comparison)
                difference
            [*"foo"*]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "foo" }.ShouldBe(["FOo"], Case.Insensitive);
    }
}