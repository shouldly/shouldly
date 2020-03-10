using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrowAsync
{
    public class FuncOfTaskScenarioAsync
    {
        [Fact]
        public void ShouldThrowAWobbly()
        {
            try
            {
                Task task = Task.Factory.StartNew(() => { throw new InvalidOperationException("exception message"); },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);

                task.ShouldNotThrowAsync("Some additional context").Wait();
            }
            catch (AggregateException e)
            {
                var inner = e.Flatten().InnerException;
                var ex = inner.ShouldBeOfType<ShouldAssertException>();
                ex.Message.ShouldContainWithoutWhitespace(@"
                            `task` should not throw but threw System.InvalidOperationException with message ""exception message""
                            Additional Info: Some additional context");
            }
        }

        [Fact]
        public void ShouldThrowAWobbly_WithNestedTasks()
        {
            try
            {
                var task = Task.Factory.StartNew(() => {
                    var child1 = Task.Factory.StartNew(() => {
                        var child2 = Task.Factory.StartNew(() => {
                            throw new InvalidOperationException();
                        }, TaskCreationOptions.AttachedToParent);
                        throw new InvalidOperationException();
                    }, TaskCreationOptions.AttachedToParent);
                });

                task.ShouldNotThrowAsync("Some additional context").Wait();
            }
            catch (AggregateException e)
            {
                var inner = e.Flatten().InnerException;
                var ex = inner.ShouldBeOfType<ShouldAssertException>();
                ex.Message.ShouldContainWithoutWhitespace(@"
                            `task`
                            should not throw but threw
                            System.AggregateException");
                ex.Message.ShouldContainWithoutWhitespace(@"
                            Additional Info:
                            Some additional context");
            }
        }

        [Fact]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { } ,
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            task.ShouldNotThrowAsync().Wait();
            
        }
    }
}

