using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class MultiDimensionalArrayScenario
    {
        [Fact]
        public void MultiDimensionalArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    new[,] { { "1", "2" }, { "3", "5" } }.ShouldBe(new[,] { { "1", "2" }, { "3", "4" } }, "Some additional context"),

    // TODO Multidimensional arrays are not outputted correctly?
errorWithSource:
@"new[,] { { ""1"", ""2"" }, { ""3"", ""5"" } }
    should be
[""1"", ""2"", ""3"", ""4""]
    but was
[""1"", ""2"", ""3"", ""5""]
    difference
[""1"", ""2"", ""3"", *""5""*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[""1"", ""2"", ""3"", ""5""]
    should be
[""1"", ""2"", ""3"", ""4""]
    but was not
    difference
[""1"", ""2"", ""3"", *""5""*]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[,] { { "1", "2" }, { "3", "4" } }.ShouldBe(new[,] { { "1", "2" }, { "3", "4" } });
        }
    }
}