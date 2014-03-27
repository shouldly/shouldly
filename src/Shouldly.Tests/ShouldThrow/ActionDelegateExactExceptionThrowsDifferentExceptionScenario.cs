using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class ActionDelegateExactExceptionThrowsDifferentExceptionScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.ThrowExact<ArgumentException>(() => { throw new ArgumentNullException(); });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should throw exact System.ArgumentException but was System.ArgumentNullException"; }
        }
    }
}