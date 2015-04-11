using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeLessThan
{
    public class StringVsNullScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "b".ShouldBeLessThan(null, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"b\" should be less than null but was \"b\"" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            ((string)null).ShouldBeLessThan("b");
        }
    }
}