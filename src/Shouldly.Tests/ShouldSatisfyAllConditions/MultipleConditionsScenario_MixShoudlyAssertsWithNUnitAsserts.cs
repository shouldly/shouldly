using NUnit.Framework;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldSatisfyAllConditions
{
    public class MultipleConditionsScenario_MixShouldlyAssertsWithNUnitAsserts : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            int result = 4;
            result.ShouldSatisfyAllConditions
                    (
                        () => result.ShouldBeOfType<int>(() => "Some additional context"),
                        () => Assert.AreEqual(4, result)
                    );
        }

        protected override void ShouldThrowAWobbly()
        {
            int result = 4;
            result.ShouldSatisfyAllConditions
                    (
                        () => result.ShouldBeOfType<float>("Some additional context"),
                        () => Assert.AreEqual(5, result)
                    );
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"result should satisfy all the conditions specified, but does not.
        The following errors were found ...
        --------------- Error 1 ---------------

    result
        should be of type
    System.Single
        but was
    System.Int32
    Additional Info:
    Some additional context
--------------- Error 2 ---------------
  Expected: 5
  But was:  4

-----------------------------------------";
            }
        }
    }
}