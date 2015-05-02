#if net40
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class PredicateClosureScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var capturedOuterVar = 4;
            new[] {1, 2, 3}.ShouldContain(i => i > capturedOuterVar, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"
        new[] { 1, 2, 3 }
                should contain an element satisfying the condition
            (i > capturedOuterVar)
                but does not
            Additional Info:
            Some additional context";
            }
        }
    }
}
#endif