using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroFloatWithFunctionContextScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            0f.ShouldBeNegative(() => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "0f was 0 and should be negative but wasn't" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }
    }
}