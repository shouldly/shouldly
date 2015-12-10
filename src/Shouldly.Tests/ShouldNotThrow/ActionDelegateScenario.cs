using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class ActionDelegateScenario
    {
        [Fact]
        public void ActionDelegateScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    Should.NotThrow(new Action(() => { throw new InvalidOperationException(); }), "Some additional context"),

    errorWithSource:
    @"`new Action(() => { throw new InvalidOperationException(); })`
    should not throw but threw
System.InvalidOperationException
    with message
""Operation is not valid due to the current state of the object.""

Additional Info:
Some additional context",

    errorWithoutSource:
    @"`new Action(() => { throw new InvalidOperationException(); })`
    should not throw but threw
System.InvalidOperationException
    with message
""Operation is not valid due to the current state of the object.""

Additional Info:
Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var action = new Action(() => { });
            action.ShouldNotThrow();
        }
    }
}