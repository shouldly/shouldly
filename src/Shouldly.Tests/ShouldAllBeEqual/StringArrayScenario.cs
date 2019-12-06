using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldAllBeEqual
{
    public class StringArrayScenario
    {
        [Fact]
        public void StringArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    new[] { "string2", "string1", "string42", "string2" }.ShouldAllBeEqual("Some additional context"),

errorWithSource:
@"new[] { ""string2"", ""string1"", ""string42"", ""string2"" }
        should have all items equal but had:
2 occurrences of [""string2""]
1 occurrence of [""string1""]
1 occurrence of [""string42""]

Additional Info:
    Some additional context",

    errorWithoutSource:
@"[""string2"", ""string1"", ""string42"", ""string2""]
        should have all items equal but had:
2 occurrences of [""string2""]
1 occurrence of [""string1""]
1 occurrence of [""string42""]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new string[] { "string2", "string2", "string2", "string2" }.ShouldAllBeEqual();
        }
    }
}