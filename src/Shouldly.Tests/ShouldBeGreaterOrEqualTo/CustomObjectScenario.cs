namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo;

public class CustomObjectScenario
{
    [Fact]
    public void CustomObjectScenarioShouldFail()
    {
        var customA = new Custom { Val = 1 };
        var customB = new Custom { Val = 2 };
        var comparer = new CustomComparer<Custom>();
        Verify.ShouldFail(() =>
                customA.ShouldBeGreaterThanOrEqualTo(customB, comparer, "Some additional context"),

            errorWithSource:
            """
            customA
                should be greater than or equal to
            Shouldly.Tests.TestHelpers.Custom (000000)
                but was
            Shouldly.Tests.TestHelpers.Custom (000000)

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Shouldly.Tests.TestHelpers.Custom (000000)
                should be greater than or equal to
            Shouldly.Tests.TestHelpers.Custom (000000)
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var customA = new Custom { Val = 1 };
        var customB = new Custom { Val = 1 };
        var comparer = new CustomComparer<Custom>();
        customA.ShouldBeGreaterThanOrEqualTo(customB, comparer);
    }
}