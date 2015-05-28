#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class Should
    {
        /*** ShouldThrow(Task) ***/
        public static TException ShouldThrow<TException>(this Task actual) where TException : Exception
        {
            return Throw<TException>(actual);
        }
        public static TException ShouldThrow<TException>(this Task actual, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, customMessage);
        }
        public static TException ShouldThrow<TException>(this Task actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(actual, customMessage);
        }

        /*** Should.Throw(Task) ***/
        public static TException Throw<TException>(Task actual) where TException : Exception
        {
            return Throw<TException>(actual, () => null);
        }
        public static TException Throw<TException>(Task actual, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, () => customMessage);
        }
        public static TException Throw<TException>(Task actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(() => actual, customMessage);
        }

        /*** ShouldThrow(Func<Task>) ***/
        public static TException ShouldThrow<TException>(this Func<Task> actual) where TException : Exception
        {
            return Throw<TException>(actual);
        }
        public static TException ShouldThrow<TException>(this Func<Task> actual, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, customMessage);
        }
        public static TException ShouldThrow<TException>(this Func<Task> actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(actual, customMessage);
        }

        /*** Should.Throw(Func<Task>) ***/
        public static TException Throw<TException>([InstantHandle] Func<Task> actual) where TException : Exception
        {
            return Throw<TException>(actual, () => null);
        }
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, () => customMessage);
        }
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** ShouldThrow(Task, TimeSpan) ***/
        public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter);
        }
        public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, customMessage);
        }
        public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, customMessage);
        }

        /*** Should.Throw(Task, TimeSpan) ***/
        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => null);            
        }
        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => customMessage);
        }
        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(() => actual, timeoutAfter, customMessage);            
        }

        /*** ShouldThrow(Func<Task>, TimeSpan) ***/
        public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter);
        }
        public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, customMessage);
        }
        public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, customMessage);
        }

        /*** Should.Throw(Func<Task>, TimeSpan) ***/
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => null);            
        }
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, timeoutAfter, () => customMessage);
        }
        public static TException Throw<TException>([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            try
            {
                RunAndWait(actual, timeoutAfter, customMessage);
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

                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(typeof(TException), e.GetType(), customMessage).ToString());
            }

            throw new ShouldAssertException(new ExpectedShouldlyMessage(typeof(TException), customMessage).ToString());
        }

        /*** ShouldNotThrow(Task) ***/
        public static void ShouldNotThrow(this Task action)
        {
            NotThrow(action);
        }
        public static void ShouldNotThrow(this Task action, string customMessage)
        {
            NotThrow(action, customMessage);
        }
        public static void ShouldNotThrow(this Task action, [InstantHandle] Func<string> customMessage)
        {
            NotThrow(action, customMessage);
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
        public static void NotThrow(Task action, [InstantHandle] Func<string> customMessage)
        {
            NotThrow(() => action, customMessage);
        }

        /*** ShouldNotThrow(Task<T>) ***/
        public static T ShouldNotThrow<T>(this Task<T> action)
        {
            return NotThrow(action);
        }
        public static T ShouldNotThrow<T>(this Task<T> action, string customMessage)
        {
            return NotThrow(action, customMessage);
        }
        public static T ShouldNotThrow<T>(this Task<T> action, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(action, customMessage);
        }

        /*** Should.NotThrow(Task<T>) ***/
        public static T NotThrow<T>(Task<T> action)
        {
            return NotThrow(action, () => null);
        }
        public static T NotThrow<T>(Task<T> action, string customMessage)
        {
            return NotThrow(action, () => customMessage);
        }
        public static T NotThrow<T>(Task<T> action, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(() => action, customMessage);
        }

        /*** ShouldNotThrow(Func<Task>) ***/
        public static void ShouldNotThrow(this Func<Task> action)
        {
            NotThrow(action);
        }
        public static void ShouldNotThrow(this Func<Task> action, string customMessage)
        {
            NotThrow(action, customMessage);
        }
        public static void ShouldNotThrow(this Func<Task> action, [InstantHandle] Func<string> customMessage)
        {
            NotThrow(action, customMessage);
        }

        /*** Should.NotThrow(Func<Task>) ***/
        public static void NotThrow([InstantHandle] Func<Task> action)
        {
            NotThrow(action, () => null);
        }
        public static void NotThrow([InstantHandle] Func<Task> action, string customMessage)
        {
            NotThrow(action, () => customMessage);
        }
        public static void NotThrow([InstantHandle] Func<Task> action, [InstantHandle] Func<string> customMessage)
        {
            NotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** ShouldNotThrow(Task, TimeSpan) ***/
        public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter)
        {
            NotThrow(action, timeoutAfter);
        }
        public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, string customMessage)
        {
            NotThrow(action, timeoutAfter, customMessage);
        }
        public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            NotThrow(action, timeoutAfter, customMessage);
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
        public static void NotThrow(Task action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            NotThrow(() => action, timeoutAfter, customMessage);
        }

        /*** ShouldNotThrow(Func<Task>, TimeSpan) ***/
        public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter)
        {
            NotThrow(action, timeoutAfter);
        }
        public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, string customMessage)
        {
            NotThrow(action, timeoutAfter, customMessage);
        }
        public static void ShouldNotThrow([InstantHandle] Func<Task> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            NotThrow(action, timeoutAfter, customMessage);
        }

        /*** Should.NotThrow(Func<Task>, TimeSpan) ***/
        public static void NotThrow([InstantHandle] Func<Task> action, TimeSpan timeoutAfter)
        {
            NotThrow(action, timeoutAfter, () => null);
        }
        public static void NotThrow([InstantHandle] Func<Task> action, TimeSpan timeoutAfter, string customMessage)
        {
            NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static void NotThrow([InstantHandle] Func<Task> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            try
            {
                RunAndWait(action, timeoutAfter, customMessage);
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (AggregateException ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.InnerException.GetType(), customMessage).ToString());
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.GetType(), customMessage).ToString());
            }
        }

        /*** ShouldNotThrow(Func<Task<T>>) ***/
        public static T ShouldNotThrow<T>(this Func<Task<T>> action)
        {
            return NotThrow(action);
        }
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, string customMessage)
        {
            return NotThrow(action, customMessage);
        }
        public static T ShouldNotThrow<T>([InstantHandle] Func<Task<T>> action, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(action, customMessage);
        }

        /*** Should.NotThrow(Func<Task<T>>) ***/
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action)
        {
            return NotThrow(action, () => null);  
        }
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, string customMessage)
        {
            return NotThrow(action, () => customMessage);            
        }
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** ShouldNotThrow(Task<T>, TimeSpan) ***/
        public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter)
        {
            return NotThrow(action, timeoutAfter);
        }
        public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, string customMessage)
        {
            return NotThrow(action, timeoutAfter, customMessage);
        }
        public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(() => action, timeoutAfter, customMessage);
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
        public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(() => action, timeoutAfter, customMessage);
        }

        /*** ShouldNotThrow(Func<Task<T>>, TimeSpan) ***/
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter)
        {
            return NotThrow(action, timeoutAfter);
        }
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, string customMessage)
        {
            return NotThrow(action, timeoutAfter, customMessage);
        }
        public static T ShouldNotThrow<T>([InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(action, timeoutAfter, customMessage);
        }

        /*** Should.NotThrow(Func<Task<T>>, TimeSpan) ***/
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter)
        {
            return NotThrow(action, timeoutAfter, () => null);
        }
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter, string customMessage)
        {
            return NotThrow(action, timeoutAfter, () => customMessage);
        }
        public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            try
            {
                // Drop the sync context so continuations will not post to it, causing a deadlock
                if (SynchronizationContext.Current != null)
                {
                    return CompleteIn(Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default).Unwrap(), timeoutAfter, customMessage);
                }

                return CompleteIn(action, timeoutAfter, customMessage);
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (AggregateException ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.InnerException.GetType(), customMessage).ToString());
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.GetType(), customMessage).ToString());
            }
        }

        private static void RunAndWait(Func<Task> actual, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            // Drop the sync context so continuations will not post to it, causing a deadlock
            if (SynchronizationContext.Current != null)
            {
                CompleteIn(Task.Factory.StartNew(actual, CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default).Unwrap(), timeoutAfter, customMessage);
            }
            else
            {
                CompleteIn(actual, timeoutAfter, customMessage);
            }
        }

        private static TException HandleAggregateException<TException>(AggregateException e, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            var innerException = e.InnerException;
            if (innerException is TException)
                return (TException)innerException;

            throw new ShouldAssertException(
                new ExpectedActualShouldlyMessage(typeof(TException), innerException.GetType(), customMessage).ToString());
        }
    }
}
#endif