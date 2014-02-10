using System;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeEnumerableTests
    {
        [Test]
        public void ShouldContain_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldContain(2);
        }

        [Test]
        public void ShouldNotContain_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldNotContain(5);
        }

        [Test]
        public void ShouldContain_WithPredicate_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldContain(x => x % 3 == 0);
        }

        [Test]
        public void ShouldNotContain_WithPredicate_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldNotContain(x => x % 4 == 0);
        }

        [Test]
        public void ShouldAllBe_WithPredicate_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldAllBe(x => x < 4);
        }


        [Test]
        public void ShouldlyMessage_WhenComparingMatchingStringsOver100Characters_ShouldNotClipStringForComparison()
        {
            var longString = new string('a', 110) + "zzzz";

            Should.NotError(
              () => longString.ShouldContain("zzzz"));
        }

        [Test]
        public void ShouldContain_WithNumbersWhenTrue_ShouldAllowTolerance()
        {
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldNotContain(3.14);
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.01);
            new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(3.14f, 0.01);
        }

        [Test]
        public void ShouldBeEmpty_WhenEmpty_ShouldNotError()
        {
            Should.NotError(() => new object[0].ShouldBeEmpty());
        }

        [Test]
        public void ShouldNotBeEmpty_WhenNotEmpty_ShouldNotError()
        {
            Should.NotError(() => new[] { new object() }.ShouldNotBeEmpty());
        }

        [Test]
        public void ShouldBeSubsetOf_WhenTrue_ShouldNotThrow()
        {
            new object[] { }.ShouldBeSubsetOf(new object[] { });
            new object[] { }.ShouldBeSubsetOf(new object[] { 1, 2, 3, 4, 5 });

            new[] { 1 }.ShouldBeSubsetOf(new[] { 1 });
            new[] { 1 }.ShouldBeSubsetOf(new[] { 1, 2, 3, 4, 5 });
            new[] { 1, 2, 3, 4, 5 }.ShouldBeSubsetOf(new[] { 1, 2, 3, 4, 5 });
            new[] { 1, 2, 3, 4, 5 }.ShouldBeSubsetOf(new[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 });

            new[] { 1.0000001d }.ShouldBeSubsetOf(new[] { 1.0000001d });
            new[] { 3.000000000001d }.ShouldBeSubsetOf(new[] { 1, 2, 3.000000000001d, 4, 5 });
            new[] { 100.0001d, 2123.93812d, 3.9712354123d, 412.12354d, 5.00000000012d }.ShouldBeSubsetOf(new[] { 100.0001d, 2123.93812d, 3.9712354123d, 412.12354d, 5.00000000012d });

            new[] { 1.0000001f }.ShouldBeSubsetOf(new[] { 1.00000010001f });
            new[] { 3.00000000000100001f }.ShouldBeSubsetOf(new[] { 1, 2, 3.000000000001f, 4, 5 });
            new[] { 100.0001f, 2123.93812f, 3.9712354123f, 412.12354f, 5.00000000012f }.ShouldBeSubsetOf(new[] { 100.0001f, 2123.93812f, 3.9712354123f, 412.12354f, 5.00000000022f });

            new[] { "" }.ShouldBeSubsetOf(new[] { "" });
            new string[] {  }.ShouldBeSubsetOf(new[] { "" });
            new[] { " " }.ShouldBeSubsetOf(new[] { " " });
            new object[] { }.ShouldBeSubsetOf(new object[] { "1", "2", "3", "4", "5" });
            new[] { "1" }.ShouldBeSubsetOf(new[] { "1" });
            new[] { "1" }.ShouldBeSubsetOf(new[] { "1", "2", "3", "4", "5" });
            new[] { "1", "2", "3", "4", "5" }.ShouldBeSubsetOf(new[] { "1", "2", "3", "4", "5" });
        }

        [Test]
        public void ShouldBeSubsetOf_WhenFalse_ShouldThrow()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new object[] { 1 }.ShouldBeSubsetOf(new object[] {}));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new object[] { 6 }.ShouldBeSubsetOf(new object[] { 1, 2, 3, 4, 5 }));

            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 1 }.ShouldBeSubsetOf(new int[] { }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 1 }.ShouldBeSubsetOf(new[] { 2, 2, 2, 2, 2, 2, 22, 3, 4, 5 }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 1, 1, 2, 3, 4, 5 }.ShouldBeSubsetOf(new[] { 1, 2, 3, 4, 5 }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 1, 2, 3, 4, 4, 5, 5, 3, 2, 2 }.ShouldBeSubsetOf(new[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 }));

            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 1.0000001 }.ShouldBeSubsetOf(new[] { 1.00000010000001 }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 3.00000000000100000001 }.ShouldBeSubsetOf(new[] { 1, 2, 3.00000000001, 4, 5 }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 100.0001, 2123.93812, 3.9712354123, 412.12354, 5.00000000012 }.ShouldBeSubsetOf(new[] { 100.0001, 2123.93812, 3.9712354123, 412.12354, 5.00000000022 }));

            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 1.0001001f }.ShouldBeSubsetOf(new[] { 1.00000010000001f }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 3.00001000000100000001f }.ShouldBeSubsetOf(new[] { 1, 2, 3.00000000001f, 4, 5 }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { 100.0001f, 2123.93812f, 3.9712354123f, 412.12354f, 5.00005000012f }.ShouldBeSubsetOf(new[] { 100.0001f, 2123.93812f, 3.9712354123f, 412.12354f, 5.00000000022f }));

            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { "1" }.ShouldBeSubsetOf(new[] { "1 " }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { " " }.ShouldBeSubsetOf(new[] { "" }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { "1" }.ShouldBeSubsetOf(new[] { " " }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new object[] { "6" }.ShouldBeSubsetOf(new object[] { "1", "2", "3", "4", "5" }));
  Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { "1", "1" }.ShouldBeSubsetOf(new[] { "1", "2", "3", "4", "5" }));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[] { "1", "2", "3", "4", "5", "6" }.ShouldBeSubsetOf(new[] { "1", "2", "3", "4", "5" }));
        }

        [Test]
        public void ShouldBeSubsetOf_WhenFalse_CorrectMessageAppears()
        {
            var ex = Assert.Throws<ChuckedAWobbly>(() => 
                new object[] { 1 }.ShouldBeSubsetOf(new object[] { }));
            ex.Message.ShouldContainWithoutWhitespace("new object[] { 1 } should be subset of [] but does not");
            ex = Assert.Throws<ChuckedAWobbly>(() => 
                new object[] { 6 }.ShouldBeSubsetOf(new object[] { 1, 2, 3, 4, 5 }));
            ex.Message.ShouldContainWithoutWhitespace("new object[] { 6 } should be subset of [ 1, 2, 3, 4, 5 ] but does not");

            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new[] { 1 }.ShouldBeSubsetOf(new int[] { }));
            ex.Message.ShouldContainWithoutWhitespace("new[] { 1 } should be subset of [] but does not");
            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new[] { 1 }.ShouldBeSubsetOf(new[] { 2, 2, 2, 2, 2, 2, 22, 3, 4, 5 }));
            ex.Message.ShouldContainWithoutWhitespace("new[] { 1 } should be subset of [ 2, 2, 2, 2, 2, 2, 22, 3, 4, 5 ] but does not");
            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new[] { 1, 1, 2, 3, 4, 5 }.ShouldBeSubsetOf(new[] { 1, 2, 3, 4, 5 }));
            ex.Message.ShouldContainWithoutWhitespace("new[] { 1, 1, 2, 3, 4, 5 } should be subset of [ 1, 2, 3, 4, 5 ] but does not");
            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new[] { 1, 2, 3, 4, 4, 5, 5, 3, 2, 2 }.ShouldBeSubsetOf(new[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 }));

            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new[] { "1" }.ShouldBeSubsetOf(new[] { "1 " }));
            ex.Message.ShouldContainWithoutWhitespace("new[] { \"1\" } should be subset of [ \"1\"  ] but does not");
            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new[] { " " }.ShouldBeSubsetOf(new[] { "" }));
            ex.Message.ShouldContainWithoutWhitespace("new[] { \" \" } should be subset of [\"\"] but does not");
            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new[] { "1" }.ShouldBeSubsetOf(new[] { " " }));
            ex.Message.ShouldContainWithoutWhitespace("new[] { \"1\" } should be subset of [\" \"] but does not");
            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new object[] { "6" }.ShouldBeSubsetOf(new object[] { "1", "2", "3", "4", "5" }));
            ex.Message.ShouldContainWithoutWhitespace("new object[] { \"6\" } should be subset of [ \"1\", \"2\", \"3\", \"4\", \"5\" ] but does not");
            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new[] { "1", "1" }.ShouldBeSubsetOf(new[] { "1", "2", "3", "4", "5" }));
            ex.Message.ShouldContainWithoutWhitespace("new[] { \"1\", \"1\" } should be subset of [\"1\", \"2\", \"3\", \"4\", \"5\" ] but does not");
            ex = Shouldly.Should.Throw<ChuckedAWobbly>(() =>
                new[] { "1", "2", "3", "4", "5", "6" }.ShouldBeSubsetOf(new[] { "1", "2", "3", "4", "5" }));
            ex.Message.ShouldContainWithoutWhitespace("new[] { \"1\", \"2\", \"3\", \"4\", \"5\", \"6\" } should be subset of [ \"1\", \"2\", \"3\", \"4\", \"5\" ] but does not");
        }
    }
}