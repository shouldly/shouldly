using Xunit;
using Xunit.Extensions;

namespace Shouldly.Tests.InternalTests
{
    public class ShouldlyStringExtensionTests
   
    {

    [Fact]
        public void Clip_ShouldNotReduceTheSizeOfAStringSmallerThanTheMaximumLength()
        {
            "small".ShouldMatch("small".Clip(10));
        }

        [Fact]
        public void Clip_ShouldReduceTheSizeOfAStringLongerThanTheMaximumLength()
        {
            "largestrin".ShouldMatch("largestringtoclip".Clip(10));
        }

        [Fact]
        public void Clip_ShouldHandleEmptyStrings()
        {
            string.Empty.ShouldMatch(string.Empty.Clip(10));
        }

        [Fact]
        public void ClipWithEllipsis_ShouldNotReduceTheSizeOfAStringSmallerThanTheMaximumLength()
        {
            "small".ShouldMatch("small".Clip(10, "..."));
        }

        [Fact]
        public void ClipWithEllipsis_ShouldReduceTheSizeOfAStringLongerThanTheMaximumLength()
        {
            "largestrin...".ShouldMatch("largestringtoclip".Clip(10, "..."));
        }

        [Fact]
        public void ClipWithEllipsis_ShouldHandleEmptyStrings()
        {
            string.Empty.ShouldMatch(string.Empty.Clip(10, "..."));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("oneline", "oneline")]
        [InlineData("line1\nline2", "line1\nline2")]
        [InlineData("line1\r\nline2", "line1\nline2")]
        [InlineData("line1\rline2", "line1\nline2")]
        public void NormalizeLineEndingsTests(string input, string result)
        {
            input.NormalizeLineEndings().ShouldBe(result);
        }

        [Theory]
        [InlineData("() => result")]
        [InlineData("( ) => result")]
        [InlineData("( ) =>result")]
        [InlineData("( )=> result")]
        [InlineData("( )=>result")]
        [InlineData("()=>result")]
        [InlineData(@"()
                        =>result")]
        [InlineData(@"()
                        => result")]
        [InlineData(@"()
                        =>
                        result")]

        [InlineData(@" =>         
                        result")] // The () might be on a different line. Shouldly context will only give us what is on the assertion line.
        public void StripLambdaExpressionSyntax_ShouldRemoveLambda(string input)
        {
            var inputWithLambdaExpressionStripped = input.StripLambdaExpressionSyntax();
            inputWithLambdaExpressionStripped.ShouldBe("result");
        }
    }
}
