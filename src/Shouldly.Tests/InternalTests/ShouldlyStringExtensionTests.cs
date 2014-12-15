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
                        result")] // The () might be on a different line. Shouldly environment will only give us what is on the assertion line.
        public void StripLambdaExpressionSyntax_ShouldRemoveLambda(string input)
        {
            var inputWithLambdaExpressionStripped = input.StripLambdaExpressionSyntax();
            inputWithLambdaExpressionStripped.ShouldBe("result");
        }
    }
}
