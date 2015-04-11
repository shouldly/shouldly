#if net40
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shouldly
{
    public static partial class Should
    {
        /*** Should.Throw(Task) ***/
        public static TException Throw<TException>(Task actual) where TException : Exception
        {
            return Throw<TException>(actual, () => null);
        }
        public static TException Throw<TException>(Task actual, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, () => customMessage);
        }
        public static TException Throw<TException>(Task actual, Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(() => actual, customMessage);
        }

        /*** Should.Throw(Func<Task>) ***/
        public static TException Throw<TException>(Func<Task> actual) where TException : Exception
        {
            return Throw<TException>(actual, () => null);
        }
        public static TException Throw<TException>(Func<Task> actual, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, () => customMessage);
        }
        public static TException Throw<TException>(Func<Task> actual, Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** Should.Throw(Task, TimeSpan) ***/
        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter)
            where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => null);            
        }
        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter, string customMessage)
            where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => customMessage);
        }

        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter, Func<string> customMessage)
            where TException : Exception
        {
            return Throw<TException>(() => actual, timeoutAfter, customMessage);            
        }

        /*** Should.Throw(Func<Task>, TimeSpan) ***/
        public static TException Throw<TException>(Func<Task> actual, TimeSpan timeoutAfter)
            where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => null);            
        }
        public static TException Throw<TException>(Func<Task> actual, TimeSpan timeoutAfter, string customMessage)
            where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => customMessage);
        }
        public static TException Throw<TException>(Func<Task> actual, TimeSpan timeoutAfter, Func<string> customMessage) where TException : Exception
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
                return HandleAggregateException<TException>(e, customMessage);
            }
            catch (Exception e)
            {
                if (e is TException)
                    return (TException)e;

                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(typeof(TException), e.GetType(), customMessage()).ToString());
            }

            throw new ShouldAssertException(new ExpectedShouldlyMessage(typeof(TException), customMessage()).ToString());
        }

        /*** Should.NotThrow(Task) ***/
        public static void NotThrow(Task action)
        {
            NotThrow(action, () => null);
        }
        public static void NotThrow(Task action, string customMessage)
        {
            NotThrow(action, () => customMessage);
        }
        public static void NotThrow(Task action, Func<string> customMessage)
        {
            NotThrow(() => action, customMessage);
        }

        /*** Should.NotThrow(Func<Task>) ***/
        public static void NotThrow(Func<Task> action)
        {
            NotThrow(action, () => null);
        }
        public static void NotThrow(Func<Task> action, string customMessage)
        {
            NotThrow(action, () => customMessage);
        }
        public static void NotThrow(Func<Task> action, Func<string> customMessage)
        {
            NotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout);
        }

        /*** Should.NotThrow(Task, TimeSpan) ***/
        public static void NotThrow(Task action, TimeSpan timeoutAfter)
        {
            NotThrow(action, timeoutAfter, () => null);
        }
        public static void NotThrow(Task action, TimeSpan timeoutAfter, string customMessage)
        {
            NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static void NotThrow(Task action, TimeSpan timeoutAfter, Func<string> customMessage)
        {
            NotThrow(() => action, timeoutAfter, customMessage);
        }

        /*** Should.NotThrow(Func<Task>, TimeSpan) ***/
        public static void NotThrow(Func<Task> action, TimeSpan timeoutAfter)
        {
            NotThrow(action, timeoutAfter, () => null);
        }
        public static void NotThrow(Func<Task> action, TimeSpan timeoutAfter, string customMessage)
        {
            NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static void NotThrow(Func<Task> action, TimeSpan timeoutAfter, Func<string> customMessage)
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

        /*** Should.NotThrow(Func<Task<T>>) ***/
        public static T NotThrow<T>(Func<Task<T>> action)
        {
            return NotThrow(action, () => null);  
        }
        public static T NotThrow<T>(Func<Task<T>> action, string customMessage)
        {
            return NotThrow(action, () => customMessage);            
        }
        public static T NotThrow<T>(Func<Task<T>> action, Func<string> customMessage)
        {
            return NotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** Should.NotThrow(Task<T>, TimeSpan) ***/
        public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter)
        {
            return NotThrow(action, timeoutAfter, () => null);
        }
        public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter, string customMessage)
        {
            return NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter, Func<string> customMessage)
        {
            return NotThrow(() => action, timeoutAfter, customMessage);
        }

        /*** Should.NotThrow(Func<Task<T>>, TimeSpan) ***/
        public static T NotThrow<T>(Func<Task<T>> action, TimeSpan timeoutAfter)
        {
            return NotThrow(action, timeoutAfter, () => null);
        }
        public static T NotThrow<T>(Func<Task<T>> action, TimeSpan timeoutAfter, string customMessage)
        {
            return NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static T NotThrow<T>(Func<Task<T>> action, TimeSpan timeoutAfter, Func<string> customMessage)
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

        private static TException HandleAggregateException<TException>(AggregateException e, Func<string> customMessage) where TException : Exception
        {
            var innerException = e.InnerException;
            if (innerException is TException)
                return (TException)innerException;

            throw new ShouldAssertException(
                new ExpectedActualShouldlyMessage(typeof(TException), innerException.GetType(), customMessage()).ToString());
        }
    }
}
#endif