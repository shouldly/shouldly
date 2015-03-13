using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class IntegerWithNegativeValuesScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 2, 3, 4, 5, 4, 123665, 11234, -1356237712831 }.ShouldContain(6);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] { 2, 3, 4, 5, 4, 123665, 11234, -1356237712831 } " +
                         "should contain 6 " +
                         "but does not"; }
        }

        protected override void ShouldPass()
        {
            new[] { 2, 3, 4, 5, 4, 123665, 11234, -1356237712831 }.ShouldContain(-1356237712831);
        }
    }
}