using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldThrowExtensions
    {
        /*** ShouldThrow(Action) ***/
        public static TException ShouldThrow<TException>(this Action actual, string? customMessage = null)
            where TException : Exception
        {
            return Should.ThrowInternal<TException>(actual, customMessage);
        }

        /*** ShouldThrow(Func<T>) ***/
        public static TException ShouldThrow<TException>(this Func<object?> actual, string? customMessage = null)
            where TException : Exception
        {
            return Should.ThrowInternal<TException>(actual, customMessage);
        }

        /*** ShouldThrow(Action) ***/

        public static Exception ShouldThrow(this Action actual, Type exceptionType, string? customMessage = null)
        {
            return Should.ThrowInternal(actual, customMessage, exceptionType);
        }

        /*** ShouldThrow(Func<T>) ***/
        public static Exception ShouldThrow(this Func<object?> actual, Type exceptionType, string? customMessage = null)
        {
            return Should.ThrowInternal(actual, customMessage, exceptionType);
        }

        /*** ShouldNotThrow(Action) ***/
        public static void ShouldNotThrow(this Action action, string? customMessage = null)
        {
            Should.NotThrowInternal(action, customMessage);
        }

        /*** ShouldNotThrow(Func<T>) ***/
        public static T ShouldNotThrow<T>(this Func<T> action, string? customMessage = null)
        {
            return Should.NotThrowInternal(action, customMessage);
        }
    }
}