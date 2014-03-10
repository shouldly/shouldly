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
   }
}
