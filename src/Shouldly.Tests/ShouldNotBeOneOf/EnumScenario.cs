namespace Shouldly.Tests.ShouldNotBeOneOf;

public class EnumScenario
{
    [Fact]
    public void EnumScenarioShouldFail()
    {
        var someFlags = SomeFlags.Val1;
        Verify.ShouldFail(() =>
            someFlags.ShouldNotBeOneOf([SomeFlags.Val1], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        SomeFlags.Val1.ShouldNotBeOneOf(SomeFlags.Val2, SomeFlags.Val3);
    }
}