#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskOfStringThrowsDifferentExceptionScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            // ReSharper disable once RedundantDelegateCreation
            Action action = () =>
            {
                new Func<Task<string>>(() => {
                    throw new RankException();
                }).Invoke();
            };

            action.ShouldThrow<InvalidOperationException>("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"`action()` should throw System.InvalidOperationException but threw System.RankException
Additional Info:
Some additional context";
            }
        }

        protected override void ShouldPass()
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