namespace Shouldly.Tests.ShouldHaveCount;

public class ArrayScenario
{
    [Fact]
    public void ArrayScenarioShouldFail()
    {
        var testList = new[] { 1, 2 };
        Verify.ShouldFail(() =>
                testList.ShouldHaveCount(3, "Some additional context"),

            errorWithSource:
            """
            testList
                should have 3 items but had
            2
                items and was
            [1, 2]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 2]
                should have 3 items but had
            2
                items

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ArrayScenarioShouldFail_With_Single_Expected()
    {
        Verify.ShouldFail(() =>
                new[] { 1, 2 }.ShouldHaveCount(1, "Some additional context"),

            errorWithSource:
            """
            new[] { 1, 2 }
                should have 1 item but had
            2
                items and was
            [1, 2]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 2]
                should have 1 item but had
            2
                items

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ArrayScenarioShouldFail_With_Single_Actual()
    {
        Verify.ShouldFail(() =>
                new[] { 1 }.ShouldHaveCount(3, "Some additional context"),

            errorWithSource:
            """
            new[] { 1 }
                should have 3 items but had
            1
                item and was
            [1]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1]
                should have 3 items but had
            1
                item

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1 }.ShouldHaveCount(1);
    }
}