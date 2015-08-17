using System;
using System.Diagnostics;
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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(typeof(TException), e.GetType(), customMessage).ToString());
            }

            throw new ShouldAssertException(new ExpectedShouldlyMessage(typeof(TException), customMessage).ToString());
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
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyThrowMessage(ex.GetType(), ex.Message, customMessage).ToString());
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
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyThrowMessage(ex.GetType(), ex.Message, customMessage).ToString());
            }
        }
    }
}