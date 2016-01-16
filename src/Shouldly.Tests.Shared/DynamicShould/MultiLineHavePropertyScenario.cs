#if net40
using System.Dynamic;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.DynamicShouldTests
{
    public class MultiLineHavePropertyScenario
    {
        [Fact]
        public void MultiLineHavePropertyScenarioShouldFail()
        {
            dynamic testDynamicObject = new ExpandoObject();
            testDynamicObject.Bar = "BarPropertyValue";
            Verify.ShouldFail(() =>
            DynamicShould
                .HaveProperty(testDynamicObject, "foo", "Some additional context"),

errorWithSource:
@"Dynamic object ""testDynamicObject"" should contain property ""foo"" but does not." + @"

Additional Info:
    Some additional context",

errorWithoutSource:
@"Dynamic object should contain property ""foo"" but does not." + @"

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            dynamic testDynamicObject = new ExpandoObject();
            testDynamicObject.Foo = "FooPropertyValue";
            DynamicShould
                .HaveProperty(testDynamicObject, "Foo");
        }
    }
}
#endif