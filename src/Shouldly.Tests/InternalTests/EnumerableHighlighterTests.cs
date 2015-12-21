using Shouldly.DifferenceHighlighting;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.InternalTests
{
    public class EnumerableHighlighterTests
    {
        readonly EnumerableDifferenceHighlighter _highlighter = new EnumerableDifferenceHighlighter();

        [Fact]
        public void CanProcessTwoEnumerables()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext( new[] { 1, 2, 3 }, new[] { 4, 5, 6 }))
                .ShouldBe(true);
        }

        [Fact]
        public void CannotProcessEnumerableActualAndIntExpected()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext(new[] {1, 2, 3}, 4))
                .ShouldBe(false);
        }

        [Fact]
        public void CannotProcessEnumerableActualAndStringExpected()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext(new[] {1, 2, 3}, "four"))
                .ShouldBe(false);
        }

        [Fact]
        public void CannotProcessStringActualAndStringExpected()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext("one", "two"))
                .ShouldBe(false);
        }

        [Fact]
        public void CannotProcessTwoInts()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext(1, 2))
                .ShouldBe(false);
        }
    }
}