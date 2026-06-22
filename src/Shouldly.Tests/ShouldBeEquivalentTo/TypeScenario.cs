namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class TypeScenario
{
    [Fact]
    public void ShouldFail()
    {
        const string subject = "Hello";
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(5, "Some additional context"));
    }

    // Regression tests for https://github.com/shouldly/shouldly/issues/1050
    // Comparing System.Type values used to walk every Type property and trip on
    // Type.DeclaringMethod ("Method may only be called on a Type for which
    // Type.IsGenericParameter is true"), surfaced as a TargetInvocationException.
    [Fact]
    public void ShouldPassWhenTypesAreEqual()
    {
        typeof(int).ShouldBeEquivalentTo(typeof(int));
    }

    [Fact]
    public void ShouldPassWhenTypeIsPropertyOfContainingObject()
    {
        var subject = new Holder { Type = typeof(int) };
        var expected = new Holder { Type = typeof(int) };

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldPassForListOfTypes()
    {
        new List<Type> { typeof(int), typeof(string) }
            .ShouldBeEquivalentTo(new List<Type> { typeof(int), typeof(string) });
    }

    [Fact]
    public void ShouldFailWhenTypesDiffer()
    {
        Verify.ShouldFail(() =>
            typeof(int).ShouldBeEquivalentTo(typeof(string), "Some additional context"));
    }

    private sealed class Holder
    {
        public Type Type { get; set; } = null!;
    }
}