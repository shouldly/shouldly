using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBe.WithTolerance
{
    public class DateTimeScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var date = new DateTime(2000, 6, 1);
            date.ShouldNotBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1.5), "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return String.Format("date should not be within {0} of {1} but was {2}" +
                                     "Additional Info:" +
                                     "Some additional context", 
                    TimeSpan.FromHours(1.5), new DateTime(2000, 6, 1, 1, 0, 1), new DateTime(2000, 6, 1)); 
            }
        }

        protected override void ShouldPass()
        {
            var date = new DateTime(2000, 6, 1);
            date.ShouldNotBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1));
        }
    }
}
