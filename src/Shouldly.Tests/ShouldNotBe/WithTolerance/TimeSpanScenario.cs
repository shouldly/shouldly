using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBe.WithTolerance
{
    public class TimeSpanScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var timeSpan = TimeSpan.FromHours(1);
            timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "timeSpan should not be within 01:30:00 of 02:06:00 but was 01:00:00"; }
        }

        protected override void ShouldPass()
        {
            var timeSpan = TimeSpan.FromHours(1);
            timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1));
        }
    }
}