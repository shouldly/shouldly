﻿using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeLessThanOrEqualTo
{
    public class IntScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            7.ShouldBeLessThanOrEqualTo(1, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "7 should be less than or equal to 1 but was 7" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            1.ShouldBeLessThanOrEqualTo(1);
        }
    }
}