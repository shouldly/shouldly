namespace Shouldly.Tests.Strings;

public class ShouldContainWithoutWhitespace
{
    [Fact]
    public void ShouldContainWithoutWhitespaceShouldFail()
    {
        var str = "Fun   with space   and \"quotes\"";
        Verify.ShouldFail(() =>
            str.ShouldContainWithoutWhitespace("Fun with space and missing quotes", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Fun   with space   and \"quotes\"".ShouldContainWithoutWhitespace("Fun with space and 'quotes'");
    }

    [Fact]
    public void ShouldExpectUppercaseNullTextWhenExpectedIsNull()
    {
        "NULL".ShouldContainWithoutWhitespace(null);
    }

    [Fact]
    public void ShouldExpectUppercaseNullTextWhenExpectedIsInstanceWithNullToString()
    {
        "NULL".ShouldContainWithoutWhitespace(new InstanceWithNullToString());
    }

    private sealed class InstanceWithNullToString
    {
        public override string? ToString() => null;
    }
}