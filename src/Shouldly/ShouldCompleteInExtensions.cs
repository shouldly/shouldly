using System.Linq;
#if net40
using System;
using System.Threading.Tasks;

namespace Shouldly
{
    public static partial class Should
    {
        public static void CompleteIn(Action action, TimeSpan timeout)
        {
            var actual = Task.Factory.StartNew(action);

            CompleteIn(actual, timeout);
        }

        public static void CompleteIn<T>(Func<T> function, TimeSpan timeout)
        {
            var actual = Task.Factory.StartNew(function);

            CompleteIn(actual, timeout);
        }

        public static void CompleteIn(Task actual, TimeSpan timeout)
        {
            try
            {
                actual.TimeoutAfter(timeout).Wait();
            }
            catch (AggregateException ae)
            {
                var te = ae.InnerExceptions.FirstOrDefault(e => e is TimeoutException);

                throw te ?? ae;
            }
        }

        public static void CompleteIn<T>(Task<T> actual, TimeSpan timeout)
        {
            try
            {
                actual.TimeoutAfter(timeout).Wait();
            }
            catch (AggregateException ae)
            {
                var te = ae.InnerExceptions.FirstOrDefault(e => e is TimeoutException);

                throw te ?? ae;
            }
        }
    }
}
#endif