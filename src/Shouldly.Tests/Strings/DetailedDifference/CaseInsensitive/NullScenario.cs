namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive
{
    public static class NullScenario
    {
        [Fact]
        public static void ShouldNotShowDifferenceWhenActualIsMissing()
        {
            var str = (string?)null;
            Verify.ShouldFail(() =>
str.ShouldBe("null", StringCompareShould.IgnoreCase),

errorWithSource:
@"str
    should be with options: Ignoring case
""null""
    but was
null",

errorWithoutSource:
@"null
    should be with options: Ignoring case
""null""
    but was not");
        }

        [Fact]
        public static void ShouldNotShowDifferenceWhenExpectedIsMissing()
        {
            var str = "null";
            Verify.ShouldFail(() =>
str.ShouldBe(null, StringCompareShould.IgnoreCase),

errorWithSource:
@"str
    should be with options: Ignoring case
null
    but was
""null""",

errorWithoutSource:
@"""null""
    should be with options: Ignoring case
null
    but was not");
        }
    }
}
