#if net40
using System.Dynamic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.DynamicShouldTests
{
    public class HavePropertyScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            dynamic testDynamicObject = new ExpandoObject();
            testDynamicObject.Foo = "FooPropertyValue";
            DynamicShould.HaveProperty(testDynamicObject, "Foo");
        }

        protected override void ShouldThrowAWobbly()
        {
            dynamic testDynamicObject = new ExpandoObject();
            testDynamicObject.Bar = "BarPropertyValue";
            DynamicShould
                .HaveProperty(testDynamicObject, "foo", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Dynamic object \"testDynamicObject\" should contain property \"foo\" but does not." + @"
Additional Info:
Some additional context"; }
        }
    }
}
#endif