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
                                .ShouldBeOfType<int>(() => "Some additional context"),
                        () 
                            => 
                            result
                            .ShouldBeGreaterThan(3, () => "Some additional context")
                    );
        }

        protected override void ShouldThrowAWobbly()
        {
            int result = 4;
            result.ShouldSatisfyAllConditions
                    (
                        () 
                            => result
                                .ShouldBeOfType<float>(() => "Some additional context"),
                        () 
                            => 
                            result
                            .ShouldBeGreaterThan(5, () => "Some additional context")
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

    result
        should be greater than
    5
        but was
    4
    Additional Info:
    Some additional context
-----------------------------------------";
            }
        }
    }
}