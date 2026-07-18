namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class NullScenario
{
    [Fact]
    public void ShouldFailWhenActualIsNull()
    {
        string? subject = null;
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo("Hello", "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenExpectedIsNull()
    {
        const string subject = "Hello";
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(null, "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenBothAreNull()
    {
        string? subject = null;
        subject.ShouldBeEquivalentTo(null);
    }
}