namespace Shouldly.Tests.ShouldNotBeOneOf;

public class EnumScenario
{
    [Fact]
    public void EnumScenarioShouldFail()
    {
        var someFlags = SomeFlags.Val1;
        Verify.ShouldFail(() =>
                someFlags.ShouldNotBeOneOf(new[] { SomeFlags.Val1 }, "Some additional context"),

            errorWithSource:
            """
            someFlags
                should not be one of
            [SomeFlags.Val1]
                but was
            SomeFlags.Val1

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            SomeFlags.Val1
                should not be one of
            [SomeFlags.Val1]
                but was

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        SomeFlags.Val1.ShouldNotBeOneOf(SomeFlags.Val2, SomeFlags.Val3);
    }
}