﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests.ShouldThrowAsync
{  
    public class FuncOfTaskScenarioAsync
   
    {

    [Fact]
        public void ShouldThrowAWobbly()
        {
            try
            {
                Task task = Task.Factory.StartNew(() => { var a = 1 + 1; Console.WriteLine(a); },
                        CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default);

                var result = task.ShouldThrowAsync<InvalidOperationException>("Some additional context");
                result.Wait();
            }
            catch (AggregateException e)
            {
                var inner = e.Flatten().InnerException;
                var ex = inner.ShouldBeOfType<ShouldAssertException>();
                ex.Message.ShouldContainWithoutWhitespace(@"
                            `task` should throw System.InvalidOperationException but did not
                            Additional Info:
                            Some additional context");
            }
        }
        
        [Fact]
        public async Task ShouldThrowAWobbly_WhenATaskIsCancelled()
        {
            // Arrange.
            // Cancel this calling code after 5 seconds.
            var cancellationTokenSource = new CancellationTokenSource(5);
            var task = Task.Delay(TimeSpan.FromSeconds(10), cancellationTokenSource.Token);

            // Act.
            var result = await Should.ThrowAsync<TaskCanceledException>(() => task);

            // Assert.
            result.ShouldNotBeNull();
        }

[Fact]
        public void ShouldThrowAWobbly_ExceptionTypePassedIn()
        {
            try
            {
                Task task = Task.Factory.StartNew(() => { var a = 1 + 1; Console.WriteLine(a); },
                        CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default);

                var result = task.ShouldThrowAsync("Some additional context", typeof(InvalidOperationException));
                result.Wait();
            }
            catch (AggregateException e)
            {
                var inner = e.Flatten().InnerException;
                var ex = inner.ShouldBeOfType<ShouldAssertException>();
                ex.Message.ShouldContainWithoutWhitespace(@"
                            `task` should throw System.InvalidOperationException but did not
                            Additional Info:
                            Some additional context");
            }
        }

        [Fact]
        public void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var result = task.ShouldThrowAsync<InvalidOperationException>();
            result.Wait();
        }

[Fact]
        public void ShouldPass_ExceptionTypePassedIn()
        {
            var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            var result = task.ShouldThrowAsync(typeof(InvalidOperationException));
            result.Wait();
        }

        [Fact] // Issue 554
        public async Task ShouldThrowAssertException()
        {
            try
            {
                Func<Task> doSomething = () => throw new DivideByZeroException();
                await Should.ThrowAsync<TimeoutException>(() => doSomething());
            }
            catch (Exception e)
            {
                var ex = e.ShouldBeOfType<ShouldAssertException>();
                ex.Message.ShouldContainWithoutWhitespace(@"
                    Task `doSomething()`
                    should throw 
                    System.TimeoutException
                    but threw
                    System.DivideByZeroException");
            }
        }

        [Fact] // Issue 554
        public async Task AsyncShouldThrowAssertException()
        {
            try
            {
                Func<Task> doSomething = () => throw new DivideByZeroException();
                await Should.ThrowAsync<TimeoutException>(async () => await doSomething());
            }
            catch (Exception e)
            {
                var ex = e.ShouldBeOfType<ShouldAssertException>();
                ex.Message.ShouldContainWithoutWhitespace(@"
                    Task `async () => await doSomething()`
                    should throw 
                    System.TimeoutException
                    but threw
                    System.DivideByZeroException");
            }
        }        
    }
}