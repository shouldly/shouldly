// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using System.Drawing;
using NUnit.UiException.Controls;

namespace NUnit.UiException.Tests
{
    [TestFixture]
    public class TestPaintLineLocation
    {
        private PaintLineLocation _line;

        [SetUp]
        public void SetUp()
        {
            _line = new PaintLineLocation(42, "hello world!", new PointF(13, 42));
        }

        [Test]
        public void Test_PaintLineLocation()
        {
            Assert.That(_line.LineIndex, Is.EqualTo(42));
            Assert.That(_line.Text, Is.EqualTo("hello world!"));
            Assert.That(_line.Location, Is.EqualTo(new PointF(13, 42)));

            return;
        }       

        [Test]
        [ExpectedException(typeof(ArgumentNullException),
            ExpectedMessage = "text",
            MatchType = MessageMatch.Contains)]
        public void Test_SetText_Throws_NullTextException()
        {
            new PaintLineLocation(0, null, new PointF(0, 0)); // throws exception
        }

        [Test]
        public void Test_Equals()
        {
            Assert.That(_line.Equals(null), Is.False);
            Assert.That(_line.Equals("hello"), Is.False);
            Assert.That(_line.Equals(new PaintLineLocation(0, "", new PointF(0, 0))), Is.False);

            Assert.That(
                _line.Equals(new PaintLineLocation(_line.LineIndex, _line.Text, new PointF(0, 0))),
                Is.False);
            Assert.That(
                _line.Equals(new PaintLineLocation(_line.LineIndex, "", _line.Location)),
                Is.False);
            Assert.That(
                _line.Equals(new PaintLineLocation(0, _line.Text, _line.Location)),
                Is.False);

            Assert.That(_line.Equals(_line), Is.True);
            Assert.That(_line.Equals(
                new PaintLineLocation(_line.LineIndex, _line.Text, _line.Location)),
                Is.True);

            return;
        }
    }
}
