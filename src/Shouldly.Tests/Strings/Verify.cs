using System;
using System.Text.RegularExpressions;

namespace Shouldly.Tests.Strings
{
    public static class Verify
    {
        static readonly Regex MatchGetHashCode = new Regex("\\(\\d{6,8}\\)", RegexOptions.Compiled);
        public static void ShouldFail(Action action, string errorWithSource, string errorWithoutSource)
        {
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
        }
    }
}