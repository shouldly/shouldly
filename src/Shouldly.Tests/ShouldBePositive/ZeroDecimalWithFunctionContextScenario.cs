using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroDecimalWithFunctionContextScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
           0m.ShouldBePositive(() => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "0m was 0m and should be positive but wasn't" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }
    }
}