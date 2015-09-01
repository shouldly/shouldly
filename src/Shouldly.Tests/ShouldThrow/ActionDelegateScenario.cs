using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class ActionDelegateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Action action = () => { };
            action.ShouldThrow<NotImplementedException>("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"`action()` should throw System.NotImplementedException but did not
Additional Info:
Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            var action = new Action(() => { throw new NotImplementedException(); });

            var ex = action.ShouldThrow<NotImplementedException>();
            ex.ShouldBeOfType<NotImplementedException>();
            ex.ShouldNotBe(null);
        }
    }
}