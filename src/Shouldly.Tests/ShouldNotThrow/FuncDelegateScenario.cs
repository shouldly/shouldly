using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class FuncDelegateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.NotThrow(new Func<int>(() => { throw new InvalidOperationException(); }), "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"Should not throw System.InvalidOperationException but does
Additional Info:
Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            Should.NotThrow(() => 1).ShouldBe(1);
        }
    }
}