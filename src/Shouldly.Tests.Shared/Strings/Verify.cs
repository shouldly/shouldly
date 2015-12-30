using System;
using System.Text.RegularExpressions;

namespace Shouldly.Tests.Strings
{
    public static class Verify
    {
#if PORTABLE
        static readonly Regex MatchGetHashCode = new Regex("\\(\\d{5,8}\\)");
#else
        static readonly Regex MatchGetHashCode = new Regex("\\(\\d{5,8}\\)", RegexOptions.Compiled);
#endif

        public static void ShouldFail(Action action, string errorWithSource, string errorWithoutSource)
        {
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
                            var sourceDisabledExceptionMsg = MatchGetHashCode.Replace(Should.Throw<ShouldAssertException>(action).Message, "(000000)");
                            sourceDisabledExceptionMsg.ShouldBe(errorWithoutSource, "Source not available", StringCompareShould.IgnoreLineEndings);
                        }
                    },
                    () =>
                    {
                        var sourceEnabledExceptionMsg = MatchGetHashCode.Replace(Should.Throw<ShouldAssertException>(action).Message, "(000000)");
                        sourceEnabledExceptionMsg.ShouldBe(errorWithSource, "Source available", StringCompareShould.IgnoreLineEndings);
                    });
#endif
        }
    }
}