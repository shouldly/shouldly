namespace Shouldly.Tests.ShouldBeOneOf;

public class EnumScenario
{
    [Fact]
    public void EnumScenarioShouldFail()
    {
        var someFlags = SomeFlags.Val1;
        Verify.ShouldFail(() =>
            someFlags.ShouldBeOneOf([SomeFlags.Val2], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        SomeFlags.Val1.ShouldBeOneOf(SomeFlags.Val1, SomeFlags.Val2);
    }
}