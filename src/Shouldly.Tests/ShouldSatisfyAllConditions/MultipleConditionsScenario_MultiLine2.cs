using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldSatisfyAllConditions
{
    public class MultipleConditionsScenario_MultiLine2 : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            int result = 4;
            result.ShouldSatisfyAllConditions
                    (
                        () 
                            => result
                                .ShouldBeOfType<int>(),
                        () 
                            => 
                            result
                            .ShouldBeGreaterThan(3)
                    );
        }

        protected override void ShouldThrowAWobbly()
        {
            int result = 4;
            result.ShouldSatisfyAllConditions
                    (
                        () 
                            => result
                                .ShouldBeOfType<float>(),
                        () 
                            => 
                            result
                            .ShouldBeGreaterThan(5)
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
--------------- Error 2 ---------------

    result
        should be greater than
    5
        but was
    4
-----------------------------------------";
            }
        }
    }
}