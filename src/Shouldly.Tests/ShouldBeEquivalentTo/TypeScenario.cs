namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class TypeScenario
{
    [Fact]
    public void ShouldFail()
    {
        const string subject = "Hello";
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(5, "Some additional context"));
    }
}