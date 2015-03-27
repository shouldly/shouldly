using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DateTimeOffsetScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            date.ShouldBe(new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1), () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return String.Format("date should be within {0} of {1} but was {2}" +
                                     " Additional Info:" +
                                     " Some additional context",
                    TimeSpan.FromHours(1), new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero),
                    new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero));
            }
        }

        protected override void ShouldPass()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            date.ShouldBe(new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1.5));
        }
    }
}