using NUnit.Framework;
using Shouldly.DifferenceHighlighting2;
using System;

namespace Shouldly.Tests.InternalTests
{
    [TestFixture]
    public static class StringDiffHighlighterTests
    {
        private static IStringDifferenceHighlighter _sut;
        [SetUp]
        public static void Setup()
        {
            _sut = new StringDifferenceHighlighter(null, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public static void Should_throw_exception_when_expected_arg_is_null()
        {
            _sut.HighlightDifferences(null, "not null");
        }
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public static void Should_throw_exception_when_actual_arg_is_null()
        {
            _sut.HighlightDifferences("not null", null);
        }
    }
}
