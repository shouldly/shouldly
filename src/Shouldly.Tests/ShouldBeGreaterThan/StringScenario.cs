using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeGreaterThan
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "a".ShouldBeGreaterThan("b");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"a\" should be greater than \"b\" but was \"a\""; }
        }

        protected override void ShouldPass()
        {
            "b".ShouldBeGreaterThan("a");
        }
    }
}