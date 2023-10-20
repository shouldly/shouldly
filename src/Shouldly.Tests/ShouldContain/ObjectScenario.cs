namespace Shouldly.Tests.ShouldContain;

public class ObjectScenario
{
    [Fact]
    public void ObjectScenarioShouldFail()
    {
        var a = new object();
        var b = new object();
        var c = new object();
        var d = new object();
        var target = new[] { a, b, c };

        Verify.ShouldFail(() =>
                target.ShouldContain(d, "Some additional context"),

            errorWithSource:
            """
            target
                should contain
            System.Object (000000)
                but was actually
            [System.Object (000000), System.Object (000000), System.Object (000000)]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [System.Object (000000), System.Object (000000), System.Object (000000)]
                should contain
            System.Object (000000)
                but did not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var a = new object();
        var b = new object();
        var c = new object();
        var target = new[] { a, b, c };
        target.ShouldContain(b);
    }
}