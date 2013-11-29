using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class Should
    {
        public static TException Throw<TException>(Action actual) where TException : Exception
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
                throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException), e.GetType()).ToString());
            }

            throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException)).ToString());
        }

        public static void NotThrow(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.GetType()).ToString());
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
                throw new ChuckedAWobbly(new ShouldlyMessage(ex.GetType()).ToString());
            }
        }
    }
}