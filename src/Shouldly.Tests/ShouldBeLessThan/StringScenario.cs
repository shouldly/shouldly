using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeLessThan
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "b".ShouldBeLessThan("a", () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"b\" should be less than \"a\" but was \"b\"" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "a".ShouldBeLessThan("b");
        }
    }
}