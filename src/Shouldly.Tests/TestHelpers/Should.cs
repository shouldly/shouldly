using System;

namespace Shouldly.Tests.TestHelpers
{
    public static class Should
    {
        public static void Error(Action action, string errorMessage)
        {
            var message = Shouldly.Should.Throw<ShouldAssertException>(action).Message;
            message.ShouldContainWithoutWhitespace(errorMessage);
            #if FullFramework
            System.Diagnostics.Trace.WriteLine("Error message:");
            System.Diagnostics.Trace.WriteLine(message);
            #endif
        }
    }
}