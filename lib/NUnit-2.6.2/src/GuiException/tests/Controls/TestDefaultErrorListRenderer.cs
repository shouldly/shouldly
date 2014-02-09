// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.UiException.Controls;
using System.Drawing;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture]
    public class TestDefaultErrorListRenderer
    {
        private ErrorList _empty;
        private ErrorList _filled;
        private DefaultErrorListRenderer _renderer;
        private Graphics _gr;

        [SetUp]
        public void SetUp()
        {
            _empty = new ErrorList();
            _filled = new ErrorList();
            _filled.StackTrace =
                "à SomeClass.SomeMethod() dans C:\\folder\\file1.cs:ligne 20\r\n" +
                "à ExternClass.ExternMethod()\r\n" +
                "à AnotherExternClass.AnotherExternMethod()\r\n" +
                "à SomeClass2.SomeMethod2() dans C:\\folder\\file2.cs:ligne 42\r\n" +
                "à SomeClass3.SomeMethod3() dans C:\\folder\\AnotherFile2.cs:ligne 93\r\n";

            _renderer = new DefaultErrorListRenderer();

            Image img = new Bitmap(300, 100);
            _gr = Graphics.FromImage(img);
            
            return;
        }

        [Test]
        public void DefaultState()
        {
            Assert.NotNull(_renderer.Font);
            Assert.That(_renderer.Font.Size, Is.EqualTo(8.25f));

            return;
        }

        [Test]
        public void MeasureItem()
        {
            TestingRenderer renderer = new TestingRenderer();
            SizeF exp;
            SizeF actual;

            ErrorItem itemClass = new ErrorItem("/dir/f.cs", "0123456789012.a()", 3);
            ErrorItem itemMethod = new ErrorItem("/dir/f.cs", "a.0123456789012()", 3);
            ErrorItem itemFile = new ErrorItem("/dir/0123456789012.cs", "a.b()", 3);

            // measure an item whoose width should be determined
            // by class field value

            exp = _gr.MeasureString("0123456789012", renderer.Font);
            actual = renderer.MeasureItem(_gr, itemClass);
            int itemHeight = renderer.Font.Height * 4 + 6;
            
            Assert.That((int)actual.Width, Is.EqualTo((int)exp.Width + 16));
            Assert.That((int)actual.Height, Is.EqualTo(itemHeight));

            // measure an item whoose width should be determined
            // by method field value

            exp = _gr.MeasureString("0123456789012()", renderer.Font);
            actual = renderer.MeasureItem(_gr, itemMethod);
            Assert.That((int)actual.Width, Is.EqualTo((int)exp.Width + 16));
            Assert.That((int)actual.Height, Is.EqualTo(itemHeight));

            // measure an item whoose width should be determined
            // by filename field value

            exp = _gr.MeasureString("0123456789012.cs", renderer.Font);
            actual = renderer.MeasureItem(_gr, itemFile);
            Assert.That((int)actual.Width, Is.EqualTo((int)exp.Width + 16));
            Assert.That((int)actual.Height, Is.EqualTo(itemHeight));

            return;
        }        

        [Test]
        public void ItemAt()
        {
            ErrorItem item;

            int itemHeight = _renderer.Font.Height * 4 + 6;

            item = _renderer.ItemAt(_filled.Items, _gr, new Point(0, 0));
            Assert.NotNull(item);
            Assert.That(item, Is.EqualTo(_filled.Items[0]));

            item = _renderer.ItemAt(_filled.Items, _gr, new Point(0, itemHeight + 1));
            Assert.NotNull(item);
            Assert.That(item, Is.EqualTo(_filled.Items[1]));

            Assert.Null(_renderer.ItemAt(_filled.Items, _gr, new Point(0, 480)));
            Assert.Null(_renderer.ItemAt(_filled.Items, _gr, new Point(0, -1)));
            Assert.Null(_renderer.ItemAt(null, _gr, new Point(0, 0)));

            return;
        }

        [Test]
        public void GetDocumentSize()
        {
            TestingRenderer renderer = new TestingRenderer();
            Size docSize;
            SizeF maxSize = SizeF.Empty;

            // measuring an empty list returns 0x0
            
            docSize = renderer.GetDocumentSize(_empty.Items, _gr);
            Assert.NotNull(docSize);
            Assert.That(docSize, Is.EqualTo(new Size(0, 0)));

            // measure for a non empty list relies on the longest item
            // in that list
			
			foreach(ErrorItem item in _filled.Items)
			{
				SizeF sz = renderer.MeasureItem(_gr, item);
				if (sz.Width > maxSize.Width)
					maxSize = sz;
			}

            docSize = renderer.GetDocumentSize(_filled.Items, _gr);
            Assert.NotNull(docSize);
            Assert.That(docSize.Width, Is.EqualTo((int)maxSize.Width));
            int itemHeight = renderer.Font.Height * 4 + 6;
            Assert.That(docSize.Height, Is.EqualTo(_filled.Items.Count * itemHeight));

            return;
        }

        [Test]
        public void DrawToGraphics_Can_Throw_ArgumentNullException()
        {
            try {
                _renderer.DrawToGraphics(null, null, _gr, new Rectangle()); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.That(e.Message.Contains("items"));
            }

            try {
                _renderer.DrawToGraphics(_filled.Items, null, null, new Rectangle()); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.That(e.Message.Contains("g"));
            }

            return;
        }

        [Test]
        public void IsDirty()
        {
            Rectangle viewport = new Rectangle(0, 0, 200, 200);
            TestingRenderer renderer = new TestingRenderer();

            // no change - true the second time

            renderer.DrawToGraphics(_filled.Items, _filled.Items[0], _gr, viewport);
            Assert.False(renderer.IsDirty(_filled.Items, _filled.Items[0], viewport));

            // changing the list set dirty flag

            renderer.DrawToGraphics(_filled.Items, _filled.Items[0], _gr, viewport);
            Assert.True(renderer.IsDirty(_empty.Items, _filled.Items[0], viewport));
           
            // changes selected item

            renderer.DrawToGraphics(_filled.Items, _filled.Items[0], _gr, viewport);
            Assert.True(renderer.IsDirty(_filled.Items, null, viewport));
            Assert.True(renderer.IsDirty(_filled.Items,
                new ErrorItem(_filled.Items[0].Path,
                              _filled.Items[0].FullyQualifiedMethodName,
                              _filled.Items[0].LineNumber),
                    viewport));

            // changes viewport

            renderer.DrawToGraphics(_filled.Items, _filled.Items[0], _gr, viewport);
            Assert.True(renderer.IsDirty(_filled.Items, _filled.Items[0], new Rectangle()));

            // reversing item order set dirty flag

            renderer.DrawToGraphics(_filled.Items, _filled.Items[0], _gr, viewport);
            _filled.Items.Reverse();
            Assert.True(renderer.IsDirty(_filled.Items, _filled.Items[_filled.Items.Count - 1], viewport));

            // Calling GetDocumentSize set dirty flag

            renderer.DrawToGraphics(_filled.Items, _filled.Items[0], _gr, viewport);
            renderer.GetDocumentSize(_filled.Items, _gr);
            Assert.True(renderer.IsDirty(_filled.Items, _filled.Items[0], viewport));

            return;
        }

        class TestingRenderer : DefaultErrorListRenderer
        {
            public new SizeF MeasureItem(Graphics g, ErrorItem item) {
                return (base.MeasureItem(g, item));
            }

            public new bool IsDirty(ErrorItemCollection items, ErrorItem selection, Rectangle viewport) {
                return (base.IsDirty(items, selection, viewport));
            }
        }
    }
}
