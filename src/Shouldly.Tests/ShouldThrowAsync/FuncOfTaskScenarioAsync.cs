﻿using System.Diagnostics;
using Xunit.Sdk;

namespace Shouldly.Tests.ShouldThrowAsync;

public class FuncOfTaskScenarioAsync
{
    [Fact]
    public async Task ShouldThrowAWobbly()
    {
        try
        {
            var task = Task.Run(() =>
                {
                    var a = 1 + 1;
                    Debug.WriteLine(a);
                });

            await task.ShouldThrowAsync<InvalidOperationException>("Some additional context");
        }
        catch (ShouldAssertException ex)
        {
            ex.Message.ShouldContainWithoutWhitespace(
                """
                `await task` should throw System.InvalidOperationException but did not
                Additional Info:
                Some additional context
                """);
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
    public async Task ShouldThrowAWobbly_ExceptionTypePassedIn()
    {
        try
        {
            var task = Task.Run(() =>
                {
                    var a = 1 + 1;
                    Debug.WriteLine(a);
                });

            await task.ShouldThrowAsync(typeof(InvalidOperationException), "Some additional context");
        }
        catch (ShouldAssertException ex)
        {
            ex.Message.ShouldContainWithoutWhitespace(
                """
                `await task` should throw System.InvalidOperationException but did not
                Additional Info:
                Some additional context
                """);
        }
    }

    [Fact]
    public async Task ShouldPass()
    {
        var task = Task.Run(() => throw new InvalidOperationException());

        await task.ShouldThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task ShouldPass_ExceptionTypePassedIn()
    {
        var task = Task.Run(() => throw new InvalidOperationException());

        await task.ShouldThrowAsync(typeof(InvalidOperationException));
    }

    [Fact]
    public async Task ShouldThrowAsync()
    {
        try
        {
            #region ShouldThrowAsync
            Func<Task> doSomething = async () =>
            {
                await Task.Delay(1);
            };
            var exception = await Should.ThrowAsync<DivideByZeroException>(() => doSomething());
            #endregion
        }
        catch (Exception e)
        {
            var ex = e.ShouldBeOfType<ShouldAssertException>();
            ex.Message.ShouldMatchApproved();
        }
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
            ex.Message.ShouldContainWithoutWhitespace(
                """
                Task `doSomething()`
                should throw
                System.TimeoutException
                but threw
                System.DivideByZeroException
                """);
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
            ex.Message.ShouldContainWithoutWhitespace(
                """
                Task `async () => await doSomething()`
                should throw
                System.TimeoutException
                but threw
                System.DivideByZeroException
                """);
        }
    }

    [Fact] // Issue 818
    public async Task ShouldThrowAssertException_ExceptionTypeIsException()
    {
        try
        {
            var doSomething = () => Task.CompletedTask;
            await Should.ThrowAsync<Exception>(async () => await doSomething());
        }
        catch (Exception e)
        {
            var ex = e.ShouldBeOfType<ShouldAssertException>();
            ex.Message.ShouldContainWithoutWhitespace(
                """
                Task `async () => await doSomething()`
                should throw
                System.Exception
                but did not
                """);
            return;
        }

        throw new XunitException("ShouldThrowAsync did not throw");
    }
}