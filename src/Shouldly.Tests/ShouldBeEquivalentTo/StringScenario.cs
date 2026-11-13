namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class StringScenario
{
    [Fact]
    public void ShouldFailWhenStringIsDifferent()
    {
        const string subject = "Hello";
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo("Goodbye", "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenCaseIsDifferent()
    {
        const string subject = "Hello";
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo("HELLO", "Some additional context"));
    }

    // A string can be enumerated, but it is a value, not a collection of characters.
    [Fact]
    public void ShouldFailWhenAStringIsComparedWithACollection()
    {
        Should.Throw<ShouldAssertException>(() => string.Empty.ShouldBeEquivalentTo(new Dictionary<string, int>()));
        Should.Throw<ShouldAssertException>(() => string.Empty.ShouldBeEquivalentTo(new HashSet<char>()));
        Should.Throw<ShouldAssertException>(() => string.Empty.ShouldBeEquivalentTo(Array.Empty<char>()));
        Should.Throw<ShouldAssertException>(() => "ab".ShouldBeEquivalentTo(new[] { 'a', 'b' }));
        Should.Throw<ShouldAssertException>(() => new[] { 'a', 'b' }.ShouldBeEquivalentTo("ab"));
        Should.Throw<ShouldAssertException>(() =>
            new { Value = (object)string.Empty }.ShouldBeEquivalentTo(new { Value = (object)new Dictionary<string, int>() }));
    }

    [Fact]
    public void ShouldFailWhenAStringIsComparedWithADictionary()
    {
        var subject = string.Empty;
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new Dictionary<string, int>(), "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenCaseMatches()
    {
        const string subject = "Hello";
        subject.ShouldBeEquivalentTo("Hello");
    }
}