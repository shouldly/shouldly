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
                messageScrubber = v =>
                {
                    
                    var msg = MatchGetHashCode.Replace(v, "(000000)");
                    return msg;
                };
            else
            {
                var scrubber = messageScrubber;
                messageScrubber = v => MatchGetHashCode.Replace(scrubber(v), "(000000)");
            }

            action
                .ShouldSatisfyAllConditions(
                    () =>
                    {
                        using (ShouldlyConfiguration.DisableSourceInErrors())
                        {
                            var sourceDisabledExceptionMsg = messageScrubber(Should.Throw<ShouldAssertException>(action).Message);
                            sourceDisabledExceptionMsg.ShouldBe(errorWithoutSource, "Source not available", StringCompareShould.IgnoreLineEndings);
                        }
                    },
                    () =>
                    {
                        var sourceEnabledExceptionMsg = messageScrubber(Should.Throw<ShouldAssertException>(action).Message);
                        sourceEnabledExceptionMsg.ShouldBe(errorWithSource, "Source available", StringCompareShould.IgnoreLineEndings);
                    });
        }
    }
}