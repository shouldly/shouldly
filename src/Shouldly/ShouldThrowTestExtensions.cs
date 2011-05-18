using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class Should
    {
        public static string Throw<TException>(Action actual) where TException : Exception
        {
            try
            {
                actual();
            }
            catch (TException e)
            {
                return e.Message;
            }
            catch (Exception e)
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException), e.GetType()).ToString());
            }

            throw new ChuckedAWobbly(new ShouldlyMessage(typeof(TException)).ToString());
        }
    }
}