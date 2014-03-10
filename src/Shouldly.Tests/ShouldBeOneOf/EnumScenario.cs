using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeOneOf
{
    public class EnumScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            SomeFlags.Val1.ShouldBeOneOf(SomeFlags.Val2);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "SomeFlags.Val1 should be one of [SomeFlags.Val2] but was SomeFlags.Val1"; }
        }

        protected override void ShouldPass()
        {
            SomeFlags.Val1.ShouldBeOneOf(SomeFlags.Val1, SomeFlags.Val2);
        }
    }
}