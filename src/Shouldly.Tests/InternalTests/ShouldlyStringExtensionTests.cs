using NUnit.Framework;

namespace Shouldly.Tests.InternalTests
{
    [TestFixture]
    public class ShouldlyStringExtensionTests
    {
        [Test]
        public void Clip_ShouldNotReduceTheSizeOfAStringSmallerThanTheMaximumLength()
        {
            "small".ShouldMatch("small".Clip(10));
        }

        [Test]
        public void Clip_ShouldReduceTheSizeOfAStringLongerThanTheMaximumLength()
        {
            "largestrin".ShouldMatch("largestringtoclip".Clip(10));
        }

        [Test]
        public void Clip_ShouldHandleEmptyStrings()
        {
            string.Empty.ShouldMatch(string.Empty.Clip(10));
        }

        [Test]
        public void ClipWithEllipsis_ShouldNotReduceTheSizeOfAStringSmallerThanTheMaximumLength()
        {
            "small".ShouldMatch("small".Clip(10, "..."));
        }

        [Test]
        public void ClipWithEllipsis_ShouldReduceTheSizeOfAStringLongerThanTheMaximumLength()
        {
            "largestrin...".ShouldMatch("largestringtoclip".Clip(10, "..."));
        }

        [Test]
        public void ClipWithEllipsis_ShouldHandleEmptyStrings()
        {
            string.Empty.ShouldMatch(string.Empty.Clip(10, "..."));
        }

        [Test]
        [TestCase(null, Result = null)]
        [TestCase("oneline", Result = "oneline")]
        [TestCase("line1\nline2", Result = "line1\nline2")]
        [TestCase("line1\r\nline2", Result = "line1\nline2")]
        [TestCase("line1\rline2", Result = "line1\nline2")]
        public string NormalizeLineEndingsTests(string input)
        {
            return input.NormalizeLineEndings();
        }

        [Test]
        [TestCase("() => result")]
        [TestCase("( ) => result")]
        [TestCase("( ) =>result")]
        [TestCase("( )=> result")]
        [TestCase("( )=>result")]
        [TestCase("()=>result")]
        [TestCase(@"()
                        =>result")]
        [TestCase(@"()
                        => result")]
        [TestCase(@"()
                        =>
                        result")]

        [TestCase(@" =>         
                        result")] // The () might be on a different line. Shouldly context will only give us what is on the assertion line.
        public void StripLambdaExpressionSyntax_ShouldRemoveLambda(string input)
        {
            var inputWithLambdaExpressionStripped = input.StripLambdaExpressionSyntax();
            inputWithLambdaExpressionStripped.ShouldBe("result");
        }
    }
}
