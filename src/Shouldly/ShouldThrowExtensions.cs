using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class Should
    {
        public static TException Throw<TException>([InstantHandle]Action actual) where TException : Exception
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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(typeof(TException), e.GetType()).ToString());
            }

            throw new ShouldAssertException(new ExpectedShouldlyMessage(typeof(TException)).ToString());
        }

        public static void NotThrow(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.GetType()).ToString());
            }
        }

        public static T NotThrow<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.GetType()).ToString());
            }
        }
    }
}