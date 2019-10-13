using Shouldly.Tests.Strings;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskWhichThrowsDirectlyScenario
    {

        [Fact]
        public void ShouldPass()
        {
            // ReSharper disable once RedundantDelegateCreation
            var task = new Func<Task>(() => { throw new InvalidOperationException(); });
            task.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void ShouldPass_ExceptionTypePassedIn()
        {
            // ReSharper disable once RedundantDelegateCreation
            var task = new Func<Task>(() => { throw new InvalidOperationException(); });
            task.ShouldThrow(typeof(InvalidOperationException));
        }

        [Fact]
        public void ShouldPassTimeoutException()
        {
            var task = new Func<Task>(() => { throw new TimeoutException(); });
            task.ShouldThrow<TimeoutException>();
        }

        [Fact]
        public void ShouldPassTimeoutException_ExceptionTypePassedIn()
        {
            var task = new Func<Task>(() => { throw new TimeoutException(); });
            task.ShouldThrow(typeof(TimeoutException));
        }

        [Fact]
        public void ShouldPassTimeoutExceptionAsync()
        {
            var task = new Func<Task>(async () => { throw new TimeoutException(); });
            task.ShouldThrow<TimeoutException>();
        }

        [Fact]
        public void ShouldPassTimeoutExceptionAsync_ExceptionTypePassedIn()
        {
            var task = new Func<Task>(async () => { throw new TimeoutException(); });
            task.ShouldThrow(typeof(TimeoutException));
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public void ShouldHandleAggregateExceptionWithNoInnerExceptions(bool withSynchronizationContext, bool useGenericOverload)
        {
            var func = new Func<Task>(() => throw new AggregateException());

            if (!withSynchronizationContext)
                SynchronizationContext.SetSynchronizationContext(null);

            Verify.ShouldFail(
                () =>
                {
                    if (useGenericOverload)
                        func.ShouldThrow<InvalidOperationException>();
                    else
                        func.ShouldThrow(typeof(InvalidOperationException));
                },
                errorWithSource:
@"Task `func`
    should throw
System.InvalidOperationException
    but threw
System.AggregateException",
                errorWithoutSource:
@"Task
    should throw
System.InvalidOperationException
    but threw
System.AggregateException");
        }
    }
}