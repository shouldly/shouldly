using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "a".ShouldBeGreaterThanOrEqualTo("b", () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"a\" should be greater than or equal to \"b\" but was \"a\"" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "a".ShouldBeGreaterThanOrEqualTo("a");
        }
    }
}