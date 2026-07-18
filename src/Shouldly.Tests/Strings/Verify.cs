using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Shouldly.Tests.Strings;

[ShouldlyMethods]
public static class Verify
{
    private static readonly Regex MatchGetHashCode = new(@"\(-?\d{6,10}\)");

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldFail(Action action, Func<string, string>? messageScrubber = null)
    {
        Func<string, string> scrub = messageScrubber != null
            ? v => MatchGetHashCode.Replace(messageScrubber(v), "(000000)")
            : v => MatchGetHashCode.Replace(v, "(000000)");

        var message = scrub(Should.Throw<ShouldAssertException>(action).Message);

        message.ShouldMatchApproved(c => c.NoDiff());
    }

    public static void ShouldFail(Action action, string errorMessage, Func<string, string>? messageScrubber = null)
    {
        Func<string, string> scrub = messageScrubber != null
            ? v => MatchGetHashCode.Replace(messageScrubber(v), "(000000)")
            : v => MatchGetHashCode.Replace(v, "(000000)");

        var actual = scrub(Should.Throw<ShouldAssertException>(action).Message);
        actual.ShouldBe(errorMessage, StringCompareShould.IgnoreLineEndings);
    }
}
