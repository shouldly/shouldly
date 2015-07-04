using System;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class Should
    {
        /*** ShouldThrow(Action) ***/
        public static TException ShouldThrow<TException>(this Action actual) where TException : Exception
        {
            return Throw<TException>(actual);
        }
        public static TException ShouldThrow<TException>(this Action actual, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, customMessage);
        }
        public static TException ShouldThrow<TException>(this Action actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Throw<TException>(actual, customMessage);
        }

        /*** ShouldNotThrow(Action) ***/
        public static void ShouldNotThrow(this Action action)
        {
            NotThrow(action);
        }
        public static void ShouldNotThrow(this Action action, string customMessage)
        {
            NotThrow(action, customMessage);
        }
        public static void ShouldNotThrow(this Action action, [InstantHandle] Func<string> customMessage)
        {
            NotThrow(action, customMessage);
        }

        /*** ShouldNotThrow(Func<T>) ***/
        public static T ShouldNotThrow<T>(this Func<T> action)
        {
            return NotThrow(action);
        }
        public static T ShouldNotThrow<T>(this Func<T> action, string customMessage)
        {
            return NotThrow(action, customMessage);
        }
        public static T ShouldNotThrow<T>(this Func<T> action, [InstantHandle] Func<string> customMessage)
        {
            return NotThrow(action, customMessage);
        }
    }
}