using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeDecoratedWith
{
    public class DerivedTypeScenario
    {
        [Fact]
        public void DerivedTypeScenarioShouldPass()
        {
            var myDecoratedThing = typeof(MyDecoratedThing);

            myDecoratedThing.ShouldBeDecoratedWith<UseCultureAttribute>();
        }
    }
}