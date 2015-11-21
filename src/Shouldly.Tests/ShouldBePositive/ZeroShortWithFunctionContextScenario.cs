using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroShortWithFunctionContextScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
           ((short)0).ShouldBePositive(() => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "(short)0 was 0 and should be positive but wasn't" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }
    }
}