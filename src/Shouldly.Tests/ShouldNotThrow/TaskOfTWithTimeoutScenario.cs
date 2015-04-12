#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class TaskOfTWithTimeoutScenario
    {
        [Test]
        public void ShouldThrowAWobbly()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                return "foo";
            },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);
            var ex = Should.Throw<ShouldCompleteInException>(() => 
                Should.NotThrow(task, TimeSpan.FromSeconds(0.5), "Some additional context"));
            ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
        }

        protected string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"Task
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
            var task = Task.Factory.StartNew(() => "foo",
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);
            var result = Should.NotThrow(task, TimeSpan.FromSeconds(0.5));
            result.ShouldBe("foo");
        }
    }
}
#endif