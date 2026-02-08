namespace Shouldly.Tests.ShouldBeBoolean;

public class FalseScenario
{
    [Fact]
    public void FalseScenarioShouldFail()
    {
        const bool myValue = true;
        Verify.ShouldFail(() =>
            myValue.ShouldBeFalse("Some additional context"));
    }

    [Fact]
    public void NullableBooleanNullShouldFail()
    {
        bool? myValue = null;

        Verify.ShouldFail(() =>
            myValue.ShouldBeFalse("Some additional context"));
    }

    [Fact]
    public void NullableBooleanTrueShouldFail()
    {
        bool? myValue = true;

        Verify.ShouldFail(() =>
            myValue.ShouldBeFalse("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        false.ShouldBeFalse();
    }
}