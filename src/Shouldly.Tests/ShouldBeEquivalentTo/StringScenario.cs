namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class StringScenario
{
    [Fact]
    public void ShouldFailWhenStringIsDifferent()
    {
        const string subject = "Hello";
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo("Goodbye", "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenCaseIsDifferent()
    {
        const string subject = "Hello";
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo("HELLO", "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenCaseMatches()
    {
        const string subject = "Hello";
        subject.ShouldBeEquivalentTo("Hello");
    }
}