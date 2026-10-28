namespace Shouldly.Tests.ShouldThrowExactly;

public class FuncOfObjectScenario
{
    [Fact]
    public void ShouldPassWhenExactTypeIsThrown()
    {
        var expected = new ArgumentException();
        var func = new Func<object?>(() => throw expected);

        ArgumentException ex = func.ShouldThrowExactly<ArgumentException>();

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public void ShouldPassWhenExactTypeIsThrown_Static()
    {
        var ex = Should.ThrowExactly<ArgumentNullException>(() => new Person(null!));

        ex.ParamName.ShouldBe("name");
    }

    [Fact]
    public void DerivedExceptionShouldFail()
    {
        var func = new Func<object?>(() => throw new ArgumentNullException());
        Verify.ShouldFail(() =>
            func.ShouldThrowExactly<ArgumentException>("Some additional context"));
    }

    [Fact]
    public void DerivedExceptionShouldFail_Static()
    {
        Verify.ShouldFail(() =>
            Should.ThrowExactly<ArgumentException>(() => new Person(null!)));
    }

    [Fact]
    public void NotThrowingShouldFail()
    {
        var func = new Func<object?>(() => 1);
        Verify.ShouldFail(() =>
            func.ShouldThrowExactly<NotImplementedException>("Some additional context"));
    }

    private class Person(string name)
    {
        public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
    }
}
