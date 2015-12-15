using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class Should
    {
        /*** Should.Throw(Action) ***/
        public static TException Throw<TException>([InstantHandle] Action actual) where TException : Exception
        {
            return Throw<TException>(actual, () => null);
        }
        public static TException Throw<TException>([InstantHandle] Action actual, string customMessage) where TException : Exception
        {
            return Throw<TException>(actual, () => customMessage);
        }
        public static TException Throw<TException>([InstantHandle] Action actual, [InstantHandle] Func<string> customMessage) where TException : Exception
        {
            return ThrowInternal<TException>(actual, customMessage);
        }
        internal static TException ThrowInternal<TException>([InstantHandle] Action actual, [InstantHandle] Func<string> customMessage,
            [CallerMemberName] string shouldlyMethod = null) where TException : Exception
        {
            try
            {
                actual();
            }
            catch (TException e)
            {
                return e;
            }
            catch (Exception e)
            {
                throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), e.GetType(), customMessage, shouldlyMethod).ToString(), e);
            }

            throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), customMessage, shouldlyMethod).ToString());
        }

        /*** Should.NotThrow(Action) ***/
        public static void NotThrow([InstantHandle] Action action)
        {
            NotThrow(action, () => null);
        }
        public static void NotThrow([InstantHandle] Action action, string customMessage)
        {
            NotThrow(action, () => customMessage);
        }
        public static void NotThrow([InstantHandle] Action action, [InstantHandle] Func<string> customMessage)
        {
            NotThrowInternal(action, customMessage);
        }
        internal static void NotThrowInternal([InstantHandle] Action action, [InstantHandle] Func<string> customMessage,
            [CallerMemberName] string shouldlyMethod = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ShouldlyThrowMessage(ex.GetType(), ex.Message, customMessage, shouldlyMethod).ToString());
            }
        }

        /*** Should.NotThrow(Func<T>) ***/
        public static T NotThrow<T>([InstantHandle] Func<T> action)
        {
            return NotThrow(action, () => null);
        }
        public static T NotThrow<T>([InstantHandle] Func<T> action, string customMessage)
        {
            return NotThrow(action, () => customMessage);
        }
        public static T NotThrow<T>([InstantHandle] Func<T> action, [InstantHandle] Func<string> customMessage)
        {
            return NotThrowInternal(action, customMessage);
        }

        /// <summary>
        /// Used to differentiate between the extension methods and the static methods
        /// </summary>
        internal static T NotThrowInternal<T>([InstantHandle] Func<T> action, [InstantHandle] Func<string> customMessage,
            [CallerMemberName] string shouldlyMethod = null)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ShouldlyThrowMessage(ex.GetType(), ex.Message, customMessage, shouldlyMethod).ToString());
            }
        }
    }
}