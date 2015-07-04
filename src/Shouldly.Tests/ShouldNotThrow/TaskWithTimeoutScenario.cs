#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldNotThrow
{
    [TestFixture]
    public class TaskWithTimeoutScenario
    {
        [Test]
        public void ShouldThrowAWobbly()
        {
            var task = Task.Factory.StartNew(() => { Thread.Sleep(5000); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var ex = Should.Throw<ShouldCompleteInException>(() => task.ShouldNotThrow(TimeSpan.FromSeconds(0.5), "Some additional context"));
            ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
        }

        protected string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"
    Task
        should complete in
    00:00:00.5000000
        but did not
    Additional Info:
    Some additional context";
            }
        }

        [Test]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            task.ShouldNotThrow(TimeSpan.FromSeconds(2));
        }
    }
}
#endif