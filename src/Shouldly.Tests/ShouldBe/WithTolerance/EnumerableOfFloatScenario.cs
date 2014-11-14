using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class EnumerableOfFloatScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { (float)Math.PI, (float)Math.PI }.ShouldBe(new[] { 3.24f, 3.24f }, 0.01);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] { (float)Math.PI, (float)Math.PI } " +
                         "should be [3.24, 3.24] " +
                         "but was[3.141593, 3.141593] " +
                         "difference [*3.141593*, *3.141593*]"; }
        }

        protected override void ShouldPass()
        {
            new[] { (float)Math.PI, (float)Math.PI }.ShouldBe(new[] { 3.14f, 3.14f }, 0.01);
        }
    }
}