using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldThrowExtensions
    {
        /*** ShouldThrow(Action) ***/
        public static TException ShouldThrow<TException>(this Action actual) where TException : Exception
        {
            return Should.Throw<TException>(actual);
        }
        public static TException ShouldThrow<TException>(this Action actual, string customMessage) where TException : Exception
        {
            return Should.Throw<TException>(actual, customMessage);
        }
        public static TException ShouldThrow<TException>(this Action actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return Should.Throw<TException>(actual, customMessage);
        }

        /*** ShouldNotThrow(Action) ***/
        public static void ShouldNotThrow(this Action action)
        {
            Should.NotThrow(action);
        }
        public static void ShouldNotThrow(this Action action, string customMessage)
        {
            Should.NotThrow(action, customMessage);
        }
        public static void ShouldNotThrow(this Action action, [InstantHandle] Func<string> customMessage)
        {
            Should.NotThrow(action, customMessage);
        }

        /*** ShouldNotThrow(Func<T>) ***/
        public static T ShouldNotThrow<T>(this Func<T> action)
        {
            return Should.NotThrow(action);
        }
        public static T ShouldNotThrow<T>(this Func<T> action, string customMessage)
        {
            return Should.NotThrow(action, customMessage);
        }
        public static T ShouldNotThrow<T>(this Func<T> action, [InstantHandle] Func<string> customMessage)
        {
            return Should.NotThrow(action, customMessage);
        }
    }
}