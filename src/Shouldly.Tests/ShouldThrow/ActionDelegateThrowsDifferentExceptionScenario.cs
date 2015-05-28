using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class ActionDelegateThrowsDifferentExceptionScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Action action = () => { throw new RankException(); };
            action.ShouldThrow<InvalidOperationException>("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"action should throw System.InvalidOperationException but was System.RankException
Additional Info:
Some additional context"; }
        }
    }
}