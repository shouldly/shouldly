using Xunit;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class SuccessScenarios
    {
        [Fact]
        public void ArrayIsSubsetOfSelf()
        {
            var arr = new[] {1};

            arr.ShouldBeSubsetOf(arr, "Some additional context");
        }

        [Fact]
        public void EmptyArrayIsSubsetOfAnything()
        {
            new int[0].ShouldBeSubsetOf(new[] {1, 2, 3, 4}, "Some additional context");
        }
    }
}