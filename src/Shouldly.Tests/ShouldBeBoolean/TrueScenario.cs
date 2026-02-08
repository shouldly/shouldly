namespace Shouldly.Tests.ShouldBeBoolean;

public class TrueScenario
{
    [Fact]
    public void TrueScenarioShouldFail()
    {
        const bool myValue = false;
        Verify.ShouldFail(() =>
            myValue.ShouldBeTrue("Some additional context"));
    }

    [Fact]
    public void NullableBooleanNullShouldFail()
    {
        bool? myValue = null;

        Verify.ShouldFail(() =>
            myValue.ShouldBeTrue("Some additional context"));
    }

    [Fact]
    public void NullableBooleanFalseShouldFail()
    {
        bool? myValue = false;

        Verify.ShouldFail(() =>
            myValue.ShouldBeTrue("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        true.ShouldBeTrue();
    }
}