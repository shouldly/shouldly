using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class ActionDelegateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.NotThrow(new Action(() => { throw new InvalidOperationException(); }), "Some additional context");            
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new Action(() => { throw new InvalidOperationException(); }) should not throw but threw System.InvalidOperationException with message \"Operation is not valid due to the current state of the object.\"" +
                        "Additional Info:" +
                        "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            var action = new Action(() => { });
            action.ShouldNotThrow();
        }
    }
}