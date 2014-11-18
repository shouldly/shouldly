using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DateTimeOffsetScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            date.ShouldBe(new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "date should be within 01:00:00 of 1/06/2000 1:00:01 AM +00:00 but was 1/06/2000 12:00:00 AM +00:00"; }
        }

        protected override void ShouldPass()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            date.ShouldBe(new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1.5));
        }
    }
}