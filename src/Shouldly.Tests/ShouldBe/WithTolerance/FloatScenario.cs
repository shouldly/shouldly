using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class FloatScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            const float pi = (float)Math.PI;
            pi.ShouldBe(3.24f, 0.01d, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "pi should be within 0.01 of 3.24 but was 3.141593 " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            const float pi = (float)Math.PI;
            pi.ShouldBe(3.14f, 0.01d);
        }
    }
}