using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSameAs
{
    public class BoxedIntScenario : ShouldlyShouldTestScenario
    {
        private readonly object _boxedInt = 1;
        private readonly object _differentBoxedInt = 1;

        protected override void ShouldThrowAWobbly()
        {
            _boxedInt.ShouldBeSameAs(_differentBoxedInt);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_boxedInt should be same as 1 but was 1"; }
        }

        protected override void ShouldPass()
        {
            _boxedInt.ShouldBeSameAs(_boxedInt);
        }
    }
}