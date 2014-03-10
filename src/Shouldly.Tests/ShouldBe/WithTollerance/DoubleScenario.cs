using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTollerance
{
    public class DoubleScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            const float pi = (float)Math.PI;
            pi.ShouldBe(3.24f, 0.01f);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "pi should be 3.24 but was 3.141593"; }
        }

        protected override void ShouldPass()
        {
            const float pi = (float)Math.PI;
            pi.ShouldBe(3.14f, 0.01f);
        }
    }
}