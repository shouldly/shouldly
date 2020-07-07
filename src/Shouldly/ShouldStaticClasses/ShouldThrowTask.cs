using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class Should
    {
        /*** Should.Throw(Task) ***/
        public static TException Throw<TException>(Task actual) where TException : Exception
        {
            return Throw<TException>(actual, () => null);
        }
        public static TException Throw<TException>(Task actual, string? customMessage) where TException : Exception
        {
            return Throw<TException>(actual, () => customMessage);
        }
        public static TException Throw<TException>(Task actual, [InstantHandle] Func<string?>? customMessage) where TException : Exception
        {
            return Throw<TException>(() => actual, customMessage);
        }

        /*** Should.Throw(Task) ***/
        public static Exception Throw(Task actual, Type exceptionType)
        {
            return Throw(actual, () => null, exceptionType);
        }
        public static Exception Throw(Task actual, string? customMessage, Type exceptionType)
        {
            return Throw(actual, () => customMessage, exceptionType);
        }
        public static Exception Throw(Task actual, [InstantHandle] Func<string?>? customMessage, Type exceptionType)
        {
            return Throw(() => actual, customMessage, exceptionType);
        }

        /*** Should.Throw(Func<Task>) ***/
        public static TException Throw<TException>([InstantHandle] Func<Task> actual) where TException : Exception
        {
            return Throw<TException>(actual, () => null);
        }
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, string? customMessage) where TException : Exception
        {
            return Throw<TException>(actual, () => customMessage);
        }
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, [InstantHandle] Func<string?>? customMessage) where TException : Exception
        {
            return Throw<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** Should.Throw(Func<Task>) ***/
        public static Exception Throw([InstantHandle] Func<Task> actual, Type exceptionType)
        {
            return Throw(actual, () => null, exceptionType);
        }
        public static Exception Throw([InstantHandle] Func<Task> actual, string? customMessage, Type exceptionType)
        {
            return Throw(actual, () => customMessage, exceptionType);
        }
        public static Exception Throw([InstantHandle] Func<Task> actual, [InstantHandle] Func<string?>? customMessage, Type exceptionType)
        {
            return Throw(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType);
        }

        /*** Should.Throw(Task, TimeSpan) ***/
        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => null);
        }
        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter, string? customMessage) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => customMessage);
        }
        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter, [InstantHandle] Func<string?>? customMessage) where TException : Exception
        {
            return Throw<TException>(() => actual, timeoutAfter, customMessage);
        }

        /*** Should.Throw(Task, TimeSpan) ***/
        public static Exception Throw(Task actual, TimeSpan timeoutAfter, Type exceptionType)
        {
            return Throw(actual, timeoutAfter, () => null, exceptionType);
        }
        public static Exception Throw(Task actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType)
        {
            return Throw(actual, timeoutAfter, () => customMessage, exceptionType);
        }
        public static Exception Throw(Task actual, TimeSpan timeoutAfter, [InstantHandle] Func<string?>? customMessage, Type exceptionType)
        {
            return Throw(() => actual, timeoutAfter, customMessage, exceptionType);
        }

        /*** Should.Throw(Func<Task>, TimeSpan) ***/
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => null);
        }
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, string? customMessage) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => customMessage);
        }
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, [InstantHandle] Func<string?>? customMessage) where TException : Exception
        {
            return ThrowInternal<TException>(actual, timeoutAfter, customMessage);
        }
        internal static TException ThrowInternal<TException>(
            [InstantHandle] Func<Task> actual, TimeSpan timeoutAfter,
            [InstantHandle] Func<string?>? customMessage,
            [CallerMemberName] string shouldlyMethod = null!) where TException : Exception
        {
            try
            {
                RunAndWait(actual, timeoutAfter, customMessage);
            }
            catch (ShouldlyTimeoutException)
            {
                throw;
            }
            catch (Exception e)
            {
                e = (e as AggregateException)?.InnerException ?? e;

                if (e is TException exception)
                    return exception;

                throw new ShouldAssertException(new TaskShouldlyThrowMessage(typeof(TException), e.GetType(), customMessage?.Invoke(), shouldlyMethod).ToString());
            }

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(typeof(TException), customMessage, shouldlyMethod).ToString());
        }

        /*** Should.Throw(Func<Task>, TimeSpan) ***/
        public static Exception Throw([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, Type exceptionType)
        {
            return Throw(actual, timeoutAfter, () => null, exceptionType);
        }
        public static Exception Throw([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType)
        {
            return Throw(actual, timeoutAfter, () => customMessage, exceptionType);
        }
        public static Exception Throw([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, [InstantHandle] Func<string?>? customMessage, Type exceptionType)
        {
            return ThrowInternal(actual, timeoutAfter, customMessage, exceptionType);
        }

        internal static Exception ThrowInternal(
            [InstantHandle] Func<Task> actual, TimeSpan timeoutAfter,
            [InstantHandle] Func<string?>? customMessage,
            Type exceptionType,
            [CallerMemberName] string shouldlyMethod = null!)
        {
            try
            {
                RunAndWait(actual, timeoutAfter, customMessage);
            }
            catch (ShouldlyTimeoutException)
            {
                throw;
            }
            catch (Exception e)
            {
                e = (e as AggregateException)?.InnerException ?? e;

                if (e.GetType() == exceptionType)
                {
                    return e;
                }

                throw new ShouldAssertException(new TaskShouldlyThrowMessage(exceptionType, e.GetType(), customMessage?.Invoke(), shouldlyMethod).ToString());
            }

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(exceptionType, customMessage, shouldlyMethod).ToString());
        }

        /*** Should.NotThrow(Task) ***/
        public static void NotThrow(Task action)
        {
            NotThrow(action, () => null);
        }
        public static void NotThrow(Task action, string? customMessage)
        {
            NotThrow(action, () => customMessage);
        }
        public static void NotThrow(Task action, [InstantHandle] Func<string?>? customMessage)
        {
            NotThrow(() => action, customMessage);
        }

        /*** Should.NotThrow(Task<T>) ***/
        public static T NotThrow<T>(Task<T> action)
        {
            return NotThrow(action, () => null);
        }
        public static T NotThrow<T>(Task<T> action, string? customMessage)
        {
            return NotThrow(action, () => customMessage);
        }
        public static T NotThrow<T>(Task<T> action, [InstantHandle] Func<string?>? customMessage)
        {
            return NotThrow(() => action, customMessage);
        }

        /*** Should.NotThrow(Func<Task>) ***/
        public static void NotThrow([InstantHandle] Func<Task> action)
        {
            NotThrow(action, () => null);
        }
        public static void NotThrow([InstantHandle] Func<Task> action, string? customMessage)
        {
            NotThrow(action, () => customMessage);
        }
        public static void NotThrow([InstantHandle] Func<Task> action, [InstantHandle] Func<string?>? customMessage)
        {
            NotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** Should.NotThrow(Task, TimeSpan) ***/
        public static void NotThrow(Task action, TimeSpan timeoutAfter)
        {
            NotThrow(action, timeoutAfter, () => null);
        }
        public static void NotThrow(Task action, TimeSpan timeoutAfter, string? customMessage)
        {
            NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static void NotThrow(Task action, TimeSpan timeoutAfter, [InstantHandle] Func<string?>? customMessage)
        {
            NotThrow(() => action, timeoutAfter, customMessage);
        }

        /*** Should.NotThrow(Func<Task>, TimeSpan) ***/
        public static void NotThrow([InstantHandle] Func<Task> action, TimeSpan timeoutAfter)
        {
            NotThrow(action, timeoutAfter, () => null);
        }
        public static void NotThrow([InstantHandle] Func<Task> action, TimeSpan timeoutAfter, string? customMessage)
        {
            NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static void NotThrow([InstantHandle] Func<Task> action, TimeSpan timeoutAfter, [InstantHandle] Func<string?>? customMessage)
        {
            NotThrowInternal(action, timeoutAfter, customMessage);
        }
        internal static void NotThrowInternal(
            [InstantHandle] Func<Task> action, TimeSpan timeoutAfter,
            [InstantHandle] Func<string?>? customMessage,
            [CallerMemberName] string shouldlyMethod = null!)
        {
            try
            {
                RunAndWait(action, timeoutAfter, customMessage);
            }
            catch (ShouldlyTimeoutException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ex = (ex as AggregateException)?.InnerException ?? ex;

                throw new ShouldAssertException(new TaskShouldlyThrowMessage(ex.GetType(), ex.Message, customMessage?.Invoke(), shouldlyMethod).ToString());
            }
        }

        /*** Should.NotThrow(Func<Task<T>>) ***/
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action)
        {
            return NotThrow(action, () => null);
        }
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, string? customMessage)
        {
            return NotThrow(action, () => customMessage);
        }
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, [InstantHandle] Func<string?>? customMessage)
        {
            return NotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** Should.NotThrow(Task<T>, TimeSpan) ***/
        public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter)
        {
            return NotThrow(action, timeoutAfter, () => null);
        }
        public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter, string? customMessage)
        {
            return NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter, [InstantHandle] Func<string?>? customMessage)
        {
            return NotThrow(() => action, timeoutAfter, customMessage);
        }

        /*** Should.NotThrow(Func<Task<T>>, TimeSpan) ***/
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter)
        {
            return NotThrow(action, timeoutAfter, () => null);
        }
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter, string? customMessage)
        {
            return NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter, [InstantHandle] Func<string?>? customMessage)
        {
            return NotThrowInternal(action, timeoutAfter, customMessage);
        }
        internal static T NotThrowInternal<T>(
            [InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter,
            [InstantHandle] Func<string?>? customMessage,
            [CallerMemberName] string shouldlyMethod = null!)
        {
            try
            {
                // Drop the sync context so continuations will not post to it, causing a deadlock
                using (Utils.WithSynchronizationContext(null))
                {
                    return CompleteIn(action, timeoutAfter, customMessage);
                }
            }
            catch (ShouldlyTimeoutException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ex = (ex as AggregateException)?.InnerException ?? ex;

                throw new ShouldAssertException(new TaskShouldlyThrowMessage(ex.GetType(), ex.Message, customMessage?.Invoke(), shouldlyMethod).ToString());
            }
        }

        private static void RunAndWait(Func<Task> actual, TimeSpan timeoutAfter, [InstantHandle] Func<string?>? customMessage)
        {
            // Drop the sync context so continuations will not post to it, causing a deadlock
            using (Utils.WithSynchronizationContext(null))
            {
                CompleteIn(actual, timeoutAfter, customMessage);
            }
        }

        private static Exception HandleTaskAggregateException(AggregateException exceptionFromTask, [InstantHandle] Func<string?>? customMessage, Type exceptionType)
        {
            var innerException = exceptionFromTask.InnerException
                ?? throw new ArgumentException("The specified exception is not from Task.Exception or it would have at least one inner exception.", nameof(exceptionFromTask));

            if (innerException.GetType() == exceptionType)
                return innerException;

            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(exceptionType, innerException.GetType(), customMessage).ToString());
        }
    }
}