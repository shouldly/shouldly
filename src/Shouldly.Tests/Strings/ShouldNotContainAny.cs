namespace Shouldly.Tests.Strings;

public class ShouldNotContainAny
{
    [Fact]
    public void ShouldPassWhenNoValuesPresent()
    {
        "my name is slim shady".ShouldNotContainAny(["marshall", "mathers"]);
    }

    [Fact]
    public void ShouldFailWhenAnyValuePresent()
    {
        var target = "my name is slim shady";
        Verify.ShouldFail(() =>
            target.ShouldNotContainAny(["name", "foo"]));
    }

    [Fact]
    public void CaseSensitiveShouldFailIfCaseMatches()
    {
        var target = "Hello World";
        Verify.ShouldFail(() =>
            target.ShouldNotContainAny(["Hello"], Case.Sensitive));
    }

    [Fact]
    public void CaseInsensitiveShouldPassIfValuesAbsentIgnoringCase()
    {
        "Hello World".ShouldNotContainAny(["mars"], Case.Insensitive);
    }

    [Fact]
    public void ShouldIncludeCustomMessageWhenProvided()
    {
        var target = "my name is slim shady";
        Verify.ShouldFail(() =>
            target.ShouldNotContainAny(["name", "slim"], Case.Insensitive, "Custom error message"));
    }
}
