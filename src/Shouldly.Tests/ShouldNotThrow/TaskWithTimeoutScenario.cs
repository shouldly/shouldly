using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class TaskWithTimeoutScenario

    {

        [Fact]
        public void ShouldThrowAWobbly()
        {
            var task = Task.Factory.StartNew(() => { Task.Delay(5000).Wait(); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var ex = Should.Throw<ShouldCompleteInException>(() => task.ShouldNotThrow(TimeSpan.FromSeconds(0.5), "Some additional context"));
            ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
        }

        string ChuckedAWobblyErrorMessage => @"
    Task
        should complete in
    00:00:00.5000000
        but did not
    Additional Info:
    Some additional context";

        [Fact]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            task.ShouldNotThrow(TimeSpan.FromSeconds(2));
        }
    }
}