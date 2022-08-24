namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class ValueTypeScenario
{
    [Fact]
    public void ShouldFail()
    {
        const int subject = 5;
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(3, "Some additional context"),

            errorWithSource:
            @"Comparing object equivalence, at path:
subject [System.Int32]

    Expected value to be
3
    but was
5

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"Comparing object equivalence, at path:
<root> [System.Int32]

    Expected value to be
3
    but was
5

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        const int subject = 5;
        subject.ShouldBeEquivalentTo(5);
    }
}