﻿using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class LongStringScenario : ShouldlyShouldTestScenario
    {
        protected string target = new string('a', 110) + "zzzz";

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldNotContain("zzzz");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "target should not contain \"zzzz\" (case insensitive comparison) " +
                       "but was actually \"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                       "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa...\"";
            }
        }

        protected override void ShouldPass()
        {
            target.ShouldNotContain("fff");
        }
    }
}