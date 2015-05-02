#if net40
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
            Should.NotThrow(() => Should.CompleteIn(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)), TimeSpan.FromSeconds(2)));
        }

        [Test]
        public void ShouldCompleteIn_WhenFinishAfterTimeout()
        {
            var ex = Should.Throw<TimeoutException>(() => 
                Should.CompleteIn(() => Thread.Sleep(TimeSpan.FromSeconds(2)), TimeSpan.FromSeconds(1), "Some additional context"));
            ex.Message.ShouldContainWithoutWhitespace(@"
    Delegate
        should complete in
    00:00:01
        but did not
    Additional Info:
    Some additional context");
        }

        [Test]
        public void ShouldCompleteInTask_WhenFinishAfterTimeout()
        {
            var ex = Should.Throw<TimeoutException>(() => 
                Should.CompleteIn(
                    () => Task.Factory.StartNew(() => Thread.Sleep(TimeSpan.FromSeconds(2))), 
                    TimeSpan.FromSeconds(1), "Some additional context"));
            ex.Message.ShouldContainWithoutWhitespace(@"
    Task
        should complete in
    00:00:01
        but did not
    Additional Info:
    Some additional context");
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
            var ex = Should.Throw<TimeoutException>(() => Should.CompleteIn(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                return "";
            }, TimeSpan.FromSeconds(1), "Some additional context"));

            ex.Message.ShouldContainWithoutWhitespace(@"
    Delegate
        should complete in
    00:00:01
        but did not
    Additional Info:
    Some additional context");
        }

        [Test]
        public void ShouldCompleteInTaskT_WhenFinishAfterTimeout()
        {
            var ex = Should.Throw<TimeoutException>(() => Should.CompleteIn(() =>
            {
                return Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    return "";
                });
            }, TimeSpan.FromSeconds(1), "Some additional context"));

            ex.Message.ShouldContainWithoutWhitespace(@"
    Task
        should complete in
    00:00:01
        but did not
    Additional Info:
    Some additional context");
        }

        [Test]
        public void ShouldCompleteInT_WhenThrowsNonTimeoutException()
        {
            Should.Throw<NotImplementedException>(() => Should.CompleteIn(new Func<string>(() => { throw new NotImplementedException(); }), TimeSpan.FromSeconds(1)));
        }
    }
}
#endif