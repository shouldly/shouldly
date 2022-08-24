using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfObjectScenario
    {
        [Fact]
        public void FuncOfObjectScenarioShouldFail()
        {
            Func<object> action = () => 1;
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
        public void FuncOfObjectScenarioShouldFail_ExceptionTypePassedIn()
        {
            Func<object> action = () => 1;
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
            var action = new Func<object>(() => throw new NotImplementedException());

            var ex = action.ShouldThrow<NotImplementedException>();
            ex.ShouldBeOfType<NotImplementedException>();
            ex.ShouldNotBe(null);
        }

        [Fact]
        public void ShouldPass_ExceptionTypePassedIn()
        {
            var action = new Func<object>(() => throw new NotImplementedException());

            var ex = action.ShouldThrow(typeof(NotImplementedException));
            ex.ShouldBeOfType<NotImplementedException>();
            ex.ShouldNotBe(null);
        }
    }
}