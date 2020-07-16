using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeDecoratedWith
{
    public class DecoratedScenario
    {
        [Fact]
        public void DerivedTypeScenarioShouldPass()
        {
            var myDecoratedThing = typeof(MyDecoratedBase);
            myDecoratedThing.ShouldBeDecoratedWith<UseCultureAttribute>();
        }
    }
}