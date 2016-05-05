#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
{
    public class TaskWithTimeoutScenario
   
    {

    [Fact]
        public void ShouldThrowAWobbly()
        {
            var task = Task.Factory.StartNew(() => { Thread.Sleep(5000); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var ex = Should.Throw<ShouldCompleteInException>(() => task.ShouldThrow<InvalidOperationException>(TimeSpan.FromSeconds(0.5), "Some additional context"));
            ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
        }

[Fact]
        public void ShouldThrowAWobbly_ExceptionTypePassedIn()
        {
            var task = Task.Factory.StartNew(() => { Thread.Sleep(5000); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var ex = Should.Throw(() => task.ShouldThrow<InvalidOperationException>(TimeSpan.FromSeconds(0.5), "Some additional context"), typeof(ShouldCompleteInException));
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
            var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var ex = task.ShouldThrow<InvalidOperationException>(TimeSpan.FromSeconds(2));
            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }

[Fact]
        public void ShouldPass_ExceptionTypePassedIn()
        {
            var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var ex = task.ShouldThrow(TimeSpan.FromSeconds(2), typeof(InvalidOperationException));
            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
#endif