using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldCompleteInTests
    {
        [Test]
        public void ShouldCompleteIn_WhenFinishBeforeTimeout()
        {
            Shouldly.Should.NotThrow(() => Shouldly.Should.CompleteIn(() => { Thread.Sleep(TimeSpan.FromSeconds(1)); }, TimeSpan.FromSeconds(2)));
        }

        [Test]
        public void ShouldCompleteIn_WhenFinishAfterTimeout()
        {
            Shouldly.Should.Throw<TimeoutException>(() => Shouldly.Should.CompleteIn(() => { Thread.Sleep(TimeSpan.FromSeconds(2)); }, TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void ShouldCompleteIn_WhenThrowsNonTimeoutException()
        {
            Shouldly.Should.Throw<AggregateException>(() => Shouldly.Should.CompleteIn(() => { throw new NotImplementedException(); }, TimeSpan.FromSeconds(1)));
        }
    }
}