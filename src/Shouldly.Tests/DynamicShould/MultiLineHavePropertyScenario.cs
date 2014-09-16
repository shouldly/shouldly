using System.Dynamic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class MultiLineHavePropertyScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            dynamic testDynamicObject = new ExpandoObject();
            testDynamicObject.Foo = "FooPropertyValue";
            DynamicShould
                .HaveProperty(testDynamicObject, "Foo");
        }

        protected override void ShouldThrowAWobbly()
        {
            dynamic testDynamicObject = new ExpandoObject();
            testDynamicObject.Bar = "BarPropertyValue";
            DynamicShould
                .HaveProperty(testDynamicObject, "foo");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Dynamic Object \"testDynamicObject\" should contain property \"foo\" but does not."; }
        }
    }
}