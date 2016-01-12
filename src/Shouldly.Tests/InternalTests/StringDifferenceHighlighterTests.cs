using NUnit.Framework;
using Shouldly.DifferenceHighlighting;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.InternalTests
{
    [TestFixture]
    public class StringDifferenceHighlighterTests
    {
        readonly StringDifferenceHighlighter _highlighter = new StringDifferenceHighlighter();

        [Test]
        public void CanProcessTwoNotNullStringsAndAShouldBeAssertionMethod()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext("string1", "string2") { ShouldMethod = "ShouldBe" })
                .ShouldBe(true);
        }

        [Test]
        public void CannotProcessTwoNotNullStringsAndANotShouldBeAssertionMethod()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext("string1", "string2") { ShouldMethod = "ShouldContain" })
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessNullAndString()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext(null, "string1"))
                .ShouldBe(false);
        }

        [Test]
        public void CannotProcessStringAndNull()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext("string1", null))
                .ShouldBe(false);
        }

    }
}