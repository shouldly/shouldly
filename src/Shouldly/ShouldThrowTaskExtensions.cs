using System.Threading;
#if net40
using System;
using System.Threading.Tasks;

namespace Shouldly
{
    public static partial class Should
    {
        public static TException Throw<TException>(Func<Task> actual) where TException : Exception
        {
            try
            {
                RunAndWait(actual);
            }
            catch (AggregateException e)
            {
                var innerException = e.InnerException;
                if (innerException is TException)
                    return (TException) innerException;

                throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException), innerException.GetType()).ToString());
            }
            catch (Exception e)
            {
                if (e is TException)
                    return (TException) e;

                throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException), e.GetType()).ToString());
            }

            throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException)).ToString());
        }

        public static void NotThrow(Func<Task> action)
        {
            try
            {
                RunAndWait(action);
            }
            catch (AggregateException ex)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.InnerException.GetType()).ToString());
            }
            catch (Exception ex)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.GetType()).ToString());
            }
        }

        private static void RunAndWait(Func<Task> actual)
        {
            // Drop the sync context so continuations will not post to it, causing a deadlock
            if (SynchronizationContext.Current != null)
            {
                Task.Factory.StartNew(actual, CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default).Unwrap().Wait();
            }
            else
            {
                actual().Wait();
            }
        }

        public static T NotThrow<T>(Func<Task<T>> action)
        {
            try
            {
                // Drop the sync context so continuations will not post to it, causing a deadlock
                if (SynchronizationContext.Current != null)
                {
                    return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default).Unwrap().Result;
                }
                
                return action().Result;
            }
            catch (AggregateException ex)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.InnerException.GetType()).ToString());
            }
            catch (Exception ex)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.GetType()).ToString());
            }
        }
    }
}
#endif