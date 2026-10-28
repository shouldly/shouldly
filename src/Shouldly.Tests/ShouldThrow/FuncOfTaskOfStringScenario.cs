namespace Shouldly.Tests.ShouldThrow;

public class FuncOfTaskOfStringScenario
{
    [Fact]
    public void FuncOfTaskOfStringScenarioShouldFail()
    {
        Func<Task<string>> action = () => Task.Run(() => "Foo");

        Verify.ShouldFail(() =>
            action.ShouldThrow<InvalidOperationException>("Some additional context"));
    }

    [Fact]
    public void FuncOfTaskOfStringScenarioShouldFail_ExceptionTypePassedIn()
    {
        Func<Task<string>> action = () => Task.Run(() => "Foo");

        Verify.ShouldFail(() =>
            action.ShouldThrow("Some additional context", typeof(InvalidOperationException)));
    }

    [Fact]
    public void ShouldPass()
    {
        Func<Task<string>> action = () => Task.FromException<string>(new InvalidOperationException());

        var ex = action.ShouldThrow<InvalidOperationException>();

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldPass_ExceptionTypePassedIn()
    {
        Func<Task<string>> action = () => Task.FromException<string>(new InvalidOperationException());

        var ex = action.ShouldThrow(typeof(InvalidOperationException));

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }
}
