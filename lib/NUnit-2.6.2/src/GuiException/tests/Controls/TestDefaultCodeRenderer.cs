// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.UiException.Controls;
using NUnit.UiException.CodeFormatters;
using System.Drawing;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture]
    public class TestDefaultCodeRenderer
    {
        private ICodeRenderer _renderer;
        private FormattedCode _empty;
        private FormattedCode _loremIpsum;
        private FormattedCode _text3x7;
        private CodeRenderingContext _args;

        [SetUp]
        public void SetUp()
        {
            _renderer = new DefaultCodeRenderer();

            ICodeFormatter formatter = new PlainTextCodeFormatter();

            _empty = formatter.Format("");

            _text3x7 = formatter.Format(
                    "111\r\n" +
                    "222\r\n" +
                    "333\r\n" +
                    "444\r\n" +
                    "555\r\n" +
                    "666\r\n" +
                    "777\r\n");

            _loremIpsum = formatter.Format(
                "Lorem ipsum dolor sit\r\n" +
                "amet, consectetur adipiscing elit.\r\n" +
                "Maecenas a nisi. In imperdiet, orci in\r\n" +
                "porta facilisis,\r\n" +
                "odio purus iaculis est, non varius urna turpis in mi.\r\n" + // longest line
                "Nullam dictum. Ut iaculis dignissim nulla.\r\n" +
                "Nullam condimentum porttitor leo.\r\n" +
                "Integer a odio et\r\n" +
                "velit suscipit pulvinar.");

            Image img = new Bitmap(100, 100);
            _args = new CodeRenderingContext();
            _args.Graphics = Graphics.FromImage(img);
            _args.Font = new Font("Courier New", 12);

            return;
        }

        [Test]
        public void DrawToGraphics_Can_Raise_ArgumentNullException()
        {
            try {
                _renderer.DrawToGraphics(null, _args, new Rectangle()); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.True(e.Message.Contains("code"));
            }

            try {
                _renderer.DrawToGraphics(_loremIpsum, null, new Rectangle()); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.True(e.Message.Contains("g"));
            }            

            return;
        }

        [Test]
        public void GetDocumentSize_Can_Raise_ArgumentNullException()
        {
            try {
                _renderer.GetDocumentSize(null, _args.Graphics, _args.Font); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.True(e.Message.Contains("code"));
            }

            try {
                _renderer.GetDocumentSize(_loremIpsum, null, _args.Font); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.True(e.Message.Contains("g"));
            }

            try {
                _renderer.GetDocumentSize(_loremIpsum, _args.Graphics, null); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.True(e.Message.Contains("font"));
            }
           
            return;
        }

        [Test]
        public void LineIndexToYCoordinate_Can_Raise_ArgumentNullException()
        {
            try {
                _renderer.LineIndexToYCoordinate(0, null, _args.Font); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.True(e.Message.Contains("g"));
            }

            try {
                _renderer.LineIndexToYCoordinate(0, _args.Graphics, null); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.True(e.Message.Contains("font"));
            }
        }
       
        [Test]
        public void LineIndexToYCoordinate()
        {
            SizeF sz = _args.Graphics.MeasureString("m", _args.Font);

            Assert.That(_renderer.LineIndexToYCoordinate(0, _args.Graphics, _args.Font),
                Is.EqualTo(0));

            Assert.That(_renderer.LineIndexToYCoordinate(1, _args.Graphics, _args.Font),
                Is.EqualTo(sz.Height));

            Assert.That(_renderer.LineIndexToYCoordinate(2, _args.Graphics, _args.Font),
                Is.EqualTo(sz.Height * 2));

            Assert.That(_renderer.LineIndexToYCoordinate(3, _args.Graphics, _args.Font),
                Is.EqualTo(sz.Height * 3));

            return;
        }

        [Test]
        public void GetDocumentSize()
        {           
            SizeF expSize;
            SizeF docSize;

            // measures text3x7's size

            expSize = _args.Graphics.MeasureString("111", _args.Font);
            docSize = _renderer.GetDocumentSize(_text3x7, _args.Graphics, _args.Font);

            Assert.That(docSize.Width, Is.EqualTo(expSize.Width));
            Assert.That(docSize.Height, Is.EqualTo(7 * expSize.Height));
            
            // measures loremIpsum's size

            expSize = _args.Graphics.MeasureString(
                "odio purus iaculis est, non varius urna turpis in mi.", _args.Font);
            docSize = _renderer.GetDocumentSize(_loremIpsum, _args.Graphics, _args.Font);

            Assert.That(docSize.Width, Is.EqualTo(expSize.Width));
            Assert.That(docSize.Height, Is.EqualTo(9 * expSize.Height));

            return;
        }
       
        [Test]
        public void ViewportLines()
        {
            DefaultCodeRenderer renderer = new DefaultCodeRenderer();
            PaintLineLocation[] lines;
            RectangleF viewport = new RectangleF(0, 0, 2, 4);
            
            Assert.That(renderer.ViewportLines(_empty, viewport, 1), Is.Not.Null);
            Assert.That(renderer.ViewportLines(_empty, viewport, 1).Length, Is.EqualTo(0));
            
            // Using the given text and viewport
            //
            // document   viewport
            //    1 1 1   * *
            //    2 2 2   * *
            //    3 3 3   * *                                
            //    4 4 4   * *
            //    5 5 5
            //    6 6 6
            //    7 7 7
            //
            // Tests below check what area of the document will be seen
            // through the viewport window. For simplicity issue, we assume
            // the code is using a Font of docSize 1x1 in pixel.

            lines = renderer.ViewportLines(_text3x7, viewport, 1);
            Assert.That(lines.Length, Is.EqualTo(5));
            Assert.That(lines[0], Is.EqualTo(new PaintLineLocation(0, "111", new PointF(0, 0))));
            Assert.That(lines[1], Is.EqualTo(new PaintLineLocation(1, "222", new PointF(0, 1))));
            Assert.That(lines[2], Is.EqualTo(new PaintLineLocation(2, "333", new PointF(0, 2))));
            Assert.That(lines[3], Is.EqualTo(new PaintLineLocation(3, "444", new PointF(0, 3))));
            Assert.That(lines[4], Is.EqualTo(new PaintLineLocation(4, "555", new PointF(0, 4))));

            viewport = new RectangleF(2, 0, 2, 4);
            lines = renderer.ViewportLines(_text3x7, viewport, 1);
            Assert.That(lines.Length, Is.EqualTo(5));
            Assert.That(lines[0], Is.EqualTo(new PaintLineLocation(0, "111", new PointF(-2, 0))));
            Assert.That(lines[1], Is.EqualTo(new PaintLineLocation(1, "222", new PointF(-2, 1))));
            Assert.That(lines[2], Is.EqualTo(new PaintLineLocation(2, "333", new PointF(-2, 2))));
            Assert.That(lines[3], Is.EqualTo(new PaintLineLocation(3, "444", new PointF(-2, 3))));
            Assert.That(lines[4], Is.EqualTo(new PaintLineLocation(4, "555", new PointF(-2, 4))));

            viewport = new RectangleF(0, -3, 2, 4);
            lines = renderer.ViewportLines(_text3x7, viewport, 1);
            Assert.That(lines.Length, Is.EqualTo(2));
            Assert.That(lines[0], Is.EqualTo(new PaintLineLocation(0, "111", new PointF(0, 3))));
            Assert.That(lines[1], Is.EqualTo(new PaintLineLocation(1, "222", new PointF(0, 4))));

            viewport = new RectangleF(1, 5, 2, 4);
            lines = renderer.ViewportLines(_text3x7, viewport, 1);
            Assert.That(lines.Length, Is.EqualTo(2));
            Assert.That(lines[0], Is.EqualTo(new PaintLineLocation(5, "666", new PointF(-1, 0))));
            Assert.That(lines[1], Is.EqualTo(new PaintLineLocation(6, "777", new PointF(-1, 1))));

            // using float values for location

            viewport = new RectangleF(1.5f, 2.5f, 2, 4);
            lines = renderer.ViewportLines(_text3x7, viewport, 1);
            Assert.That(lines.Length, Is.EqualTo(5));
            Assert.That(lines[0], Is.EqualTo(new PaintLineLocation(2, "333", new PointF(-1.5f, -0.5f))));
            Assert.That(lines[1], Is.EqualTo(new PaintLineLocation(3, "444", new PointF(-1.5f, 0.5f))));
            Assert.That(lines[2], Is.EqualTo(new PaintLineLocation(4, "555", new PointF(-1.5f, 1.5f))));
            Assert.That(lines[3], Is.EqualTo(new PaintLineLocation(5, "666", new PointF(-1.5f, 2.5f))));
            Assert.That(lines[4], Is.EqualTo(new PaintLineLocation(6, "777", new PointF(-1.5f, 3.5f))));

            // using a font sized as 8x15

            viewport = new RectangleF(1.5f * 8, 2.5f * 15, 2 * 8, 4 * 15);
            lines = renderer.ViewportLines(_text3x7, viewport, 15);
            Assert.That(lines.Length, Is.EqualTo(5));
            Assert.That(lines[0], Is.EqualTo(new PaintLineLocation(2, "333", new PointF(-1.5f * 8, -7.5f))));
            Assert.That(lines[1], Is.EqualTo(new PaintLineLocation(3, "444", new PointF(-1.5f * 8, 7.5f))));
            Assert.That(lines[2], Is.EqualTo(new PaintLineLocation(4, "555", new PointF(-1.5f * 8, 22.5f))));
            Assert.That(lines[3], Is.EqualTo(new PaintLineLocation(5, "666", new PointF(-1.5f * 8, 37.5f))));
            Assert.That(lines[4], Is.EqualTo(new PaintLineLocation(6, "777", new PointF(-1.5f * 8, 52.5f))));

            return;
        }        
    }
}
