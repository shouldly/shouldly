using System;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldNotThrow
{
    [TestFixture]
    public class ExtractsCodeScenarios
    {
        [Test]
        public void ExtractsCodeCorrectly1()
        {
            TestHelpers.Should.Error(() =>
                Should.NotThrow(() => methodCall()),
                "`methodCall()` should not throw but threw System.Exception with message \"Exception of type 'System.Exception' was thrown.\"");
        }

        [Test]
        public void ExtractsCodeCorrectly2()
        {
            TestHelpers.Should.Error(() =>
                Should.Throw<Exception>(() => noThrowMethodCall()),
                "`noThrowMethodCall()` should throw System.Exception but did not");
        }

        [Test]
        public void ExtractsCodeCorrectly3()
        {
            TestHelpers.Should.Error(() =>
                Should.NotThrow(() => { methodCallWithParameters(1, 2); }),
                "`methodCallWithParameters(1, 2);` should not throw but threw System.Exception with message \"Exception of type 'System.Exception' was thrown.\"");
        }

        [Test]
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

        [Test]
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

        private void noThrowMethodCall()
        {
        }

        private void methodCallWithParameters(int i, int i1)
        {
            throw new Exception();
        }

        private void methodCall2()
        {
            throw new Exception();
        }

        private bool methodCall1()
        {
            return true;
        }

        private void methodCall()
        {
            throw new Exception();
        }
    }
}