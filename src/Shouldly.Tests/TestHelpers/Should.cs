using System;
using System.Diagnostics;

namespace Shouldly.Tests.TestHelpers
{
    [ShouldlyMethods]
    public static class Should
    {
        public static void Error(Action action, string errorMessage)
        {
            var message = Shouldly.Should.Throw<ShouldAssertException>(action).Message;
            message.ShouldContainWithoutWhitespace(errorMessage);
            Trace.WriteLine("Error message:");
            Trace.WriteLine(message);
        }

        public static void NotError(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException(new ExpectedShouldlyMessage(ex.GetType(), null).ToString());
            }
        }
    }
}