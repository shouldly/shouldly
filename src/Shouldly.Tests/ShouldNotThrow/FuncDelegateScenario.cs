using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class FuncDelegateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var action = new Func<int>(() => { throw new InvalidOperationException(); });
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
            var action = new Func<int>(() => 1);
            action.ShouldNotThrow().ShouldBe(1);
        }
    }
}