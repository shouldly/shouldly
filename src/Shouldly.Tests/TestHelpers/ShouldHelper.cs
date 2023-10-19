namespace Shouldly.Tests.TestHelpers;

public static class ShouldHelper
{
    public static void Error(Action action, string errorMessage)
    {
        var message = Should.Throw<ShouldAssertException>(action).Message;
        message.ShouldContainWithoutWhitespace(errorMessage);
        System.Diagnostics.Trace.WriteLine("Error message:");
        System.Diagnostics.Trace.WriteLine(message);
    }
}