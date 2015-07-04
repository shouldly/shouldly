#if net40
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class Should
    {
        /*** Should.ThrowAsync(Task) ***/
        public static Task<TException> ThrowAsync<TException>(Task task) where TException : Exception
        {
            return ThrowAsync<TException>(task, () => null);
        }
        public static Task<TException> ThrowAsync<TException>(Task task, string customMessage) where TException : Exception
        {
            return ThrowAsync<TException>(task, () => customMessage);
        }
        public static Task<TException> ThrowAsync<TException>(Task task, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return ThrowAsync<TException>(() => task, customMessage);
        }

        /*** Should.ThrowAsync(Func<Task>) ***/
        public static Task<TException> ThrowAsync<TException>(Func<Task> actual) where TException : Exception
        {
            return ThrowAsync<TException>(actual, () => null);
        }
        public static Task<TException> ThrowAsync<TException>(Func<Task> actual, string customMessage) where TException : Exception
        {
            return ThrowAsync<TException>(actual, () => customMessage);
        }
        public static Task<TException> ThrowAsync<TException>(Func<Task> actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return actual().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    if (t.Exception == null)
                        throw new ShouldAssertException(new ShouldlyThrowShouldlyMessage(typeof(TException), customMessage).ToString());

                    return HandleAggregateException<TException>(t.Exception, customMessage);
                }

                if (t.IsCanceled)
                    throw new ShouldAssertException(new ShouldlyThrowShouldlyMessage(typeof(TException), customMessage).ToString()
                        , new TaskCanceledException("Task is cancelled"));

                throw new ShouldAssertException(new ShouldlyThrowShouldlyMessage(typeof(TException), customMessage).ToString());
            });
        }
    }
}
#endif