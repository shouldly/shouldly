using Shouldly.Tests.TestHelpers;
#if net40
using System;
using System.Threading;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldCompleteInTests
    {
        [Test]
        public void ShouldCompleteIn_WhenFinishBeforeTimeout()
        {
            Should.NotThrow(() => Should.CompleteIn(() => Thread.Sleep(TimeSpan.FromSeconds(1)), TimeSpan.FromSeconds(2)));
        }

        [Test]
        public void ShouldCompleteIn_WhenFinishAfterTimeout()
        {
            Should.Throw<TimeoutException>(() => Should.CompleteIn(() => Thread.Sleep(TimeSpan.FromSeconds(2)), TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void ShouldCompleteIn_WhenThrowsNonTimeoutException()
        {
            Should.Throw<NotImplementedException>(() => Should.CompleteIn(() => { throw new NotImplementedException(); }, TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void ShouldCompleteInT_WhenFinishBeforeTimeout()
        {
            Should.NotThrow(() => Should.CompleteIn(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                return "";
            }, TimeSpan.FromSeconds(2)));
        }

        [Test]
        public void ShouldCompleteInT_WhenFinishAfterTimeout()
        {
            Should.Throw<TimeoutException>(() => Should.CompleteIn(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                return "";
            }, TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void ShouldCompleteInT_WhenThrowsNonTimeoutException()
        {
            Should.Throw<NotImplementedException>(() => Should.CompleteIn<string>(() => { throw new NotImplementedException(); }, TimeSpan.FromSeconds(1)));
        }
    }
}
#endif