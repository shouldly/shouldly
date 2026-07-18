namespace Shouldly.Tests.DynamicShould;

public class ThrowFromVoidMethodScenario
{
    private class Foo
    {
        public void NoExceptionMethod()
        {
        }

        public void ExceptionMethod()
        {
            throw new InvalidOperationException();
        }
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