using System;
using System.Text.RegularExpressions;

namespace Shouldly.Tests.Strings
{
    [ShouldlyMethods]
    public static class Verify
    {
#if PORTABLE
        static readonly Regex MatchGetHashCode = new Regex("\\(\\d{5,8}\\)");
#else
        static readonly Regex MatchGetHashCode = new Regex("\\(\\d{5,8}\\)", RegexOptions.Compiled);
#endif

        public static void ShouldFail(Action action, string errorWithSource, string errorWithoutSource, Func<string, string> messageScrubber = null)
        {
            if (messageScrubber == null)
                messageScrubber = v => MatchGetHashCode.Replace(v, "(000000)");
            else
            {
                var scrubber = messageScrubber;
                messageScrubber = v => MatchGetHashCode.Replace(scrubber(v), "(000000)");
            }
#if PORTABLE
            // Portable does not have source
            var portableExceptionMessage = MatchGetHashCode.Replace(Should.Throw<ShouldAssertException>(action).Message, "(000000)");
            portableExceptionMessage.ShouldBe(errorWithoutSource, "Source available", StringCompareShould.IgnoreLineEndings);
#else
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
#endif
        }
    }
}