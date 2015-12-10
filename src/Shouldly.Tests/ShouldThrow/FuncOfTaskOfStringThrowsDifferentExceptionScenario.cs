#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskOfStringThrowsDifferentExceptionScenario
    {

        [Fact]
        public void FuncOfTaskOfStringThrowsDifferentExceptionScenarioShouldFail()
        {
            Action action = () =>
            {
                new Func<Task<string>>(() =>
                {
                    throw new RankException();
                }).Invoke();
            };

            Verify.ShouldFail(() =>
            action.ShouldThrow<InvalidOperationException>("Some additional context"),

errorWithSource:
@"`action()` should throw System.InvalidOperationException but threw System.RankException
Additional Info:
Some additional context",

errorWithoutSource:
@"`action()` should throw System.InvalidOperationException but threw System.RankException
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
    }
}
#endif