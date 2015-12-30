#if !PORTABLE
using System;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class ExtractsCodeScenarios
    {
        [Fact]
        public void ExtractsCodeCorrectly1()
        {
            TestHelpers.Should.Error(() =>
                Should.NotThrow(() => methodCall()),
                @"`methodCall()` should not throw but threw System.Exception with message ""Exception of type 'System.Exception' was thrown.""");
        }

        [Fact]
        public void ExtractsCodeCorrectly2()
        {
            TestHelpers.Should.Error(() =>
                Should.Throw<Exception>(() => noThrowMethodCall()),
                "`noThrowMethodCall()` should throw System.Exception but did not");
        }

        [Fact]
        public void ExtractsCodeCorrectly3()
        {
            TestHelpers.Should.Error(() =>
                Should.NotThrow(() => { methodCallWithParameters(1, 2); }),
                @"`methodCallWithParameters(1, 2);` should not throw but threw System.Exception with message ""Exception of type 'System.Exception' was thrown.""");
        }

        [Fact]
        public void ExtractsCodeCorrectly4()
        {
            TestHelpers.Should.Error(
                () => Should.NotThrow(() =>
                {
                    if (methodCall1())
                    {
                        methodCallWithParameters(1, 2);
                    }
                }),
                @"`if (methodCall1()) { methodCallWithParameters(1, 2); }` should not throw but threw System.Exception with message ""Exception of type 'System.Exception' was thrown.""");
        }

        [Fact]
        public void ExtractsCodeCorrectly5()
        {
            TestHelpers.Should.Error(() =>
                Should.NotThrow(() =>
                {
                    if (methodCall1())
                    {
                        methodCall2();
                    }
                }),
                @"`if (methodCall1()) { methodCall2(); }` should not throw but threw System.Exception with message ""Exception of type 'System.Exception' was thrown.""");
        }

        void noThrowMethodCall()
        {
        }

        void methodCallWithParameters(int i, int i1)
        {
            throw new Exception();
        }

        void methodCall2()
        {
            throw new Exception();
        }

        bool methodCall1()
        {
            return true;
        }

        void methodCall()
        {
            throw new Exception();
        }
    }
}
#endif
