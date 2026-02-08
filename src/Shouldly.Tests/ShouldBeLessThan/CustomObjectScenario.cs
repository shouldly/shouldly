namespace Shouldly.Tests.ShouldBeLessThan;

public class CustomObjectScenario
{
    [Fact]
    public void CustomObjectScenarioShouldFail()
    {
        var customA = new Custom { Val = 1 };
        var customB = new Custom { Val = 1 };
        var comparer = new CustomComparer<Custom>();
        Verify.ShouldFail(() =>
            customA.ShouldBeLessThan(customB, comparer, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var customA = new Custom { Val = 2 };
        var customB = new Custom { Val = 1 };
        var comparer = new CustomComparer<Custom>();
        customB.ShouldBeLessThan(customA, comparer);
    }
}