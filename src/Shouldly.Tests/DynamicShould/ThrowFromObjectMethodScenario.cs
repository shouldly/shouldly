namespace Shouldly.Tests.DynamicShould;

public class ThrowFromObjectMethodScenario
{
    private class Foo
    {
        public object NoExceptionMethod() => this;

        public object ExceptionMethod() =>
            throw new InvalidOperationException();
    }

    [Fact]
    public void NotThrowFromDynamicMethodScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            Shouldly.DynamicShould
                .Throw<InvalidOperationException>(() => ((dynamic)new Foo()).NoExceptionMethod(), "Some additional context"));
    }

    [Fact]
    public void ThrowOtherExceptionFromDynamicMethodScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            Shouldly.DynamicShould
                .Throw<ArgumentException>(() => ((dynamic)new Foo()).NoExceptionMethod(), "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        Shouldly.DynamicShould.Throw<InvalidOperationException>(() => ((dynamic)new Foo()).ExceptionMethod(), "Some additional context");
    }
}