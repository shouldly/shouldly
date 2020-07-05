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
            return ShouldThrow<TException>(actual, () => null);
        }
        public static TException ShouldThrow<TException>(this Action actual, string? customMessage) where TException : Exception
        {
            return ShouldThrow<TException>(actual, () => customMessage);
        }
        public static TException ShouldThrow<TException>(this Action actual, [InstantHandle] Func<string?>? customMessage) where TException : Exception
        {
            return Should.ThrowInternal<TException>(actual, customMessage);
        }

        /*** ShouldThrow(Func<T>) ***/
        public static TException ShouldThrow<TException>(this Func<object?> actual) where TException : Exception
        {
            return ShouldThrow<TException>(actual, () => null);
        }
        public static TException ShouldThrow<TException>(this Func<object?> actual, string? customMessage) where TException : Exception
        {
            return ShouldThrow<TException>(actual, () => customMessage);
        }
        public static TException ShouldThrow<TException>(this Func<object?> actual, [InstantHandle] Func<string?>? customMessage) where TException : Exception
        {
            return Should.ThrowInternal<TException>(actual, customMessage);
        }

        /*** ShouldThrow(Action) ***/
        public static Exception ShouldThrow(this Action actual, Type exceptionType)
        {
            return ShouldThrow(actual, () => null, exceptionType);
        }
        public static Exception ShouldThrow(this Action actual, string? customMessage, Type exceptionType)
        {
            return ShouldThrow(actual, () => customMessage, exceptionType);
        }
        public static Exception ShouldThrow(this Action actual, [InstantHandle] Func<string?>? customMessage, Type exceptionType)
        {
            return Should.ThrowInternal(actual, customMessage, exceptionType);
        }

        /*** ShouldThrow(Func<T>) ***/
        public static Exception ShouldThrow(this Func<object?> actual, Type exceptionType)
        {
            return ShouldThrow(actual, () => null, exceptionType);
        }
        public static Exception ShouldThrow(this Func<object?> actual, string? customMessage, Type exceptionType)
        {
            return ShouldThrow(actual, () => customMessage, exceptionType);
        }
        public static Exception ShouldThrow(this Func<object?> actual, [InstantHandle] Func<string?>? customMessage, Type exceptionType)
        {
            return Should.ThrowInternal(actual, customMessage, exceptionType);
        }

        /*** ShouldNotThrow(Action) ***/
        public static void ShouldNotThrow(this Action action)
        {
            ShouldNotThrow(action, () => null);
        }
        public static void ShouldNotThrow(this Action action, string? customMessage)
        {
            ShouldNotThrow(action, () => customMessage);
        }
        public static void ShouldNotThrow(this Action action, [InstantHandle] Func<string?>? customMessage)
        {
            Should.NotThrowInternal(action, customMessage);
        }

        /*** ShouldNotThrow(Func<T>) ***/
        public static T ShouldNotThrow<T>(this Func<T> action)
        {
            return ShouldNotThrow(action, () => null);
        }
        public static T ShouldNotThrow<T>(this Func<T> action, string? customMessage)
        {
            return ShouldNotThrow(action, () => customMessage);
        }
        public static T ShouldNotThrow<T>(this Func<T> action, [InstantHandle] Func<string?>? customMessage)
        {
            return Should.NotThrowInternal(action, customMessage);
        }
    }
}