using System.Text.RegularExpressions;

namespace Shouldly.Tests.Strings;

[ShouldlyMethods]
public static class Verify
{
    private static readonly Regex MatchGetHashCode = new(@"\(-?\d{6,10}\)");

    public static void ShouldFail(Action action, string errorWithSource, string errorWithoutSource, Func<string, string>? messageScrubber = null)
    {
        if (messageScrubber == null)
        {
            messageScrubber = v =>
            {
                var msg = MatchGetHashCode.Replace(v, "(000000)");
                return msg;
            };
        }
        else
        {
            var scrubber = messageScrubber;
            messageScrubber = v =>
            {
                var msg = scrubber(v);
                var res = MatchGetHashCode.Replace(msg, "(000000)");
                return res;
            };
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