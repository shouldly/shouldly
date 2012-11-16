using System.Collections.Generic;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeAndShouldNotBeSameAsTests
    {
        [Test]
        public void ShouldBeSameAs_WhenSameReference()
        {
            var list = new List<int> { 1, 2, 3 };
            IList<int> sameReference = list;

            list.ShouldBeSameAs(sameReference);
        }

        [Test]
        public void ShouldNotBeSameAs_WhenDifferentValue()
        {
            const int one = 1;
            const int two = 2;

            one.ShouldNotBeSameAs(two);
        }

        [Test]
        public void ShouldNotBeSameAs_WhenDifferentReference()
        {
            var zulu = new object();
            var tutsie = new object();

            zulu.ShouldNotBeSameAs(tutsie);
        }
    }
}