using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            get { return "date should be within 01:00:00 of 1/06/2000 1:00:01 AM but was 1/06/2000 12:00:00 AM"; }
        }

        protected override void ShouldPass()
        {
            var date = new DateTime(2000, 6, 1);
            date.ShouldBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1.5d));
        }
    }
}
