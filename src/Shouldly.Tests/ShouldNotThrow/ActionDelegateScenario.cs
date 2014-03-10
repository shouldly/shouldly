using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class ActionDelegateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.NotThrow(() => { throw new InvalidOperationException(); });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should not throw System.InvalidOperationException but does"; }
        }

        protected override void ShouldPass()
        {
            Should.NotThrow(() => { });
        }
    }
}