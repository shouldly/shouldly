#if net40
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class PredicateClosureScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var capturedOuterVar = 4;
            new[] { 1, 2, 3 }.ShouldNotContain(i => i < capturedOuterVar);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"
        new[] { 1, 2, 3 }
                should not contain an element satisfying the condition
            (i < capturedOuterVar)
                but does";
            }
        }
    }
}
#endif