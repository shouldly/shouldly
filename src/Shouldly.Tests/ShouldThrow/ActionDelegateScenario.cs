using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class ActionDelegateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.Throw<NotImplementedException>(() => { }, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"Should throw System.NotImplementedException but does not
Additional Info:
Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            var ex = Should.Throw<NotImplementedException>(new Action(() => { throw new NotImplementedException(); }));
            ex.ShouldBeOfType<NotImplementedException>();
            ex.ShouldNotBe(null);
        }
    }
}