namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class ValueTypeScenario
{
    [Fact]
    public void ShouldFail()
    {
        const int subject = 5;
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(3, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        const int subject = 5;
        subject.ShouldBeEquivalentTo(5);
    }
}