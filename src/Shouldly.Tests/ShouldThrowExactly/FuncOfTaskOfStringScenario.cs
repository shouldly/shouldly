namespace Shouldly.Tests.ShouldThrowExactly;

public class FuncOfTaskOfStringScenario
{
    [Fact]
    public void ShouldPassWhenExactTypeIsThrown()
    {
        var expected = new ArgumentException();
        var action = new Func<Task<string>>(() => Task.FromException<string>(expected));

        ArgumentException ex = action.ShouldThrowExactly<ArgumentException>();

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public void ShouldPassWhenExactTypeIsThrown_Static()
    {
        var expected = new ArgumentException();

        var ex = Should.ThrowExactly<ArgumentException>(() => Task.FromException<string>(expected));

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public void DerivedExceptionShouldFail()
    {
        var action = new Func<Task<string>>(() => Task.FromException<string>(new ArgumentNullException()));
        Verify.ShouldFail(() =>
            action.ShouldThrowExactly<ArgumentException>("Some additional context"));
    }
}
