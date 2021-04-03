using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class FuncOfTaskOfTWithTimeoutScenario
    {
        [Fact]
        public void ShouldThrowAWobbly()
        {
            var task = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                return "foo";
            });

            var ex = Should.Throw<ShouldCompleteInException>(() =>
                task.ShouldNotThrow(TimeSpan.FromSeconds(0.5), "Some additional context"));

            ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
        }

        private string ChuckedAWobblyErrorMessage => @"Task
        should complete in
    00:00:00.5000000
        but did not
    Additional Info:
    Some additional context";

        [Fact]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => "foo",
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var result = task.ShouldNotThrow(TimeSpan.FromSeconds(2.0));
            result.ShouldBe("foo");
        }
    }
}