using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldSatisfyAllConditions
{
    public class GenericMultipleConditionsScenario
    {

        [Fact]
        public void GenericMultipleConditionsScenarioShouldFail()
        {
            int result = 4;
            Verify.ShouldFail(() =>
result.ShouldSatisfyAllConditions
    (
        "Some additional context",
        r => r.ShouldBeOfType<float>("Some additional context"),
        r => r.ShouldBeGreaterThan(5, "Some additional context")
    ),

errorWithSource:
@"result
    should satisfy all the conditions specified, but does not.
The following errors were found ...
--------------- Error 1 ---------------
    r => r
        should be of type
    System.Single
        but was
    System.Int32

    Additional Info:
        Some additional context

--------------- Error 2 ---------------
    r => r
        should be greater than
    5
        but was
    4

    Additional Info:
        Some additional context

-----------------------------------------

Additional Info:
    Some additional context",

errorWithoutSource:
@"4
    should satisfy all the conditions specified, but does not.
The following errors were found ...
--------------- Error 1 ---------------
    4
        should be of type
    System.Single
        but was
    System.Int32

    Additional Info:
        Some additional context

--------------- Error 2 ---------------
    4
        should be greater than
    5
        but was not

    Additional Info:
        Some additional context

-----------------------------------------

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            int result = 4;
            result.ShouldSatisfyAllConditions
                    (
                        r => result.ShouldBeOfType<int>(),
                        r => result.ShouldBeGreaterThan(3)
                    );
        }
    }
}