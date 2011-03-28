using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
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
            throw new ChuckedAWobbly(new ShouldlyMessage(actual).ToString());
        }
    }
}