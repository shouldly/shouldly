using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldNotThrow
{
    [TestFixture]
    public class ExtractsCodeScenarios
    {
        [Test]
        [ExpectedException(ExpectedMessage = "() => methodCall() should not throw but threw System.Exception with message \"Exception of type 'System.Exception' was thrown.\"")]
        public void ExtractsCodeCorrectly1()
        {
            Should.NotThrow(() => methodCall());
        }
        [Test]
        [ExpectedException(ExpectedMessage = "() => noThrowMethodCall() should throw System.Exception but did not")]
        public void ExtractsCodeCorrectly2()
        {
            Should.Throw<Exception>(() => noThrowMethodCall());
        }

        [Test]
        [ExpectedException(ExpectedMessage = "() => { methodCallWithParameters(1, 2); } should not throw but threw System.Exception with message \"Exception of type 'System.Exception' was thrown.\"")]
        public void ExtractsCodeCorrectly3()
        {
            Should.NotThrow(() => { methodCallWithParameters(1, 2); });
        }

        [Test]
        public void ExtractsCodeCorrectly4()
        {
            try
            {
                Should.NotThrow(() =>
                {
                    if (methodCall1())
                    {
                        methodCallWithParameters(1, 2);
                    }
                });
            }
            catch (ShouldAssertException ex)
            {
                ex.Message.Replace("\r\n", "\n")
                    .ShouldBe(
                        @"() =>
                {
                    if (methodCall1())
                    {
                        methodCallWithParameters(1, 2);
                    }
                } should not throw but threw System.Exception with message ""Exception of type 'System.Exception' was thrown."""
                            .Replace("\r\n", "\n"));
            }
        }

        [Test]
        public void ExtractsCodeCorrectly5()
        {
            try
            {
                Should.NotThrow(() =>
                {
                    if (methodCall1())
                    {
                        methodCall2();
                    }
                });
            }
            catch (ShouldAssertException ex)
            {
                ex.Message.Replace("\r\n", "\n")
                    .ShouldBe(
                        @"() =>
                {
                    if (methodCall1())
                    {
                        methodCall2();
                    }
                } should not throw but threw System.Exception with message ""Exception of type 'System.Exception' was thrown."""
                            .Replace("\r\n", "\n"));
            }
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