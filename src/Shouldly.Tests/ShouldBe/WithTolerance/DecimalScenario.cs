using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DecimalScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            const decimal pi = (decimal)Math.PI;
            pi.ShouldBe(3.24m, 0.01m);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "pi should be within 0.01 of 3.24 but was 3.14159265358979"; }
        }

        protected override void ShouldPass()
        {
            const decimal pi = (decimal)Math.PI;
            pi.ShouldBe(3.14m, 0.01m);
        }
    }
}