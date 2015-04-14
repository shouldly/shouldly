﻿using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldAllBe
{
    public class IntegerArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] {1, 2, 3}.ShouldAllBe(x => x < 2, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { 1, 2, 3 } should satisfy the condition (x < 2) but [2,3] do not" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {1, 2, 3}.ShouldAllBe(x => x < 4);
        }
    }
}