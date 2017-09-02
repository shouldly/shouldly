using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldSatisfyAnyConditions
{
    public class MultipleConditionsScenario_MultiLine2
    {

        [Fact]
        public void MultipleConditionsScenario_MultiLine2ShouldFail()
        {
            int result = 4;
            Verify.ShouldFail(() =>
result.ShouldSatisfyAnyConditions
(
    () => result.ShouldBeOfType<float>("Some additional context"),
    () => result.ShouldBeGreaterThan(5, "Some additional context")
),

errorWithSource:
@"result
    should satisfy any of the conditions specified, but does not.
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

-----------------------------------------",

errorWithoutSource:
@"4
    should satisfy any of the conditions specified, but does not.
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

-----------------------------------------");
        }

        [Fact]
        public void ShouldPass()
        {
            int result = 4;

            result.ShouldSatisfyAnyConditions
                    (
                        ()
                            => result
                                .ShouldBeOfType<int>(),
                        ()
                            =>
                            result
                            .ShouldBeGreaterThan(3)
                    );

            result.ShouldSatisfyAnyConditions
                    (
                        ()
                            => result
                                .ShouldBeOfType<float>(),
                        ()
                            =>
                            result
                            .ShouldBeGreaterThan(3)
                    );

            result.ShouldSatisfyAnyConditions
                    (
                        ()
                            => result
                                .ShouldBeOfType<int>(),
                        ()
                            =>
                            result
                            .ShouldBeGreaterThan(5)
                    );
        }
    }
}