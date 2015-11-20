using NUnit.Framework;
using Shouldly.DifferenceHighlighting2;
using Shouldly.Internals;
using Shouldly.Internals.Assertions;

namespace Shouldly.Tests.InternalTests
{
    [TestFixture]
    public class StringShouldBeAssertionTests
    {
        [Test]
        public static void IsSatisfied_should_return_true_when_the_lambda_parameter_returns_true()
        {
            var sut = new StringShouldBeAssertion(
                "", "", (e, a) => true, null, null);
            Assert.That(sut.IsSatisfied());
        }

        [Test]
        public static void IsSatisfied_should_return_false_when_the_lambda_parameter_returns_false()
        {
            var sut = new StringShouldBeAssertion(
                "", "", (e, a) => false, null, null);
            Assert.That(!sut.IsSatisfied());
        }

        [Test]
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

            var actual = sut.GenerateMessage("custom message");

            Assert.AreEqual(expected, actual);
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
