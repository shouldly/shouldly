namespace Shouldly.Tests.Strings;

public class ShouldNotContainAll
{
    [Fact]
    public void ShouldPassWhenNotAllValuesPresent()
    {
        "my name is slim shady".ShouldNotContainAll(["name", "slim", "eminem"]);
    }

    [Fact]
    public void ShouldFailWhenAllValuesPresent()
    {
        var target = "my name is slim shady";
        Verify.ShouldFail(() =>
            target.ShouldNotContainAll(["name", "slim", "shady"]));
    }

    [Fact]
    public void CaseSensitiveShouldFailIfAllMatch()
    {
        var target = "Hello World";
        Verify.ShouldFail(() =>
            target.ShouldNotContainAll(["Hello"], Case.Sensitive));
    }

    [Fact]
    public void CaseSensitiveShouldPassIfCaseDoesNotMatch()
    {
        "Hello World".ShouldNotContainAll(["hello"], Case.Sensitive);
    }

    [Fact]
    public void CaseInsensitiveShouldFailIfAllPresentIgnoringCase()
    {
        var target = "Hello World";
        Verify.ShouldFail(() =>
            target.ShouldNotContainAll(["hello"], Case.Insensitive));
    }

    [Fact]
    public void CaseInsensitiveShouldPassIfNotAllPresentIgnoringCase()
    {
        "Hello World".ShouldNotContainAll(["hello", "mars"], Case.Insensitive);
    }

    [Fact]
    public void ShouldIncludeCustomMessageWhenProvided()
    {
        var target = "my name is slim shady";
        Verify.ShouldFail(() =>
            target.ShouldNotContainAll(["name", "slim", "shady"], Case.Insensitive, "Custom error message"));
    }
}
