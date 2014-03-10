using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeGreaterThan
{
    public class StringVsNullScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            ((string) null).ShouldBeGreaterThan("b");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "((string) null) should be greater than \"b\" but was null"; }
        }

        protected override void ShouldPass()
        {
            "b".ShouldBeGreaterThan(null);
        }
    }
}