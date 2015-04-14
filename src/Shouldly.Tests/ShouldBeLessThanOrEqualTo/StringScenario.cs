using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeLessThanOrEqualTo
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "b".ShouldBeLessThanOrEqualTo("a", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"b\" should be less than or equal to \"a\" but was \"b\"" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "a".ShouldBeLessThanOrEqualTo("a");
        }
    }
}