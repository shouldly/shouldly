using System.Threading;
using System;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class Should
    {
        /*** CompleteIn(Action) ***/
        public static void CompleteIn(Action action, TimeSpan timeout, string? customMessage = null)
        {
            var actual = Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default);
            CompleteIn(actual, timeout, () => customMessage, "Delegate");
        }

        /*** CompleteIn(Func<T>) ***/
        public static T CompleteIn<T>(Func<T> function, TimeSpan timeout, string? customMessage = null)
        {
            var actual = Task.Factory.StartNew(function, CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default);

            return CompleteIn(actual, timeout, () => customMessage, "Delegate");
        }

        /*** CompleteIn(Func<Task>) ***/
        public static void CompleteIn(Func<Task> actual, TimeSpan timeout, string? customMessage = null)
        {
            CompleteIn(actual(), timeout, () => customMessage, "Task");
        }

        /*** CompleteIn(Func<Task<T>>) ***/
        public static T CompleteIn<T>(Func<Task<T>> actual, TimeSpan timeout, string? customMessage = null)
        {
            return CompleteIn(actual(), timeout, () => customMessage, "Task");
        }

        /*** CompleteIn(Task<T>) ***/
        public static void CompleteIn(Task actual, TimeSpan timeout, string? customMessage = null)
        {
            CompleteIn(actual, timeout, () => customMessage, "Task");
        }

        /*** CompleteIn(Task<T>) ***/
        public static T CompleteIn<T>(Task<T> actual, TimeSpan timeout, string? customMessage = null)
        {
            return CompleteIn(actual, timeout, () => customMessage, "Task");
        }

        private static T CompleteIn<T>(Task<T> actual, TimeSpan timeout, [InstantHandle] Func<string?>? customMessage, string what)
        {
            CompleteIn((Task)actual, timeout, customMessage, what);
            return actual.Result;
        }

        private static void CompleteIn(Task actual, TimeSpan timeout, [InstantHandle] Func<string?>? customMessage, string what)
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

                var inner = flattened.InnerException!;
                // When exception is a timeout exception we can provide a better error, otherwise rethrow
                if (inner is ShouldlyTimeoutException exception)
                {
                    var message = new CompleteInShouldlyMessage(what, timeout, customMessage).ToString();
                    throw new ShouldCompleteInException(message, exception);
                }

                PreserveStackTrace(inner);
                throw inner;
            }
        }

        private static void PreserveStackTrace(Exception exception)
        {
            // TODO Need to sort this out for core
            var preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
              BindingFlags.Instance | BindingFlags.NonPublic);

            preserveStackTrace?.Invoke(exception, null);
        }
    }
}