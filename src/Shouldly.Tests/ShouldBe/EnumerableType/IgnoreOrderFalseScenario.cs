namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderFalseScenario
{
    [Fact]
    public void IgnoreOrderFalseScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new List<int> { 1, 4, 2 }.ShouldBe([1, 2, 3], false, "Some additional context"),

            errorWithSource:
            """
            new List<int> { 1, 4, 2 }
                should be
            [1, 2, 3]
                but was
            [1, 4, 2]
                difference
            [1, *4*, *2*]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 4, 2]
                should be
            [1, 2, 3]
                but was not
                difference
            [1, *4*, *2*]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new List<int> { 1, 2, 3 }.ShouldBe([1, 2, 3], ignoreOrder: false);
    }
}