using System.Dynamic;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.DynamicShould
{
    public class MultiLineHavePropertyScenario_MaximumLines
    {

        [Fact]
        public void MultiLineHavePropertyScenario_MaximumLinesShouldFail()
        {
            dynamic testDynamicObject = new ExpandoObject();
            testDynamicObject.Bar = "BarPropertyValue";
            Verify.ShouldFail(() =>
            Shouldly.DynamicShould
                .HaveProperty
                (
                testDynamicObject,
                "foo",
                "Some additional context"
                ),

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
            Shouldly.DynamicShould
                .HaveProperty
                (
                testDynamicObject,
                "Foo"
                );
        }
    }
}