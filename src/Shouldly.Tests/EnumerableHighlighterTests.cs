using NUnit.Framework;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.Tests
{
    [TestFixture]
    public class EnumerableHighlighterTests
    {
        private readonly EnumerableHighlighter _context = new EnumerableHighlighter(new DifferenceHighlighter());

        [Test]
        public void CanProcessTwoEnumerables()
        {
            _context
                .CanProcess(new[] {1, 2, 3}, new[] {4, 5, 6})
                .ShouldBe(true);
        }

        [Test]
        public void CannotProcessEnumerableActualAndIntExpected()
        {
            _context
                .CanProcess(new[] {1, 2, 3}, 4)
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessEnumerableActualAndStringExpected()
        {
            _context
                .CanProcess(new[] {1, 2, 3}, "four")
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessStringActualAndStringExpected()
        {
            _context
                .CanProcess("one", "two")
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessTwoInts()
        {
            _context
                .CanProcess(1, 2)
                .ShouldBe(false);
        }
    }
}