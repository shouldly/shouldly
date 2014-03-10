using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class ActionDelegateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.Throw<NotImplementedException>(() => { });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should throw System.NotImplementedException but does not"; }
        }

        protected override void ShouldPass()
        {
            var ex = Should.Throw<NotImplementedException>(() => { throw new NotImplementedException(); });
            ex.ShouldBeOfType<NotImplementedException>();
            ex.ShouldNotBe(null);
        }
    }
}