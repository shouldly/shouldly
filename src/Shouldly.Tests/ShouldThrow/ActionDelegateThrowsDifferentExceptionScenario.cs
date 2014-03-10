using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class ActionDelegateThrowsDifferentExceptionScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.Throw<InvalidOperationException>(() => { throw new RankException(); });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should throw System.InvalidOperationException but was System.RankException"; }
        }
    }
}