using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "a".ShouldBeGreaterThanOrEqualTo("b");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"a\" should be greater than or equal to \"b\" but was \"a\""; }
        }

        protected override void ShouldPass()
        {
            "a".ShouldBeGreaterThanOrEqualTo("a");
        }
    }
}