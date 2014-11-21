using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DateTimeScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var date = new DateTime(2000, 6, 1);
            date.ShouldBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            { 
                return String.Format("date should be within {0} of {1} but was {2}",
                    TimeSpan.FromHours(1), new DateTime(2000, 6, 1, 1, 0, 1), new DateTime(2000, 6, 1)); 
            }
        }

        protected override void ShouldPass()
        {
            var date = new DateTime(2000, 6, 1);
            date.ShouldBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1.5d));
        }
    }
}
