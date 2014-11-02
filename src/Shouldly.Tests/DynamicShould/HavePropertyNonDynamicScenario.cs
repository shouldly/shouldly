using System.Dynamic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.DynamicShouldTests
{
    public class HavePropertyNonDynamicScenario : ShouldlyShouldTestScenario
    {
        class Foo
        {
            public string Bar { get; set; }
        }

        protected override void ShouldPass()
        {
            dynamic testDynamicObject = new Foo();
            DynamicShould.HaveProperty(testDynamicObject, "Bar");
        }

        protected override void ShouldThrowAWobbly()
        {
            dynamic testDynamicObject = new Foo();
            testDynamicObject.Bar = "BarPropertyValue";
            DynamicShould
                .HaveProperty(testDynamicObject, "foo");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Dynamic object \"testDynamicObject\" should contain property \"foo\" but does not."; }
        }
    }
}