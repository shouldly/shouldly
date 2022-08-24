namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskWhichThrowsDirectlyScenario
    {
        [Fact]
        public void ShouldPass()
        {
            // ReSharper disable once RedundantDelegateCreation
            var task = new Func<Task>(() => throw new InvalidOperationException());
            task.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void ShouldPass_ExceptionTypePassedIn()
        {
            // ReSharper disable once RedundantDelegateCreation
            var task = new Func<Task>(() => throw new InvalidOperationException());
            task.ShouldThrow(typeof(InvalidOperationException));
        }

        [Fact]
        public void ShouldPassTimeoutException()
        {
            var task = new Func<Task>(() => throw new TimeoutException());
            task.ShouldThrow<TimeoutException>();
        }

        [Fact]
        public void ShouldPassTimeoutException_ExceptionTypePassedIn()
        {
            var task = new Func<Task>(() => throw new TimeoutException());
            task.ShouldThrow(typeof(TimeoutException));
        }

        [Fact]
        public async Task ShouldPassTimeoutExceptionAsync()
        {
            var task = new Func<Task>(async () => { await Task.FromException(new TimeoutException()); });
            await Task.FromResult(task.ShouldThrow<TimeoutException>());
        }

        [Fact]
        public async Task ShouldPassTimeoutExceptionAsync_ExceptionTypePassedIn()
        {
            var task = new Func<Task>(async () => { await Task.FromException(new TimeoutException()); });
            await Task.FromResult(task.ShouldThrow(typeof(TimeoutException)));
        }
    }
}