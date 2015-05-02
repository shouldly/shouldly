using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings
{
    public class ShouldMatch : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldMatch(@"\d+", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"Cheese\" should match \"\\d+\" but was \"Cheese\" " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldMatch(@"C.e{2}s[e]");
        }
    }
}