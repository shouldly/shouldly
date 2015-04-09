using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class StringArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] {"1", "2", "3"}.ShouldBeSubsetOf(new[] {"1", "2"}, () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { \"1\", \"2\", \"3\" } should be subset of [\"1\", \"2\"] but [\"3\"] is outside subset" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {"1", "2", "3"}.ShouldBeSubsetOf(new[] {"1", "2", "3", "4"}, () => "Some additional context");
        }
    }
}