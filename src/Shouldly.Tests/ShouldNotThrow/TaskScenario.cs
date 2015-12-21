#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class TaskScenario
    {
        [Fact]
        public void TaskScenarioShouldFail()
        {
            var task = Task.Factory.StartNew(() => { throw new RankException(); },
                            CancellationToken.None, TaskCreationOptions.None,
                            TaskScheduler.Default);

            Verify.ShouldFail(() =>
task.ShouldNotThrow("Some additional context"),

errorWithSource:
@"Task `task`
    should not throw but threw
System.RankException
    with message
""Attempted to operate on an array with the incorrect number of dimensions.""

Additional Info:
    Some additional context",

errorWithoutSource:
@"Task
    should not throw but threw
System.RankException
    with message
""Attempted to operate on an array with the incorrect number of dimensions.""

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);

            task.ShouldNotThrow();
        }
    }
}
#endif