#if net40
using System.Reflection;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace Shouldly
{
    public static partial class Should
    {
        public static void CompleteIn(Action action, TimeSpan timeout)
        {
            var actual = Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default);

            CompleteIn(actual, timeout);
        }

        public static T CompleteIn<T>(Func<T> function, TimeSpan timeout)
        {
            var actual = Task.Factory.StartNew(function, CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default);

            return CompleteIn(actual, timeout);
        }

        public static void CompleteIn(Task actual, TimeSpan timeout)
        {
            try
            {
                actual.TimeoutAfter(timeout).Wait();
            }
            catch (AggregateException ae)
            {
                var flattened = ae.Flatten();
                if (flattened.InnerExceptions.Count != 1) 
                    throw;

                var inner = flattened.InnerException;
                PreserveStackTrace(inner);
                throw inner;
            }
        }

        public static T CompleteIn<T>(Task<T> actual, TimeSpan timeout)
        {
            try
            {
                actual.TimeoutAfter(timeout).Wait();
            }
            catch (AggregateException ae)
            {
                var flattened = ae.Flatten();
                if (flattened.InnerExceptions.Count != 1)
                    throw;

                var inner = flattened.InnerException;
                PreserveStackTrace(inner);
                throw inner;
            }

            return actual.Result;
        }

        private static void PreserveStackTrace(Exception exception)
        {
            MethodInfo preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
              BindingFlags.Instance | BindingFlags.NonPublic);
            preserveStackTrace.Invoke(exception, null);
        }
    }
}
#endif