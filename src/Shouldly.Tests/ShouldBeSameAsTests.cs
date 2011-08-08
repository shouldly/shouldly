using System.Collections.Generic;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeAndShouldNotBeSameAsTests
    {
        [Test]
        public void ShouldBeSameAs_WhenSameReference_ShouldNotThrow()
        {
            List<int> list = new List<int> { 1, 2, 3 };
            IList<int> sameReference = list;

            list.ShouldBeSameAs(sameReference);

            Should.Error(
                () => list.ShouldNotBeSameAs(sameReference),
                "() => list should not be same as [1, 2, 3] but was [1, 2, 3] difference [1, 2, 3]"
                );
        }

        [Test]
        public void ShouldBeSameAs_WhenDifferentReferences_ShouldThrow()
        {
            var first = new object();
            var second = new object();

            Should.Error(
                () => first.ShouldBeSameAs(second),
                "() => first should be same as System.Object but was System.Object"
            );

            first.ShouldNotBeSameAs(second);
        }

        [Test]
        public void ShouldBeSameAs_WhenEqualListsButDifferentReferences_ShouldThrow()
        {
            var list = new List<int> { 1, 2, 3 };
            var equalListWithDifferentRef = new List<int> { 1, 2, 3 };

            list.ShouldBe(equalListWithDifferentRef);

            Should.Error(
                () => list.ShouldBeSameAs(equalListWithDifferentRef),
                "() => list should be same as [1, 2, 3] but was [1, 2, 3] difference [1, 2, 3]"
            );

            list.ShouldNotBeSameAs(equalListWithDifferentRef);
        }

        [Test]
        public void ShouldBeSameAs_WhenComparingBoxedValueType_WillThrow()
        {
            var first = 1;

            Should.Error(
                () => first.ShouldBeSameAs(first),
                "() => first should be same as 1 but was 1"
            );

            first.ShouldNotBeSameAs(first);
        }
    }
}