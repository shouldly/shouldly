using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskOfStringScenario
    {
        [Fact]
        public void FuncOfTaskOfStringScenarioShouldFail()
        {
            var task = Task.Factory.StartNew(() => "Foo",
                            CancellationToken.None, TaskCreationOptions.None,
                            TaskScheduler.Default);

            Verify.ShouldFail(() =>
task.ShouldThrow<InvalidOperationException>("Some additional context"),

errorWithSource:
@"Task `task`
    should throw
System.InvalidOperationException
    but did not

Additional Info:
    Some additional context",

errorWithoutSource:
@"Task
    should throw
System.InvalidOperationException
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void FuncOfTaskOfStringScenarioShouldFail_ExceptionTypePassedIn()
        {
            var task = Task.Factory.StartNew(() => "Foo",
                            CancellationToken.None, TaskCreationOptions.None,
                            TaskScheduler.Default);

            Verify.ShouldFail(() =>
task.ShouldThrow("Some additional context", typeof(InvalidOperationException)),

errorWithSource:
@"Task `task`
    should throw
System.InvalidOperationException
    but did not

Additional Info:
    Some additional context",

errorWithoutSource:
@"Task
    should throw
System.InvalidOperationException
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew<string>(() => { throw new InvalidOperationException(); },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);

            var ex = task.ShouldThrow<InvalidOperationException>();

            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }

        [Fact]
        public void ShouldPass_ExceptionTypePassedIn()
        {
            var task = Task.Factory.StartNew<string>(() => { throw new InvalidOperationException(); },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);

            var ex = task.ShouldThrow(typeof(InvalidOperationException));

            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
