namespace Shouldly.Tests.ShouldBeAssignableTo;

public class BasicTypesScenario
{
    [Fact]
    public void BasicTypesScenarioShouldFail()
    {
        var two = 2;
        Verify.ShouldFail(() =>
                two.ShouldBeAssignableTo<double>("Some additional context"),

            errorWithSource:
            """
            two
                should be assignable to
            System.Double
                but was
            System.Int32

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            2
                should be assignable to
            System.Double
                but was
            System.Int32

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeAssignableTo<int>();
    }
}