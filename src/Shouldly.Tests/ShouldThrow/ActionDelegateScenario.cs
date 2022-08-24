namespace Shouldly.Tests.ShouldThrow
{
    public class ActionDelegateScenario
    {
        [Fact]
        public void ActionDelegateScenarioShouldFail()
        {
            var action = () => { };
            Verify.ShouldFail(() =>
action.ShouldThrow<NotImplementedException>("Some additional context"),

errorWithSource:
@"`action()`
    should throw
System.NotImplementedException
    but did not

Additional Info:
    Some additional context",

    errorWithoutSource:
@"delegate
    should throw
System.NotImplementedException
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ActionDelegateScenarioShouldFail_ExceptionTypePassedIn()
        {
            var action = () => { };
            Verify.ShouldFail(() =>
action.ShouldThrow(typeof(NotImplementedException), "Some additional context"),

errorWithSource:
@"`action()`
    should throw
System.NotImplementedException
    but did not

Additional Info:
    Some additional context",

    errorWithoutSource:
@"delegate
    should throw
System.NotImplementedException
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var action = new Action(() => throw new NotImplementedException());

            var ex = action.ShouldThrow<NotImplementedException>();
            ex.ShouldBeOfType<NotImplementedException>();
            ex.ShouldNotBe(null);
        }

        [Fact]
        public void ShouldPass_ExceptionTypePassedIn()
        {
            var action = new Action(() => throw new NotImplementedException());

            var ex = action.ShouldThrow(typeof(NotImplementedException));
            ex.ShouldBeOfType<NotImplementedException>();
            ex.ShouldNotBe(null);
        }
    }
}