using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class ActionDelegateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var action = new Action(() => { throw new InvalidOperationException(); });
            action.ShouldNotThrow("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"action should not throw System.InvalidOperationException but does
Additional Info:
Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            var action = new Action(() => { });
            action.ShouldNotThrow();
        }
    }
}