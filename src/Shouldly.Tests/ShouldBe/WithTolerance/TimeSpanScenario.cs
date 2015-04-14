using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class TimeSpanScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var timeSpan = TimeSpan.FromHours(1);
            timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1), "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "timeSpan should be within 01:00:00 of 02:06:00 but was 01:00:00" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            var timeSpan = TimeSpan.FromHours(1);
            timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d));
        }
    }
}