using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeEnumerableTests
    {
        public class ShouldAllBe
        {
            [Test]
            public void ShouldAllBe_WithPredicate_WhenTrue_ShouldNotThrow()
            {
                new[] {1, 2, 3}.ShouldAllBe(x => x < 4);
            }
        }

        public class ShouldBeEmpty
        {
            [Test]
            public void ShouldBeEmpty_WhenEmpty_ShouldNotError()
            {
                Should.NotError(() => new object[0].ShouldBeEmpty());
            }

            [Test]
            public void ShouldNotBeEmpty_WhenNotEmpty_ShouldNotError()
            {
                Should.NotError(() => new[] {new object()}.ShouldNotBeEmpty());
            }
        }

        public class ShouldBeSubsetOf
        {
            [Test]
            public void ShouldBeSubsetOf_EmptyEnumerableShouldBeSubsetOfAnyEnumerable()
            {
                new object[] {}.ShouldBeSubsetOf(new object[] {});
                new object[] {}.ShouldBeSubsetOf(new object[] {1, 2, 3, 4, 5});
                new string[] {}.ShouldBeSubsetOf(new[] {""});
            }

            [Test]
            public void ShouldBeSubsetOf_EnumerablesWithIntegers_WhenTrue_ShouldNotThrow()
            {
                new[] {1}.ShouldBeSubsetOf(new[] {1});
                new[] {1}.ShouldBeSubsetOf(new[] {1, 2, 3, 4, 5});
                new[] {1, 2, 3, 4, 5}.ShouldBeSubsetOf(new[] {1, 2, 3, 4, 5});
                new[] {1, 2, 3, 4, 5}.ShouldBeSubsetOf(new[] {1, 2, 3, 4, 5, 1, 2, 3, 4, 5});
            }

            [Test]
            public void ShouldBeSubsetOf_EnumerablesWithDoubles_WhenTrue_ShouldNotThrow()
            {
                new[] {1.0000001d}.ShouldBeSubsetOf(new[] {1.0000001d});
                new[] {3.000000000001d}.ShouldBeSubsetOf(new[] {1, 2, 3.000000000001d, 4, 5});
                new[] {100.0001d, 2123.93812d, 3.9712354123d, 412.12354d, 5.00000000012d}.ShouldBeSubsetOf(new[]
                {100.0001d, 2123.93812d, 3.9712354123d, 412.12354d, 5.00000000012d});
            }

            [Test]
            public void ShouldBeSubsetOf_EnumerablesWithFloates_WhenTrue_ShouldNotThrow()
            {
                new[] {1.0000001f}.ShouldBeSubsetOf(new[] {1.00000010001f});
                new[] {3.00000000000100001f}.ShouldBeSubsetOf(new[] {1, 2, 3.000000000001f, 4, 5});
                new[] {100.0001f, 2123.93812f, 3.9712354123f, 412.12354f, 5.00000000012f}.ShouldBeSubsetOf(new[]
                {100.0001f, 2123.93812f, 3.9712354123f, 412.12354f, 5.00000000022f});
            }

            [Test]
            public void ShouldBeSubsetOf_EnumerablesWithStrings_WhenTrue_ShouldNotThrow()
            {
                new[] {""}.ShouldBeSubsetOf(new[] {""});
                new[] {" "}.ShouldBeSubsetOf(new[] {" "});
                new object[] {}.ShouldBeSubsetOf(new object[] {"1", "2", "3", "4", "5"});
                new[] {"1"}.ShouldBeSubsetOf(new[] {"1"});
                new[] {"1"}.ShouldBeSubsetOf(new[] {"1", "2", "3", "4", "5"});
                new[] {"1", "2", "3", "4", "5"}.ShouldBeSubsetOf(new[] {"1", "2", "3", "4", "5"});
            }

            [Test]
            public void ShouldBeSubsetOf_EnumerablesWithIntegers_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new object[] {1}.ShouldBeSubsetOf(new object[] {}));
                Assert.Throws<ChuckedAWobbly>(() => new object[] {6}.ShouldBeSubsetOf(new object[] {1, 2, 3, 4, 5}));
                Assert.Throws<ChuckedAWobbly>(() => new[] {1}.ShouldBeSubsetOf(new int[] {}));
                Assert.Throws<ChuckedAWobbly>(() => new[] {1}.ShouldBeSubsetOf(new[] {2, 2, 2, 2, 2, 2, 22, 3, 4, 5}));
                Assert.Throws<ChuckedAWobbly>(() => new[] {1, 1, 2, 3, 4, 5}.ShouldBeSubsetOf(new[] {1, 2, 3, 4, 5}));
                Assert.Throws<ChuckedAWobbly>(
                    () => new[] {1, 2, 3, 4, 4, 5, 5, 3, 2, 2}.ShouldBeSubsetOf(new[] {1, 2, 3, 4, 5, 1, 2, 3, 4, 5}));
            }

            [Test]
            public void ShouldBeSubsetOf_EnumerablesWithDoubles_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new[] {1.0000001}.ShouldBeSubsetOf(new[] {1.00000010000001}));
                Assert.Throws<ChuckedAWobbly>(
                    () => new[] {3.00000000000100000001}.ShouldBeSubsetOf(new[] {1, 2, 3.00000000001, 4, 5}));
                Assert.Throws<ChuckedAWobbly>(
                    () =>
                        new[] {100.0001, 2123.93812, 3.9712354123, 412.12354, 5.00000000012}.ShouldBeSubsetOf(new[]
                        {100.0001, 2123.93812, 3.9712354123, 412.12354, 5.00000000022}));
            }

            [Test]
            public void ShouldBeSubsetOf_EnumerablesWithFloats_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new[] {1.0001001f}.ShouldBeSubsetOf(new[] {1.00000010000001f}));
                Assert.Throws<ChuckedAWobbly>(
                    () => new[] {3.00001000000100000001f}.ShouldBeSubsetOf(new[] {1, 2, 3.00000000001f, 4, 5}));
                Assert.Throws<ChuckedAWobbly>(
                    () =>
                        new[] {100.0001f, 2123.93812f, 3.9712354123f, 412.12354f, 5.00005000012f}.ShouldBeSubsetOf(new[]
                        {100.0001f, 2123.93812f, 3.9712354123f, 412.12354f, 5.00000000022f}));
            }

            [Test]
            public void ShouldBeSubsetOf_EnumerablesWithStrings_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new[] {"1"}.ShouldBeSubsetOf(new[] {"1 "}));
                Assert.Throws<ChuckedAWobbly>(() => new[] {" "}.ShouldBeSubsetOf(new[] {""}));
                Assert.Throws<ChuckedAWobbly>(() => new[] {"1"}.ShouldBeSubsetOf(new[] {" "}));
                Assert.Throws<ChuckedAWobbly>(
                    () => new object[] {"6"}.ShouldBeSubsetOf(new object[] {"1", "2", "3", "4", "5"}));
                Assert.Throws<ChuckedAWobbly>(() => new[] {"1", "1"}.ShouldBeSubsetOf(new[] {"1", "2", "3", "4", "5"}));
                Assert.Throws<ChuckedAWobbly>(
                    () => new[] {"1", "2", "3", "4", "5", "6"}.ShouldBeSubsetOf(new[] {"1", "2", "3", "4", "5"}));
            }

            [Test]
            public void ShouldBeSubsetOf_WhenFalse_CorrectMessageAppears()
            {
                var ex = Assert.Throws<ChuckedAWobbly>(() =>
                    new object[] {1}.ShouldBeSubsetOf(new object[] {}));
                ex.Message.ShouldContainWithoutWhitespace("new object[] { 1 } should be subset of [] but does not");
                ex = Assert.Throws<ChuckedAWobbly>(() =>
                    new object[] {6}.ShouldBeSubsetOf(new object[] {1, 2, 3, 4, 5}));
                ex.Message.ShouldContainWithoutWhitespace(
                    "new object[] { 6 } should be subset of [ 1, 2, 3, 4, 5 ] but does not");
                ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                    new[] {"1", "2", "3", "4", "5", "6"}.ShouldBeSubsetOf(new[] {"1", "2", "3", "4", "5"}));
                ex.Message.ShouldContainWithoutWhitespace(
                    "new[] { \"1\", \"2\", \"3\", \"4\", \"5\", \"6\" } should be subset of [ \"1\", \"2\", \"3\", \"4\", \"5\" ] but does not");
            }
        }
    }
}