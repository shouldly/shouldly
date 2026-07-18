namespace Shouldly.Tests.ShouldThrow;

public class ActionDelegateThrowsDifferentExceptionScenario
{
    [Fact]
    public void NestedBlockLambdaWithoutAdditionalInformationsScenarioShouldFail()
    {
        Action action = () => throw new RankException();
        Verify.ShouldFail(() =>
            action.ShouldThrow<InvalidOperationException>("Some additional context"));
    }

    [Fact]
    public void NestedBlockLambdaWithoutAdditionalInformationsScenarioShouldFail_ExceptionTypePassedIn()
    {
        Action action = () => throw new RankException();
        Verify.ShouldFail(() =>
            action.ShouldThrow(typeof(InvalidOperationException), "Some additional context"));
    }
}