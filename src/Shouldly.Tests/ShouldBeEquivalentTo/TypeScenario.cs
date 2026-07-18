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

    [Fact]
    public void ShouldPassWhenTypesAreEqual()
    {
        typeof(int).ShouldBeEquivalentTo(typeof(int));
    }

    [Fact]
    public void ShouldPassWhenTypeIsPropertyOfContainingObject()
    {
        var subject = new TypeHolder { Type = typeof(int) };
        var expected = new TypeHolder { Type = typeof(int) };

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
        var subject = new TypeHolder { Type = typeof(int) };
        var expected = new TypeHolder { Type = typeof(string) };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    public class TypeHolder
    {
        public Type Type { get; set; } = null!;
    }
}
