using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeGreaterThan
{
    public class StringVsNullScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            ((string) null).ShouldBeGreaterThan("b", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "(string) null should be greater than \"b\" but was null" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "b".ShouldBeGreaterThan(null);
        }
    }
}