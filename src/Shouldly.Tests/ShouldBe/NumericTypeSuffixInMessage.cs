using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class NumericTypeSuffixInMessage : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            2.0f.ShouldBe(2UL);
        }

        protected override void ShouldThrowAWobbly()
        {
            const ulong uLong = 2UL;
            uLong.ShouldBe(3UL);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"uLong should be 3uL but was 2uL";
            }
        }
    }
}
