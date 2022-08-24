namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfObjectThrowsDifferentExceptionScenario
    {
        [Fact]
        public void NestedBlockLambdaWithoutAdditionalInformationsScenarioShouldFail()
        {
            Func<object> action = () => throw new RankException();
            Verify.ShouldFail(() =>
action.ShouldThrow<InvalidOperationException>("Some additional context"),

errorWithSource:
@"`action()`
    should throw
System.InvalidOperationException
    but threw
System.RankException

Additional Info:
    Some additional context",

errorWithoutSource:
@"delegate
    should throw
System.InvalidOperationException
    but threw
System.RankException

Additional Info:
    Some additional context");
        }

    [Fact]
    public void NestedBlockLambdaWithoutAdditionalInformationsScenarioShouldFail_ExceptionTypePassedIn()
    {
        Func<object> action = () => throw new RankException();
        Verify.ShouldFail(() =>
action.ShouldThrow(typeof(InvalidOperationException), "Some additional context"),

errorWithSource:
@"`action()`
    should throw
System.InvalidOperationException
    but threw
System.RankException

Additional Info:
    Some additional context",

errorWithoutSource:
@"delegate
    should throw
System.InvalidOperationException
    but threw
System.RankException

Additional Info:
    Some additional context");
    }
}
}