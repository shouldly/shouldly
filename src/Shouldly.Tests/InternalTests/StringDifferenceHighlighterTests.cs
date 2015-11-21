using Shouldly.DifferenceHighlighting;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.InternalTests
{
    public class StringDifferenceHighlighterTests
    {
        readonly StringDifferenceHighlighter _highlighter = new StringDifferenceHighlighter();

        [Fact]
        public void CanProcessTwoNotNullStringsAndAShouldBeAssertionMethod()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext("string1", "string2") { ShouldMethod = "ShouldBe" })
                .ShouldBe(true);
        }

        [Fact]
        public void CannotProcessTwoNotNullStringsAndANotShouldBeAssertionMethod()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext("string1", "string2") { ShouldMethod = "ShouldContain" })
                .ShouldBe(false);
        }

        [Fact]
        public void CannotProcessNullAndString()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext(null, "string1"))
                .ShouldBe(false);
        }

        [Fact]
        public void CannotProcessStringAndNull()
        {
            _highlighter
                .CanProcess(new TestShouldlyAssertionContext("string1", null))
                .ShouldBe(false);
        }

    }
}