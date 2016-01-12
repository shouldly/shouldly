using NUnit.Framework;
using Shouldly.DifferenceHighlighting;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.InternalTests
{
    [TestFixture]
    public class EnumerableHighlighterTests
    {
        readonly EnumerableDifferenceHighlighter _highlighter = new EnumerableDifferenceHighlighter();

        [Test]
        public void CanProcessTwoEnumerables()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext( new[] { 1, 2, 3 }, new[] { 4, 5, 6 }))
                .ShouldBe(true);
        }

        [Test]
        public void CannotProcessEnumerableActualAndIntExpected()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext(new[] {1, 2, 3}, 4))
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessEnumerableActualAndStringExpected()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext(new[] {1, 2, 3}, "four"))
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessStringActualAndStringExpected()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext("one", "two"))
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessTwoInts()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext(1, 2))
                .ShouldBe(false);
        }
    }
}