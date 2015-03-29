using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DoubleScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            const double pi = Math.PI;
            pi.ShouldBe(3.24d, 0.01d, () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "pi should be within 0.01 of 3.24 but was 3.14159265358979 " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            const double pi = Math.PI;
            pi.ShouldBe(3.14d, 0.01d);
        }
    }
}