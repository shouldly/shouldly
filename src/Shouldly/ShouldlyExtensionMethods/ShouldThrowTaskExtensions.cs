#if Async
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldThrowTaskExtensions
    {
        /*** ShouldThrow(Task) ***/
        public static TException ShouldThrow<TException>(this Task actual) where TException : Exception
        {
            return ShouldThrow<TException>(() => actual);
        }
        public static TException ShouldThrow<TException>(this Task actual, string customMessage) where TException : Exception
        {
            return ShouldThrow<TException>(() => actual, customMessage);
        }
        public static TException ShouldThrow<TException>(this Task actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return ShouldThrow<TException>(() => actual, customMessage);
        }

        /*** ShouldThrow(Func<Task>) ***/
        public static TException ShouldThrow<TException>(this Func<Task> actual) where TException : Exception
        {
            return ShouldThrow<TException>(actual, () => null);
        }
        public static TException ShouldThrow<TException>(this Func<Task> actual, string customMessage) where TException : Exception
        {
            return ShouldThrow<TException>(actual, () => customMessage);
        }
        public static TException ShouldThrow<TException>(this Func<Task> actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return ShouldThrow<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** ShouldThrow(Task, TimeSpan) ***/
        public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter) where TException : Exception
        {
            return ShouldThrow<TException>(() => actual, timeoutAfter);
        }
        public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter, string customMessage) where TException : Exception
        {
            return ShouldThrow<TException>(() => actual, timeoutAfter, customMessage);
        }
        public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return ShouldThrow<TException>(() => actual, timeoutAfter, customMessage);
        }

        /*** ShouldThrow(Func<Task>, TimeSpan) ***/
        public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter) where TException : Exception
        {
            return ShouldThrow<TException>(actual, timeoutAfter, () => null);
        }
        public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter, string customMessage) where TException : Exception
        {
            return ShouldThrow<TException>(actual, timeoutAfter, () => customMessage);
        }
        public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Should.ThrowInternal<TException>(actual, timeoutAfter, customMessage);
        }

        /*** ShouldNotThrow(Task) ***/
        public static void ShouldNotThrow(this Task action)
        {
            ShouldNotThrow(action, () => null);
        }
        public static void ShouldNotThrow(this Task action, string customMessage)
        {
            ShouldNotThrow(action, () => customMessage);
        }
        public static void ShouldNotThrow(this Task action, [InstantHandle] Func<string> customMessage)
        {
            ShouldNotThrow(() => action, customMessage);
        }

        /*** ShouldNotThrow(Task<T>) ***/
        public static T ShouldNotThrow<T>(this Task<T> action)
        {
            return ShouldNotThrow(() => action);
        }
        public static T ShouldNotThrow<T>(this Task<T> action, string customMessage)
        {
            return ShouldNotThrow(() => action, customMessage);
        }
        public static T ShouldNotThrow<T>(this Task<T> action, [InstantHandle] Func<string> customMessage)
        {
            return ShouldNotThrow(() => action, customMessage);
        }

        /*** ShouldNotThrow(Func<Task>) ***/
        public static void ShouldNotThrow(this Func<Task> action)
        {
            ShouldNotThrow(action, () => null);
        }
        public static void ShouldNotThrow(this Func<Task> action, string customMessage)
        {
            ShouldNotThrow(action, () => customMessage);
        }
        public static void ShouldNotThrow(this Func<Task> action, [InstantHandle] Func<string> customMessage)
        {
            ShouldNotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** ShouldNotThrow(Task, TimeSpan) ***/
        public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter)
        {
            ShouldNotThrow(() => action, timeoutAfter);
        }
        public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, string customMessage)
        {
            ShouldNotThrow(() => action, timeoutAfter, customMessage);
        }
        public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            ShouldNotThrow(() => action, timeoutAfter, customMessage);
        }

        /*** ShouldNotThrow(Func<Task>, TimeSpan) ***/
        public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter)
        {
            ShouldNotThrow(action, timeoutAfter, () => null);
        }
        public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, string customMessage)
        {
            ShouldNotThrow(action, timeoutAfter, () => customMessage);
        }
        public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            Should.NotThrowInternal(action, timeoutAfter, customMessage);
        }

        /*** ShouldNotThrow(Func<Task<T>>) ***/
        public static T ShouldNotThrow<T>(this Func<Task<T>> action)
        {
            return ShouldNotThrow(action, () => null);
        }
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, string customMessage)
        {
            return ShouldNotThrow(action, () => customMessage);
        }
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, [InstantHandle] Func<string> customMessage)
        {
            return ShouldNotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
        }

        /*** ShouldNotThrow(Task<T>, TimeSpan) ***/
        public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter)
        {
            return ShouldNotThrow(() => action, timeoutAfter);
        }
        public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, string customMessage)
        {
            return ShouldNotThrow(() => action, timeoutAfter, customMessage);
        }
        public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            return ShouldNotThrow(() => action, timeoutAfter, customMessage);
        }

        /*** ShouldNotThrow(Func<Task<T>>, TimeSpan) ***/
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter)
        {
            return ShouldNotThrow(action, timeoutAfter, () => null);
        }
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, string customMessage)
        {
            return ShouldNotThrow(action, timeoutAfter, () => customMessage);
        }
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            return Should.NotThrowInternal(action, timeoutAfter, customMessage);
        }
    }
}
#endif