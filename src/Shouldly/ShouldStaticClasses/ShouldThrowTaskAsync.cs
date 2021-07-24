using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class Should
    {
        /*** Should.ThrowAsync(Task) ***/

        public static Task<TException> ThrowAsync<TException>(Task task, string? customMessage = null)
            where TException : Exception
        {
            return ThrowAsync<TException>(() => task, customMessage);
        }

        public static Task<Exception> ThrowAsync(Task task, Type exceptionType, string? customMessage = null)
        {
            return ThrowAsync(() => task, exceptionType, customMessage);
        }

        /*** Should.ThrowAsync(Func<Task>) ***/

        public static Task<TException> ThrowAsync<TException>(Func<Task> actual, string? customMessage = null)
            where TException : Exception
        {
            var stackTrace = new StackTrace(true);
            try
            {
                return actual()
                    .ContinueWith(new Func<Task, Exception>(t =>
                    {
                        if (t.IsFaulted)
                        {
                            if (t.Exception!.InnerException is TException expectedException)
                                return expectedException;

                            // If Task.IsFaulted is true, there is at least one inner exception.
                            return new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), t.Exception.InnerException!.GetType(), customMessage, stackTrace).ToString());
                        }

                        if (t.IsCanceled)
                        {
                            return new TaskCanceledException(t);
                        }

                        return new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), customMessage, stackTrace).ToString());
                    }))
                    .ContinueWith(x =>
                    {
                        switch (x.Result)
                        {
                            case ShouldAssertException assert:
                                throw assert;
                            case TException expectedException:
                                return expectedException;
                            default:
                                throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), x.Result.GetType(), customMessage, stackTrace).ToString(), x.Result);
                        }
                    });
            }
            catch (Exception e)
            {
                if (e is TException exception)
                    return Task.FromResult(exception);

                throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), e.GetType(), customMessage, stackTrace).ToString());
            }
        }

        /*** Should.ThrowAsync(Func<Task>) ***/
        public static Task<Exception> ThrowAsync(Func<Task> actual, Type exceptionType, string? customMessage = null)
        {
            var stackTrace = new StackTrace(true);
            return actual().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    if (t.Exception == null)
                        throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace).ToString());

                    return HandleTaskAggregateException(t.Exception, customMessage, exceptionType);
                }

                if (t.IsCanceled)
                {
                    throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace).ToString(),
                        new TaskCanceledException("Task is cancelled"));
                }

                throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace).ToString());
            });
        }

        /*** Should.NotThrowAsync(Task) ***/
        public static Task NotThrowAsync(Task task, string? customMessage = null)
        {
            return NotThrowAsyncInternal(() => task, customMessage);
        }

        /*** Should.NotThrowAsync(Func<Task>) ***/
        public static Task NotThrowAsync(Func<Task> actual, string? customMessage = null)
        {
            return NotThrowAsyncInternal(actual, customMessage);
        }

        internal static Task NotThrowAsyncInternal(
            [InstantHandle] Func<Task> actual,
            string? customMessage,
            [CallerMemberName] string shouldlyMethod = null!)
        {
            var stackTrace = new StackTrace(true);
            return actual().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var flattened = t.Exception!.Flatten();
                    if (flattened.InnerExceptions.Count == 1 && flattened.InnerException != null)
                    {
                        var inner = flattened.InnerException;
                        throw new ShouldAssertException(new AsyncShouldlyNotThrowShouldlyMessage(inner.GetType(), customMessage, stackTrace, inner.Message, shouldlyMethod).ToString());
                    }

                    throw new ShouldAssertException(new AsyncShouldlyNotThrowShouldlyMessage(t.Exception.GetType(), customMessage, stackTrace, t.Exception.Message, shouldlyMethod).ToString());
                }
            });
        }
    }
}