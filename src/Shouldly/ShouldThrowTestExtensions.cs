using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Shouldly
{
    [DebuggerStepThrough]
    public static class Should
    {
        public static string Throw<EXCEPTION>(Action actual) where EXCEPTION : Exception
        {
            try
            {
                actual();
            }
            catch (EXCEPTION e)
            {
                return e.Message;
            }
            throw new AssertionException(new ShouldlyMessage(actual).ToString());
        }
    }
}