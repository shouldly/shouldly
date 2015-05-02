using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class MultiDimentionalArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[,] { { "1", "2" }, { "3", "5" } }.ShouldBe(new[,] { { "1", "2" }, { "3", "4" } }, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[,] {{\"1\", \"2\"}, {\"3\", \"5\"}} " +
                       "should be [\"1\", \"2\", \"3\", \"4\"] " +
                       "but was [\"1\", \"2\", \"3\", \"5\"] " +
                       "difference [\"1\", \"2\", \"3\", *\"5\"*]" + @"
Additional Info:
Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[,] { { "1", "2" }, { "3", "4" } }.ShouldBe(new[,] { { "1", "2" }, { "3", "4" } });
        }
    }
}