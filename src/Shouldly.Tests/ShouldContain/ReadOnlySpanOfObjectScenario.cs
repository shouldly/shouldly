namespace Shouldly.Tests.ShouldContain;

public class ReadOnlySpanOfObjectScenario
{
    [Fact]
    public void ReadOnlySpanOfObjectScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                {
                    var a = new TestObject();
                    var b = new TestObject();
                    var c = new TestObject();
                    var d = new TestObject();

                    ReadOnlySpan<TestObject> target = [a, b, c];
                    target.ShouldContain(d, "Some additional context");
                },

            errorWithSource:
            """
            target
                should contain
            Shouldly.Tests.ShouldContain.ReadOnlySpanOfObjectScenario+TestObject (000000)
                but was actually
            [Shouldly.Tests.ShouldContain.ReadOnlySpanOfObjectScenario+TestObject (000000), Shouldly.Tests.ShouldContain.ReadOnlySpanOfObjectScenario+TestObject (000000), Shouldly.Tests.ShouldContain.ReadOnlySpanOfObjectScenario+TestObject (000000)]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [Shouldly.Tests.ShouldContain.ReadOnlySpanOfObjectScenario+TestObject (000000), Shouldly.Tests.ShouldContain.ReadOnlySpanOfObjectScenario+TestObject (000000), Shouldly.Tests.ShouldContain.ReadOnlySpanOfObjectScenario+TestObject (000000)]
                should contain
            Shouldly.Tests.ShouldContain.ReadOnlySpanOfObjectScenario+TestObject (000000)
                but did not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var a = new TestObject();
        var b = new TestObject();
        var c = new TestObject();
        ReadOnlySpan<TestObject> target = [a, b, c];
        target.ShouldContain(b);
    }

    private sealed class TestObject : IEquatable<TestObject>
    {
        public bool Equals(TestObject? other) => object.ReferenceEquals(this, other);
    }
}