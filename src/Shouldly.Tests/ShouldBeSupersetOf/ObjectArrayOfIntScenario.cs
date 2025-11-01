namespace Shouldly.Tests.ShouldBeSupersetOf;

public class ObjectArrayOfIntScenario
{
    [Fact]
    public void ObjectArrayOfIntScenarioShouldFail()
    {
        var arr = new object[] { 1, 2 };
        var arr2 = new object[] { 1, 2, 3 };

        Verify.ShouldFail(() =>
                arr.ShouldBeSupersetOf(arr2, "Some additional context"),

            errorWithSource:
            """
            arr
                should be superset of
            [1, 2, 3]
                but
            [3]
                is outside superset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 2]
                should be superset of
            [1, 2, 3]
                but
            [3]
                is outside superset

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var arr = new object[] { 1, 2, 3, 4 };
        var arr2 = new object[] { 1, 2, 3 };

        arr.ShouldBeSupersetOf(arr2);
    }
}