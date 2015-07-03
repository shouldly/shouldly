#if net40
using System;
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

        /*** ShouldNotThrow(Func<Task>, TimeSpan) ***/
        public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter)
        {
            NotThrow(action, timeoutAfter);
        }
        public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, string customMessage)
        {
            NotThrow(action, timeoutAfter, customMessage);
        }
        public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            NotThrow(action, timeoutAfter, customMessage);
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
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(action, customMessage);
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

        /*** ShouldNotThrow(Func<Task<T>>, TimeSpan) ***/
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter)
        {
            return NotThrow(action, timeoutAfter);
        }
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, string customMessage)
        {
            return NotThrow(action, timeoutAfter, customMessage);
        }
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(action, timeoutAfter, customMessage);
        }
    }
}
#endif