using System;
using System.Text.RegularExpressions;

namespace Shouldly.Tests.Strings
{
    [ShouldlyMethods]
    public static class Verify
    {
        static readonly Regex MatchGetHashCode = new Regex("\\(\\d{5,8}\\)");

        public static void ShouldFail(Action action, string errorWithSource, string errorWithoutSource, Func<string, string> messageScrubber = null)
        {
            if (messageScrubber == null)
                messageScrubber = v => MatchGetHashCode.Replace(v, "(000000)");
            else
            {
                var scrubber = messageScrubber;
                messageScrubber = v => MatchGetHashCode.Replace(scrubber(v), "(000000)");
            }

            action
                .ShouldSatisfyAllConditions(
                    () =>
                    { //TODO: this is currently a hack this shoudld be compared with an error qualified with source
                        using (ShouldlyConfiguration.DisableSourceInErrors())
                        {
                            var msg = Should.Throw<ShouldAssertException>(action).Message;
                            var sourceDisabledExceptionMsg = messageScrubber(msg);
                            string expectedNearly = CheckMessage(sourceDisabledExceptionMsg, errorWithoutSource);
                            sourceDisabledExceptionMsg.ShouldBe(expectedNearly,"Source not available", StringCompareShould.IgnoreLineEndings);
                        }
                    });
        }
        //this method is there to tell that the message from the ShouldBe is nearly as same as the one expectd that is it has a copy statement at start
        public static string CheckMessage(string actualMessage, string expectedMessge)
        {
            if (actualMessage.Contains(@"To approve the changes run this command:
copy /Y"))
            {
                return actualMessage;
            }
            else
            {
                return expectedMessge;
            }
        }
    }
}