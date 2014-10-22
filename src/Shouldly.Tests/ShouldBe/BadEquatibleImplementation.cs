using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class BadEquatibleClassScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new BadEquatable().ShouldBe(new BadEquatable());
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new BadEquatable() should be Shouldly.Tests.ShouldBe.BadEquatibleClassScenario+BadEquatable but was Shouldly.Tests.ShouldBe.BadEquatibleClassScenario+BadEquatable"; }
        }

        protected override void ShouldPass()
        {
            var instance = new BadEquatable();
            instance.ShouldBe(instance);
        }

        public class BadEquatable : IEquatable<BadEquatable>
        {
            public bool Equals(BadEquatable other)
            {
                return false;
            }
        }
    }
}