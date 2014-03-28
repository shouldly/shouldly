using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrowExact
{
    class ActionDelegateExactExceptionScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.ThrowExact<ArgumentException>(() => { });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should throw exact System.ArgumentException but does not"; }
        }

        protected override void ShouldPass()
        {
            var ex = Should.ThrowExact<ArgumentException>(() => { throw new ArgumentException(); });
            ex.ShouldBeOfType<ArgumentException>();
            ex.ShouldNotBe(null);
        }
    }
}