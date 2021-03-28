using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.DynamicShould
{
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
                        .Throw<InvalidOperationException>(() => ((dynamic)new Foo()).NoExceptionMethod(), "Some additional context"),

errorWithSource:
@"`(dynamic)new Foo()).NoExceptionMethod(`
    should throw
System.InvalidOperationException
    but did not

Additional Info:
    Some additional context",

errorWithoutSource:
@"delegate
    should throw
System.InvalidOperationException
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ThrowOtherExceptionFromDynamicMethodScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
                    Shouldly.DynamicShould
                        .Throw<ArgumentException>(() => ((dynamic)new Foo()).NoExceptionMethod(), "Some additional context"),

errorWithSource:
@"`(dynamic)new Foo()).NoExceptionMethod(`
    should throw
System.ArgumentException
    but did not

Additional Info:
    Some additional context",

errorWithoutSource:
@"delegate
    should throw
System.ArgumentException
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            Shouldly.DynamicShould.Throw<InvalidOperationException>(() => ((dynamic)new Foo()).ExceptionMethod(), "Some additional context");
        }
    }
}