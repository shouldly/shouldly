#if net40
using System;
using System.Threading.Tasks;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
{
    public class TaskThrowsDifferentExceptionScenario
    {
        [Fact]
        public void TaskThrowsDifferentExceptionScenarioShouldFail()
        {
            var task = Task.Factory.StartNew(() => { throw new RankException(); });
            Verify.ShouldFail(() =>
task.ShouldThrow<InvalidOperationException>("Some additional context"),

errorWithSource:
@"Task `task`
    should throw
System.InvalidOperationException
    but threw
System.RankException

Additional Info:
    Some additional context",

errorWithoutSource:
@"Task
    should throw
System.InvalidOperationException
    but threw
System.RankException

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); });

            var ex = task.ShouldThrow<InvalidOperationException>();
            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
#endif