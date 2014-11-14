using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBe.WithTolerance
{
    public class DateTimeOffsetScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            date.ShouldNotBe(new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1.5));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "date should not be 1/06/2000 1:00:01 AM +00:00 but was 1/06/2000 12:00:00 AM +00:00"; }
        }

        protected override void ShouldPass()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            date.ShouldNotBe(new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1));
        }
    }
}