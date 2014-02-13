using System;
using System.Linq;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeEnumerableTests
    {
        public class ShouldContain
        {
            [Test]
            public void ShouldContain_EnumerablesWithIntegers_WhenTrue_ShouldNotThrow()
            {
                new[] {1}.ShouldContain(1);
                new[] { 1, 2, 3, 4, 5 }.ShouldContain(5);
                new[] { 13562377128312434, 2, 3, 4, 5 }.ShouldContain(13562377128312434);
                new[] {2, 3, 4, 5, 4, 123665, 11234, -1356237712831}.ShouldContain(-1356237712831);
            }

            [Test]
            public void ShouldContain_EnumerablesWithIntegers_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new int[] { }.ShouldContain(1));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 1, 2, 3, 4, 5, 5 }.ShouldContain(55));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 13562377128312434, 2, 3, 4, 5 }.ShouldContain(13562377128112434));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 2, 3, 4, 5, 4, 123665, 11234, -1356237712831 }.ShouldContain(-1356235712831));
            }

            [Test]
            public void ShouldNotContain_EnumerablesWithIntegers_WhenTrue_ShouldNotThrow()
            {
                new int[] {}.ShouldNotContain(1);
                new[] {1, 2, 3, 4, 5, 5}.ShouldNotContain(55);
                new[] {13562377128312434, 2, 3, 4, 5}.ShouldNotContain(13562377128112434);
                new[] {2, 3, 4, 5, 4, 123665, 11234, -1356237712831}.ShouldNotContain(-1356235712831);
            }

            [Test]
            public void ShouldNotContain_EnumerablesWithIntegers_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => 
                    new[] { 1 }.ShouldNotContain(1));
                Assert.Throws<ChuckedAWobbly>(() => 
                    new[] { 1, 2, 3, 4, 5 }.ShouldNotContain(5));
                Assert.Throws<ChuckedAWobbly>(() => 
                    new[] { 13562377128312434, 2, 3, 4, 5 }.ShouldNotContain(13562377128312434));
                Assert.Throws<ChuckedAWobbly>(() => 
                    new[] { 2, 3, 4, 5, 4, 123665, 11234, -1356237712831 }.ShouldNotContain(-1356237712831));
            }

            [Test]
            public void ShouldContain_EnumerablesWithDoubles_WithTolerance_WhenTrue_ShouldNotThrow()
            {
                new[] {2123.93812d}.ShouldContain(2124, 2);
                new[] {1, 2, 3d, 4, 5}.ShouldContain(6, 2d);
                new[] {100.0001d, 2123.93812d, 3.9712354123d, 412.12354d, 5.00000000012d}
                    .ShouldContain(5.00000012d,0.000001);
                new[] {100.0001d, 2123.93812d, 3.9712354123d, -412.12354d, 5.00000000012d}
                    .ShouldContain(-412.123d, 0.001);
                new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.01);
            }

            [Test]
            public void ShouldContain_EnumerablesWithDoubles_WithTolerace_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => 
                    new[] { 2123.93812d }.ShouldContain(2124, 0.01));
                Assert.Throws<ChuckedAWobbly>(() => 
                    new[] { 1, 2, 3d, 4, 5 }.ShouldContain(6, 0.5));
                Assert.Throws<ChuckedAWobbly>(() => 
                    new[] { 100.0001d, 2123.93812d, 3.9712354123d, 412.12354d, 5.00000000012d }
                    .ShouldContain(5.00000012d, 0.00000001));
                Assert.Throws<ChuckedAWobbly>(() => 
                    new[] { 100.0001d, 2123.93812d, 3.9712354123d, -412.12354d, 5.00000000012d }
                    .ShouldContain(-412.123d, 0.00001));
            }

            [Test]
            public void ShouldContain_EnumerablesWithStrings_WhenTrue_ShouldNotThrow()
            {
                new[] { "1" }.ShouldContain("1");
                new[] { "1", "2", "3", "4", "5" }.ShouldContain("5");
                new[] { "13562377128312434", "2", "3", "4", "5" }.ShouldContain("13562377128312434");
            }

            [Test]
            public void ShouldContain_EnumerablesWithStrings_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new string[] { }.ShouldContain("1"));
                Assert.Throws<ChuckedAWobbly>(() => new[] { "1", "2", "3", "4", "5" }.ShouldContain("55"));
                Assert.Throws<ChuckedAWobbly>(() => new[] { "13562377128312434", "2", "3", "4", "5" }.ShouldContain("13562377128112434"));
            }

            [Test]
            public void ShouldNotContain_EnumerablesWithStrings_WhenTrue_ShouldNotThrow()
            {
                new string[] { }.ShouldNotContain("1");
                new[] { "1", "2", "3", "4", "5" }.ShouldNotContain("55");
                new[] { "13562377128312434", "2", "3", "4", "5" }.ShouldNotContain("13562377128112434");
            }

            [Test]
            public void ShouldNotContain_EnumerablesWithStrings_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new[] { "1" }.ShouldNotContain("1"));
                Assert.Throws<ChuckedAWobbly>(() => new[] { "1", "2", "3", "4", "5" }.ShouldNotContain("5"));
                Assert.Throws<ChuckedAWobbly>(() => new[] { "13562377128312434", "2", "3", "4", "5" }.ShouldNotContain("13562377128312434"));
            }

            [Test]
            public void ShouldContain_EnumerablesWithIntegers_WithPredicate_WhenTrue_ShouldNotThrow()
            {
                new[] { new int() }.ShouldContain(new int());
                new[] {1, 2, 3}.ShouldContain(x => x.Equals(1));
                new[] {1, 2, 3, 4, 9999999}.ShouldContain(x => x >= 9999999);
                new[] {457, 237, 565, 981, 7551}.ShouldContain(x => x%2 != 0);
                new[] {457, 237, 565, 981, 7551}.ShouldContain(x => x < 10000 && x > 5000);
            }

            [Test]
            public void ShouldNotContain_EnumerablesWithIntegers_WithPredicate_WhenTrue_ShouldNotThrow()
            {
                new[] { new int() }.ShouldNotContain(1);
                new[] { 1, 2, 3 }.ShouldNotContain(x => x.Equals(4));
                new[] { 1, 2, 3, 4, 9999999 }.ShouldNotContain(x => x > 9999999);
                new[] { 457, 237, 565, 981, 7551 }.ShouldNotContain(x => x % 2 == 0);
                new[] { 457, 237, 565, 981, 7551 }.ShouldNotContain(x => x < 10000 && x > 8000);
            }

            [Test]
            public void ShouldContain_EnumerablesWithIntegers_WithPredicate_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new[] { new int() }.ShouldContain(1));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 1, 2, 3 }.ShouldContain(x => x.Equals(4)));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 1, 2, 3, 4, 9999999 }.ShouldContain(x => x > 9999999));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 457, 237, 565, 981, 7551 }.ShouldContain(x => x % 2 == 0));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 457, 237, 565, 981, 7551 }.ShouldContain(x => x < 10000 && x > 8000));
            }

            [Test]
            public void ShouldNotContain_EnumerablesWithIntegers_WithPredicate_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new[] { new int() }.ShouldNotContain(new int()));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 1, 2, 3 }.ShouldNotContain(x => x.Equals(1)));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 1, 2, 3, 4, 9999999 }.ShouldNotContain(x => x >= 9999999));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 457, 237, 565, 981, 7551 }.ShouldNotContain(x => x % 2 != 0));
                Assert.Throws<ChuckedAWobbly>(() => new[] { 457, 237, 565, 981, 7551 }.ShouldNotContain(x => x < 10000 && x > 5000));
            }

            [Test]
            public void ShouldContain_EnumerablesWithStrings_WithPredicate_WhenTrue_ShouldNotThrow()
            {
                new[] { "1", "2", "3" }.ShouldContain(x => x.Equals("1"));
                new[] {"1", "2", "3", "4", "9999999"}.ShouldContain(
                    x => x.Contains("9") && x.EndsWith("9") && x.Length == 7);
                new[] {"457", "237", "565", "981", "7551"}.ShouldContain(x => x.IndexOfAny(new[] {'9'}).Equals(0));
                new[] {"457", "237", "565", "981", "7551"}.ShouldContain(
                    x => x.ToList().All(y => Int32.Parse(y.ToString()) < 10));
            }

            [Test]
            public void ShouldNotContain_EnumerablesWithStrings_WithPredicate_WhenTrue_ShouldNotThrow()
            {
                new[] { "1", "2", "3" }.ShouldNotContain(x => x.Equals("5"));
                new[] { "1", "2", "3", "4", "9999999" }.ShouldNotContain(
                    x => x.Contains("9") && x.EndsWith("9") && x.Length == 6);
                new[] { "457", "237", "565", "981", "7551" }.ShouldNotContain(x => x.IndexOfAny(new[] { '9' }).Equals(5));
                new[] { "457", "237", "565", "981", "7551" }.ShouldNotContain(
                    x => x.ToList().All(y => Int32.Parse(y.ToString()) > 10));
            }

            [Test]
            public void ShouldContain_EnumerablesWithStrings_WithPredicate_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new[] {"1", "2", "3"}.ShouldContain(x => x.Equals("5")));
                Assert.Throws<ChuckedAWobbly>(
                    () =>
                        new[] {"1", "2", "3", "4", "9999999"}.ShouldContain(
                            x => x.Contains("9") && x.EndsWith("9") && x.Length == 6));
                Assert.Throws<ChuckedAWobbly>(
                    () =>
                        new[] {"457", "237", "565", "981", "7551"}.ShouldContain(
                            x => x.IndexOfAny(new[] {'9'}).Equals(5)));
                Assert.Throws<ChuckedAWobbly>(() => new[] { "457", "237", "565", "981", "7551" }.ShouldContain(
                    x => x.ToList().All(y => Int32.Parse(y.ToString()) > 10)));
            }

            [Test]
            public void ShouldNotContain_EnumerablesWithStrings_WithPredicate_WhenFalse_ShouldThrow()
            {
                Assert.Throws<ChuckedAWobbly>(() => new[] { "1", "2", "3" }.ShouldNotContain(x => x.Equals("1")));
                Assert.Throws<ChuckedAWobbly>(
                    () =>
                        new[] { "1", "2", "3", "4", "9999999" }.ShouldNotContain(
                            x => x.Contains("9") && x.EndsWith("9") && x.Length == 7));
                Assert.Throws<ChuckedAWobbly>(
                    () =>
                        new[] { "457", "237", "565", "981", "7551" }.ShouldNotContain(
                            x => x.IndexOfAny(new[] {'9'}).Equals(0)));
                Assert.Throws<ChuckedAWobbly>(
                    () =>
                        new[] { "457", "237", "565", "981", "7551" }.ShouldNotContain(
                            x => x.ToList().All(y => Int32.Parse(y.ToString()) < 10)));
            }

            [Test]
            public void ShouldContain_WithNumbersWhenTrue_ShouldAllowTolerance()
            {
                new[] {1.0, 2.1, Math.PI, 4.321, 5.4321}.ShouldNotContain(3.14);
                new[] {1.0f, 2.1f, (float) Math.PI, 4.321f, 5.4321f}.ShouldContain(3.14f, 0.01);
            }

            [Test]
            public void ShouldlyMessage_WhenComparingMatchingStringsOver100Characters_ShouldNotClipStringForComparison()
            {
                var longString = new string('a', 110) + "zzzz";

                Should.NotError(
                    () => longString.ShouldContain("zzzz"));
            }
        }

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