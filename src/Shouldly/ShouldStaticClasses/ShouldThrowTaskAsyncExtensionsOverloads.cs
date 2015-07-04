#if net40
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class Should
    {
        /*** ShouldThrowAsync(Task) ***/
        public static Task<TException> ShouldThrowAsync<TException>(this Task task) where TException : Exception
        {
            return ThrowAsync<TException>(task);
        }
        public static Task<TException> ShouldThrowAsync<TException>(this Task task, string customMessage) where TException : Exception
        {
            return ThrowAsync<TException>(task, customMessage);
        }
        public static Task<TException> ShouldThrowAsync<TException>(this Task task, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return ThrowAsync<TException>(task, customMessage);
        }

        /*** ShouldThrowAsync(Func<Task>) ***/
        public static Task<TException> ShouldThrowAsync<TException>(this Func<Task> actual) where TException : Exception
        {
            return ThrowAsync<TException>(actual);
        }
        public static Task<TException> ShouldThrowAsync<TException>(this Func<Task> actual, string customMessage) where TException : Exception
        {
            return ThrowAsync<TException>(actual, customMessage);
        }
        public static Task<TException> ShouldThrowAsync<TException>(this Func<Task> actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return ThrowAsync<TException>(actual, customMessage);
        }
    }
}
#endif