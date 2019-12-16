using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class TaskOfTScenario
    {

        [Fact]
        [UseCulture("en-US")]
        public void TaskOfTScenarioShouldFail()
        {
            var task = Task.Factory.StartNew<string>(() => { throw new RankException(); },
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
            var task = Task.Factory.StartNew(() => "foo",
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);

            var result = task.ShouldNotThrow();
            result.ShouldBe("foo");
        }
    }
}