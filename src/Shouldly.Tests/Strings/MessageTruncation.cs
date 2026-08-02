namespace Shouldly.Tests.Strings;

public class MessageTruncation
{
    [Fact]
    public void DifferenceBeyondTheEchoBudgetIsStillShown()
    {
        var shared = new string('a', ShouldlyConfiguration.MaxStringLengthInMessages + 5000);
        var actual = shared + "XXXX";
        var expected = shared + "YYYY";

        var message = Should.Throw<ShouldAssertException>(() => actual.ShouldBe(expected)).Message;

        // The echoed values are clipped long before this point, so the only way these can
        // show up is if the difference was computed from the untruncated values.
        message.ShouldContain("difference");
        message.ShouldContain("XXXX");
        message.ShouldContain("YYYY");
    }

    [Fact]
    public void TruncatedValuesSayHowMuchWasCut()
    {
        var limit = ShouldlyConfiguration.MaxStringLengthInMessages;
        var actual = new string('a', limit + 1);

        var message = Should.Throw<ShouldAssertException>(() => actual.ShouldBe("b")).Message;

        message.ShouldContain($"(truncated to {limit} of {limit + 1} characters, " +
                              "see ShouldlyConfiguration.MaxStringLengthInMessages)");
    }

    [Fact]
    public void ValuesWithinTheBudgetAreNotAnnotated()
    {
        var actual = new string('a', ShouldlyConfiguration.MaxStringLengthInMessages);

        var message = Should.Throw<ShouldAssertException>(() => actual.ShouldBe("b")).Message;

        message.ShouldNotContain("truncated");
    }

    [Fact]
    public void RaisingTheLimitEchoesMore()
    {
        var actual = new string('a', 4000) + "X";

        using (MaxStringLengthScope.Of(5000))
        {
            var message = Should.Throw<ShouldAssertException>(() => actual.ShouldBe("b")).Message;

            message.ShouldNotContain("truncated");
            message.ShouldContain(actual);
        }
    }

    [Fact]
    public void LoweringTheLimitEchoesLess()
    {
        var actual = new string('a', 30);

        using (MaxStringLengthScope.Of(10))
        {
            var message = Should.Throw<ShouldAssertException>(() => actual.ShouldBe("b")).Message;

            message.ShouldContain("(truncated to 10 of 30 characters");
        }
    }

    [Fact]
    public void TheLimitMustBePositive()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ShouldlyConfiguration.MaxStringLengthInMessages = 0);
    }

    [Fact]
    public void LineDiffCapsTheNumberOfChangedLines()
    {
        var actual = string.Join("\n", Enumerable.Range(0, 100).Select(i => $"a{i}"));
        var expected = string.Join("\n", Enumerable.Range(0, 100).Select(i => $"b{i}"));

        var message = Should.Throw<ShouldAssertException>(() => actual.ShouldBe(expected)).Message;

        // 100 changed lines per side, 20 shown.
        message.ShouldContain("Expected: ... and 80 more line(s)");
        message.ShouldContain("Actual:   ... and 80 more line(s)");
    }

    [Fact]
    public void LargeMultiLineValuesStillProduceABoundedMessage()
    {
        // Well past the point where splitting both values into per-line arrays would be
        // wasteful: the highlighter must fall back to the O(1) character-mode diff rather
        // than materialising line arrays for the whole input.
        var actual = string.Join("\n", Enumerable.Range(0, 50_000).Select(i => $"line {i} a"));
        var expected = string.Join("\n", Enumerable.Range(0, 50_000).Select(i => $"line {i} b"));

        var message = Should.Throw<ShouldAssertException>(() => actual.ShouldBe(expected)).Message;

        // A difference is still reported, and the whole message stays small. The line-mode
        // "... more line(s)" marker must be absent: its presence would mean both values were
        // split into full per-line arrays instead of taking the O(1) character-mode path.
        message.ShouldContain("difference");
        message.ShouldNotContain("more line(s)");
        message.Length.ShouldBeLessThan(5000);
    }

    // MaxStringLengthInMessages is AsyncLocal-backed, so an override here can't leak
    // into tests running in parallel — it still needs restoring for later tests on this context.
    private static class MaxStringLengthScope
    {
        public static IDisposable Of(int value)
        {
            var previous = ShouldlyConfiguration.MaxStringLengthInMessages;
            ShouldlyConfiguration.MaxStringLengthInMessages = value;
            return new Restore(previous);
        }

        private sealed class Restore(int previous) : IDisposable
        {
            public void Dispose() => ShouldlyConfiguration.MaxStringLengthInMessages = previous;
        }
    }
}
