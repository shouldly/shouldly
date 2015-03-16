#if net40
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shouldly
{
    public static partial class Should
    {
        public static TException Throw<TException>(Func<Task> actual) where TException : Exception
        {
            return Throw<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout);
        }

        public static TException Throw<TException>(Func<Task> actual, TimeSpan timeoutAfter) where TException : Exception
        {
            try
            {
                RunAndWait(actual, timeoutAfter);
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (AggregateException e)
            {
                var innerException = e.InnerException;
                if (innerException is TException)
                    return (TException)innerException;

                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(typeof(TException), innerException.GetType()).ToString());
            }
            catch (Exception e)
            {
                if (e is TException)
                    return (TException)e;

                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(typeof(TException), e.GetType()).ToString());
            }

            throw new ShouldAssertException(new ExpectedShouldlyMessage(typeof(TException)).ToString());
        }

        public static void NotThrow(Func<Task> action)
        {
            NotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout);
        }

        public static void NotThrow(Func<Task> action, TimeSpan timeoutAfter)
        {
            try
            {
                RunAndWait(action, timeoutAfter);
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (AggregateException ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.InnerException.GetType()).ToString());
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.GetType()).ToString());
            }
        }

        private static void RunAndWait(Func<Task> actual, TimeSpan timeoutAfter)
        {
            // Drop the sync context so continuations will not post to it, causing a deadlock
            if (SynchronizationContext.Current != null)
            {
                CompleteIn(Task.Factory.StartNew(actual, CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default).Unwrap(), timeoutAfter);
            }
            else
            {
                CompleteIn(actual, timeoutAfter);
            }
        }

        public static T NotThrow<T>(Func<Task<T>> action)
        {
            return NotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout);
        }

        public static T NotThrow<T>(Func<Task<T>> action, TimeSpan timeoutAfter)
        {
            try
            {
                // Drop the sync context so continuations will not post to it, causing a deadlock
                if (SynchronizationContext.Current != null)
                {
                    return CompleteIn(Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default).Unwrap(), timeoutAfter);
                }

                return CompleteIn(action, timeoutAfter);
            }
            catch (AggregateException ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.InnerException.GetType()).ToString());
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.GetType()).ToString());
            }
        }
    }
}
#endif