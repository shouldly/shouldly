using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeAndShouldNotBeSameAsTests
    {
        [Test]
        public void ShouldBeSameAs_WhenSameReference_ShouldNotChuckAWobbly()
        {
            List<int> list = new List<int>();
            IList<int> sameReference = list;

            list.ShouldBeSameAs(sameReference);
            ShouldChuck(() => list.ShouldNotBeSameAs(sameReference));
        }

        [Test]
        public void ShouldBeSameAs_WhenDifferentReferences_ShouldChuckAWobbly()
        {
            var first = new object();
            var second = new object();

            ShouldChuck(() => first.ShouldBeSameAs(second));
            first.ShouldNotBeSameAs(second);
        }

        [Test]
        public void ShouldBeSameAs_WhenEqualListsButDifferentReferences_ShouldChuckAWobbly()
        {
            var list = new List<int>();
            var equalListWithDifferentRef = new List<int>();

            list.ShouldBe(equalListWithDifferentRef);
            ShouldChuck(() => list.ShouldBeSameAs(equalListWithDifferentRef));
            list.ShouldNotBeSameAs(equalListWithDifferentRef);
        }

        [Test]
        public void ShouldBeSameAs_WhenComparingBoxedValueType_WillChuckAWobbly()
        {
            var first = 1;
            ShouldChuck(() => first.ShouldBeSameAs(first));
            first.ShouldNotBeSameAs(first);
        }

        private static void ShouldChuck(Action action)
        {
            Shouldly.Should.Throw<Shouldly.ChuckedAWobbly>(action);
        }
    }
}