using NUnit.Framework;
using Shouldly.DifferenceHighlighting;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.InternalTests
{
    [TestFixture]
    public class EnumerableHighlighterTests
    {
        private readonly EnumerableHighlighter _context = new EnumerableHighlighter(new DifferenceHighlighter());

        [Test]
        public void CanProcessTwoEnumerables()
        {
            _context
                .CanProcess(MockHelper.GetMockTestEnvironment( new[] { 1, 2, 3 }, new[] { 4, 5, 6 }).Object)
                .ShouldBe(true);
        }

        [Test]
        public void CannotProcessEnumerableActualAndIntExpected()
        {
            _context
                .CanProcess(MockHelper.GetMockTestEnvironment(new[] {1, 2, 3}, 4).Object)
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessEnumerableActualAndStringExpected()
        {
            _context
                .CanProcess(MockHelper.GetMockTestEnvironment(new[] {1, 2, 3}, "four").Object)
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessStringActualAndStringExpected()
        {
            _context
                .CanProcess(MockHelper.GetMockTestEnvironment("one", "two").Object)
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessTwoInts()
        {
            _context
                .CanProcess(MockHelper.GetMockTestEnvironment(1, 2).Object)
                .ShouldBe(false);
        }
    }
}