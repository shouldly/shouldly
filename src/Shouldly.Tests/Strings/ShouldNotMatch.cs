﻿using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings
{
    class ShouldNotMatch : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldNotMatch(@"\w+", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "\"Cheese\" should match \"\\w+\" but was \"Cheese\" " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldNotMatch(@"Cat");
        }
    }
}