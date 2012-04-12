using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldlyStringExtensionTests
    {
        [Test]
        public void ClipShouldNotReduceTheSizeOfAStringSmallerThanTheMaximumLength()
        {
            "small".ShouldMatch("small".Clip(10));
        }

        [Test]
        public void ClipShouldReduceTheSizeOfAStringLongerThanTheMaximumLength()
        {
            "largestrin".ShouldMatch("largestringtoclip".Clip(10));
        }

        [Test]
        public void ClipShouldHandleEmptyStrings()
        {
            string.Empty.ShouldMatch(string.Empty.Clip(10));
        }

    }
}
