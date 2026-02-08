namespace Shouldly.Tests.DynamicShould;

public class MultiLineHavePropertyScenario_MaximumLines
{
    [Fact]
    public void MultiLineHavePropertyScenario_MaximumLinesShouldFail()
    {
        dynamic testDynamicObject = new ExpandoObject();
        testDynamicObject.Bar = "BarPropertyValue";
        Verify.ShouldFail(() =>
            Shouldly.DynamicShould
                .HaveProperty(
                    testDynamicObject,
                    "foo",
                    "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        dynamic testDynamicObject = new ExpandoObject();
        testDynamicObject.Foo = "FooPropertyValue";
        Shouldly.DynamicShould
            .HaveProperty(
                testDynamicObject,
                "Foo");
    }
}