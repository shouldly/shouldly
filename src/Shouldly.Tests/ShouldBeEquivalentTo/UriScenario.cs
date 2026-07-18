namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class UriScenario
{
    [Fact]
    public void ShouldPassWhenUrisAreEqual()
    {
        var subject = new Uri("http://abc/");
        var expected = new Uri("http://abc/");
        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldPassWhenUrisDifferOnlyByTrailingSlashNormalisation()
    {
        var original = new Uri("http://abc");
        var roundTripped = new Uri(original.ToString());

        original.ShouldBeEquivalentTo(roundTripped);
        roundTripped.ShouldBeEquivalentTo(original);
    }

    [Fact]
    public void ShouldPassWhenUriIsPropertyOfContainingObject()
    {
        var original = new Uri("http://abc");
        var roundTripped = new Uri(original.ToString());

        var subject = new Holder { Endpoint = roundTripped };
        var expected = new Holder { Endpoint = original };

        subject.ShouldBeEquivalentTo(expected);
    }

    // Regression test for https://github.com/shouldly/shouldly/issues/1205
    // A Uri round-tripped through its string form (e.g. bound from config, serialised,
    // then re-parsed) gains a trailing slash via ToString() normalisation, so its
    // OriginalString differs even though the Uris are equal. Before the Uri special
    // case, equivalence walked every Uri property and tripped on OriginalString.
    [Fact]
    public void ShouldPassWhenUriIsRoundTrippedThroughString()
    {
        var original = new Uri("https://abc.def");
        var roundTripped = new Uri(original.ToString());

        original.OriginalString.ShouldNotBe(roundTripped.OriginalString);
        original.ShouldBeEquivalentTo(roundTripped);
    }

    [Fact]
    public void ShouldFailWhenUrisAreNotEqual()
    {
        var subject = new Uri("http://abc/");
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new Uri("http://def/"), "Some additional context"));
    }

    private sealed class Holder
    {
        public Uri Endpoint { get; set; } = null!;
    }
}
