using Shouldly.DifferenceHighlighting2;
using Shouldly.Internals;
using Shouldly.Internals.Assertions;
using Xunit;

namespace Shouldly.Tests.InternalTests
{
    public class StringShouldBeAssertionTests
    {
        [Fact]
        public static void IsSatisfied_should_return_true_when_the_lambda_parameter_returns_true()
        {
            var sut = new StringShouldBeAssertion(
                "", "", (e, a) => true, null, null);
            sut.IsSatisfied().ShouldBeTrue();
        }

        [Fact]
        public static void IsSatisfied_should_return_false_when_the_lambda_parameter_returns_false()
        {
            var sut = new StringShouldBeAssertion(
                "", "", (e, a) => false, null, null);
            sut.IsSatisfied().ShouldBeFalse();
        }

        [Fact]
        public static void GenerateMessage_should_generate_the_correct_message_when_the_lambda_parameter_returns_false()
        {
            var sut = new StringShouldBeAssertion(
                "expected", "actual", (e, a) => false,
                new MockCodeTextGetter(),
                new MockDiffHighlighter());
            var expected = @"
    SomeCodeText
        should be
    ""expected""
        but was
    ""actual""
        difference
    expected and actual are different!
    Additional Info:
    custom message";

            sut.GenerateMessage("custom message").ShouldBe(expected);
        }

        class MockCodeTextGetter : ICodeTextGetter
        {
            public string GetCodeText() { return "SomeCodeText"; }
        }

        class MockDiffHighlighter : IStringDifferenceHighlighter
        {
            public string HighlightDifferences(string expected, string actual)
            {
                return string.Format("{0} and {1} are different!", expected, actual);
            }
        }
    }
}
