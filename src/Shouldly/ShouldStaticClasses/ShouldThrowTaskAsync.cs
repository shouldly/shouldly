using System;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

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

        public static Task<Exception> ThrowAsync(Task task, Type exceptionType)
        {
            return ThrowAsync(task, null, exceptionType);
        }

        public static Task<Exception> ThrowAsync(Task task, string? customMessage, Type exceptionType)
        {
            return ThrowAsync(() => task, () => customMessage, exceptionType);
        }

        /*** Should.ThrowAsync(Func<Task>) ***/

        public static Task<TException> ThrowAsync<TException>(Func<Task> actual, string? customMessage = null)
            where TException : Exception
        {
#if StackTrace
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
                            return new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), t.Exception.InnerException!.GetType(), () => customMessage, stackTrace).ToString());
                        }

                        if (t.IsCanceled)
                        {
                            return new TaskCanceledException(t);
                        }

                        return new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), () => customMessage, stackTrace).ToString());
                    }))
                    .ContinueWith(x =>
                    {
                        switch (x.Result)
                        {
                            case TException expectedException:
                                return expectedException;
                            case ShouldAssertException assert:
                                throw assert;
                            default:
                                throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), x.Result.GetType(), () => customMessage, stackTrace).ToString(), x.Result);
                        }
                    });
            }
            catch (Exception e)
            {
                if (e is TException exception)
                    return Task.FromResult(exception);

                throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), e.GetType(), () => customMessage, stackTrace).ToString());
            }
#else
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
                            return new ShouldAssertException(new TaskShouldlyThrowMessage(typeof(TException), t.Exception.InnerException!.GetType(), customMessage).ToString());
                        }

                        if (t.IsCanceled)
                        {
                            return new TaskCanceledException(t);
                        }

                        return new ShouldAssertException(new TaskShouldlyThrowMessage(typeof(TException), () => customMessage).ToString());
                    }))
                    .ContinueWith(x =>
                    {
                        switch (x.Result)
                        {
                            case TException expectedException:
                                return expectedException;
                            case ShouldAssertException assert:
                                throw assert;
                            default:
                                throw new ShouldAssertException(
                                    new TaskShouldlyThrowMessage(typeof(TException), () => customMessage).ToString()
                                    , x.Result);
                        }
                    });
            }
            catch (Exception e)
            {
                if (e is TException exception)
                    return Task.FromResult(exception);

                throw new ShouldAssertException(new TaskShouldlyThrowMessage(typeof(TException), e.GetType(), customMessage).ToString());
            }
#endif
        }

        /*** Should.ThrowAsync(Func<Task>) ***/
        public static Task<Exception> ThrowAsync(Func<Task> actual, Type exceptionType)
        {
            return ThrowAsync(actual, () => null, exceptionType);
        }

        public static Task<Exception> ThrowAsync(Func<Task> actual, string? customMessage, Type exceptionType)
        {
            return ThrowAsync(actual, () => customMessage, exceptionType);
        }

        public static Task<Exception> ThrowAsync(Func<Task> actual, [InstantHandle] Func<string?>? customMessage, Type exceptionType)
        {
#if StackTrace
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
                    throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace).ToString()
                        , new TaskCanceledException("Task is cancelled"));

                throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace).ToString());
            });
#else
            return actual().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    if (t.Exception == null)
                        throw new ShouldAssertException(new TaskShouldlyThrowMessage(exceptionType, customMessage).ToString());

                    return HandleTaskAggregateException(t.Exception, customMessage, exceptionType);
                }

                if (t.IsCanceled)
                    throw new ShouldAssertException(new TaskShouldlyThrowMessage(exceptionType, customMessage).ToString()
                        , new TaskCanceledException("Task is cancelled"));

                throw new ShouldAssertException(new TaskShouldlyThrowMessage(exceptionType, customMessage).ToString());
            });
#endif
        }

        /*** Should.NotThrowAsync(Task) ***/
        public static Task NotThrowAsync(Task task, string? customMessage = null)
        {
            return NotThrowAsyncInternal(() => task, () => customMessage);
        }

        /*** Should.NotThrowAsync(Func<Task>) ***/
        public static Task NotThrowAsync(Func<Task> actual, string? customMessage = null)
        {
            return NotThrowAsyncInternal(actual, () => customMessage);
        }

        internal static Task NotThrowAsyncInternal(
            [InstantHandle] Func<Task> actual,
            [InstantHandle] Func<string?>? customMessage,
            [CallerMemberName] string shouldlyMethod = null!)
        {
#if StackTrace
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

#else
            return actual().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var flattened = t.Exception!.Flatten();
                    if (flattened.InnerExceptions.Count == 1 && flattened.InnerException != null)
                    {
                        var inner = flattened.InnerException;
                        throw new ShouldAssertException(new TaskShouldlyThrowMessage(inner.GetType(), inner.Message, customMessage?.Invoke(), shouldlyMethod).ToString());
                    }

                    throw new ShouldAssertException(new TaskShouldlyThrowMessage(t.Exception.GetType(), t.Exception.Message, customMessage?.Invoke(), shouldlyMethod).ToString());
                }
            });
#endif
        }
    }
}