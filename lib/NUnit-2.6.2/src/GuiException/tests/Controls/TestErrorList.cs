// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if NET_3_5 || NET_4_0 || NET_4_5
using System;
using NSubstitute;
using NUnit.Framework;
using NUnit.UiException.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture, Platform("Net-3.5,Mono-3.5,Net-4.0")]
    public class TestErrorList
    {
        private TestingErrorList _list;
        private bool _selectionNotification;

        private string _trace1;
        private string _trace2;

        private IErrorListRenderer _mockRenderer;

        [SetUp]
        public void SetUp()
        {
            _mockRenderer = Substitute.For<IErrorListRenderer>();

            _list = new TestingErrorList(_mockRenderer);

            _trace1 =
                "à SomeClass.SomeMethod() dans C:\\folder\\file1.cs:ligne 20\r\n" +
                "à ExternClass.ExternMethod()\r\n" +
                "à AnotherExternClass.AnotherExternMethod()\r\n" +
                "à SomeClass2.SomeMethod2() dans C:\\folder\\file2.cs:ligne 42\r\n" +
                "à SomeClass3.SomeMethod3() dans C:\\folder\\AnotherFile2.cs:ligne 93\r\n";

            _trace2 =
                "à SomeClass.SomeMethod() dans C:\\folder\\file1.cs:ligne 20\r\n" +
                "à ExternClass.ExternMethod()\r\n" +
                "à AnotherExternClass.AnotherExternMethod()\r\n" +
                "à SomeClass2.SomeMethod2() dans C:\\folder\\file2.cs:ligne 42\r\n";

            _selectionNotification = false;

            _list.SelectedItemChanged += new EventHandler(_list_SelectedItemChanged);

            return;
        }

        void _list_SelectedItemChanged(object sender, EventArgs e)
        {
            this._selectionNotification = true;
        }

        [Test]
        public void DefaultState()
        {
            Assert.False(_list.AutoSelectFirstItem);
            Assert.Null(_list.StackTrace);
            Assert.NotNull(_list.Items);
            Assert.That(_list.Items.Count, Is.EqualTo(0));
            Assert.Null(_list.SelectedItem);
            Assert.That(_list.HoveredIndex, Is.EqualTo(-1));
            Assert.That(_list.ListOrderPolicy, Is.EqualTo(ErrorListOrderPolicy.InitialOrder));

            return;
        }

        [Test]
        public void AutoSelectFirstItem()
        {
            _list.UseDefaultRenderer();

            // Test #1:
            // Populate StackTrace with one localizable item
            // When AutoSelectFirstItem is set, we expect the
            // first item to be automatically selected.

            _list.AutoSelectFirstItem = true;
            _list.StackTrace = _trace1;
            Assert.NotNull(_list.SelectedItem);
            Assert.That(_list.SelectedItem, Is.EqualTo(_list.Items[0]));

            // Test #2:
            // Populate StackTrace with no localizable item
            // Though AutoSelectFirstItem is set, selection
            // should be null

            _list.StackTrace = "à SomeClass.SomeMethod()";
            Assert.Null(_list.SelectedItem);

            // Test #3
            // Populate StackTrace with one localizable item.
            // This time AutoSelectFirstItem is not set. We
            // expect selection to be null.

            _list.AutoSelectFirstItem = false;
            _list.StackTrace = "à SomeClass.SomeMethod() dans C:\\folder\\file.cs:ligne 1";
            Assert.Null(_list.SelectedItem);

            return;
        }

        [Test]
        public void Populate_StackTraceSource()
        {
            Size docSize = new Size(200, 500);

            _mockRenderer.GetDocumentSize(_list.Items, _list.WorkingGraphics).Returns(docSize);

            _list.StackTrace = _trace1;
            Assert.That(_list.AutoScrollMinSize, Is.EqualTo(new Size(200, 500)));

            Assert.That(_list.StackTrace, Is.EqualTo(_trace1));
            Assert.That(_list.Items.Count, Is.EqualTo(5));
            Assert.That(_list.Items[0].LineNumber, Is.EqualTo(20));
            Assert.That(_list.Items[3].LineNumber, Is.EqualTo(42));
            Assert.That(_list.Items[4].LineNumber, Is.EqualTo(93));

            return;
        }

        [Test]
        public void CurrentSelection()
        {
            _list.UseDefaultRenderer();
            _list.StackTrace = _trace1;

            // can select an item with underlying source code

            _selectionNotification = false;
            _list.SelectedItem = _list.Items[0];
            Assert.That(_list.SelectedItem, Is.EqualTo(_list.Items[0]));
            Assert.True(_selectionNotification);

            // attempting to select an item not localizable
            // has no effect

            _selectionNotification = false;
            _list.SelectedItem = _list.Items[1];
            Assert.That(_list.SelectedItem, Is.EqualTo(_list.Items[0]));
            Assert.False(_selectionNotification);

            // attempting to select an item not in the list
            // has no effect

            _selectionNotification = false;
            _list.SelectedItem = new ErrorItem("C:\\folder\\file42.cs", "SomeClass42.SomeMethod42()", 3);
            Assert.That(_list.SelectedItem, Is.EqualTo(_list.Items[0]));
            Assert.False(_selectionNotification);

            // can pass null to SelectedItem
            _list.SelectedItem = null;
            Assert.Null(_list.SelectedItem);
            Assert.True(_selectionNotification);

            // select an item an clear StackTrace
            // selection should be reset to null

            _list.SelectedItem = _list.Items[0];
            _selectionNotification = false;
            _list.StackTrace = null;
            Assert.Null(_list.SelectedItem);
            Assert.True(_selectionNotification);

            return;
        }

        [Test]
        public void ListOrderPolicy()
        {
            ErrorList list = new ErrorList();

            // Check ListOrderPolicy behavior when AutoSelectFirstItem is not set

            list.AutoSelectFirstItem = false;
            list.ListOrderPolicy = ErrorListOrderPolicy.ReverseOrder;
            Assert.That(list.ListOrderPolicy, Is.EqualTo(ErrorListOrderPolicy.ReverseOrder));

            list.StackTrace = _trace1;
            Assert.That(list.Items[0].LineNumber, Is.EqualTo(93));
            Assert.That(list.Items[1].LineNumber, Is.EqualTo(42));
            Assert.That(list.Items[2].LineNumber, Is.EqualTo(0));
            Assert.That(list.Items[3].LineNumber, Is.EqualTo(0));
            Assert.That(list.Items[4].LineNumber, Is.EqualTo(20));

            list.StackTrace = _trace2;
            Assert.That(list.Items[0].LineNumber, Is.EqualTo(42));
            Assert.That(list.Items[1].LineNumber, Is.EqualTo(0));
            Assert.That(list.Items[2].LineNumber, Is.EqualTo(0));
            Assert.That(list.Items[3].LineNumber, Is.EqualTo(20));

            list.ListOrderPolicy = ErrorListOrderPolicy.InitialOrder;
            Assert.That(list.ListOrderPolicy, Is.EqualTo(ErrorListOrderPolicy.InitialOrder));

            Assert.That(list.Items[0].LineNumber, Is.EqualTo(20));
            Assert.That(list.Items[1].LineNumber, Is.EqualTo(0));
            Assert.That(list.Items[2].LineNumber, Is.EqualTo(0));
            Assert.That(list.Items[3].LineNumber, Is.EqualTo(42));

            list.StackTrace = _trace1;
            Assert.That(list.Items[0].LineNumber, Is.EqualTo(20));
            Assert.That(list.Items[1].LineNumber, Is.EqualTo(0));
            Assert.That(list.Items[2].LineNumber, Is.EqualTo(0));
            Assert.That(list.Items[3].LineNumber, Is.EqualTo(42));
            Assert.That(list.Items[4].LineNumber, Is.EqualTo(93));

            // When the AutoSelectFirstItem flag is set, the selected item
            // is the one the most on top of the list, with source attachment.
            // Where the most on top depends whether the order is kept unchanged
            // or reversed.

            list.AutoSelectFirstItem = true;
            list.ListOrderPolicy = ErrorListOrderPolicy.InitialOrder;
            list.StackTrace = _trace1;
            Assert.That(list.SelectedItem.LineNumber, Is.EqualTo(20));

            list.ListOrderPolicy = ErrorListOrderPolicy.ReverseOrder;
            list.StackTrace = _trace1;
            Assert.That(list.SelectedItem.LineNumber, Is.EqualTo(93));

            return;
        }

        [Test]
        public void CanReportInvalidItems()
        {
            ErrorList list = new ErrorList();

            // feeding ErrorList with garbage details should make it
            // fail gracefully.

            list.StackTrace =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\r\n" +
                "Nam at nisi ut neque sollicitudin ultrices. Sed rhoncus\r\n" +
                "rhoncus arcu. Morbi eu elit ut augue congue luctus. Nullam\r\n" +
                "eu eros. Nunc blandit varius orci. Mauris condimentum diam\r\n" +
                "ac ligula. Nullam ut metus. Maecenas sagittis nibh in nisl.\r\n" +
                "Phasellus rhoncus diam a nulla. Integer vestibulum.\r\n";

            Assert.That(list.Items.Count, Is.EqualTo(1));
            Assert.That(list.Items[0].BaseMethodName, Is.EqualTo("Fail to parse stack trace"));
            Assert.IsFalse(list.Items[0].HasSourceAttachment);

            return;
        }


        [Test]
        public void Invoking_DrawToGraphics()
        {
            Size docSize = new Size(200, 500);

            _mockRenderer.GetDocumentSize(_list.Items, _list.WorkingGraphics).Returns(docSize);

            _list.StackTrace = _trace1;

            _list.HoveredIndex = 0;
            _list.FireOnPaint();

            _mockRenderer.Received().DrawToGraphics(_list.Items, _list.SelectedItem, _list.WorkingGraphics, _list.ClientRectangle);
            _mockRenderer.Received().DrawItem(_list.Items[0], 0, true, false, _list.WorkingGraphics, _list.ClientRectangle);

            return;
        }

        [Test]
        public void Click_Can_Select_Item()
        {
            Size docSize = new Size(200, 500);
            Point point;
            ErrorItem selection;

            _mockRenderer.GetDocumentSize(_list.Items, _list.WorkingGraphics).Returns(docSize);
            _list.StackTrace = _trace1;

            // simulate a click to 10, 10 - a clickable element

            point = new Point(10, 10);
            _mockRenderer.ItemAt(_list.Items, _list.WorkingGraphics, point).Returns(_list.Items[0]);
            _list.FireClick(point);

            Assert.NotNull(_list.SelectedItem);
            Assert.That(_list.SelectedItem, Is.EqualTo(_list.Items[0]));

            // simulate a click in 10, 110 - this element is not clickable => no source

            selection = _list.SelectedItem;
            point = new Point(10, 110);
            _mockRenderer.ItemAt(_list.Items, _list.WorkingGraphics, point).Returns(_list.Items[1]);
            _list.FireClick(point);

            Assert.NotNull(_list.SelectedItem);
            Assert.That(_list.SelectedItem, Is.SameAs(selection));

            return;
        }

        [Test]
        public void DrawItem()
        {
            Size docSize = new Size(200, 500);
            Point point;

            _mockRenderer.GetDocumentSize(_list.Items, _list.WorkingGraphics).Returns(docSize);

            _list.StackTrace = _trace1;

            // mouse move hover a selectable item

            point = new Point(0, 0);
            _mockRenderer.ItemAt(_list.Items, _list.WorkingGraphics, point).Returns(_list.Items[0]);
            _list.FireMouseMove(point);
            Assert.True(_list.ITEM_ENTERED_NOTIFICATION);
            Assert.That(_list.HoveredIndex, Is.EqualTo(0));

            _list.ResetFlags();
            point = new Point(0, 50);
            _mockRenderer.ItemAt(_list.Items, _list.WorkingGraphics, point).Returns(_list.Items[1]);
            _list.FireMouseMove(point);
            Assert.False(_list.ITEM_ENTERED_NOTIFICATION); // items[1] is not hoverable...
            Assert.True(_list.ITEM_LEAVED_NOTIFICATION); // has left items[0]            
            Assert.That(_list.HoveredIndex, Is.EqualTo(-1));

            _list.ResetFlags();
            point = new Point(0, 100);
            _mockRenderer.ItemAt(_list.Items, _list.WorkingGraphics, point).Returns(_list.Items[3]);
            _list.FireMouseMove(point);
            Assert.True(_list.ITEM_ENTERED_NOTIFICATION); // items[3] is hoverable...
            Assert.False(_list.ITEM_LEAVED_NOTIFICATION); // items[1] was not hoverable
            Assert.That(_list.HoveredIndex, Is.EqualTo(3));

            // reset of stack trace causes HoverIndex to reset as well

            _mockRenderer.GetDocumentSize(_list.Items, _list.WorkingGraphics).Returns(docSize);
            _list.StackTrace = null;
            Assert.That(_list.HoveredIndex, Is.EqualTo(-1));

            return;
        }

        class TestingErrorList : ErrorList
        {
            public bool ITEM_ENTERED_NOTIFICATION;
            public bool ITEM_LEAVED_NOTIFICATION;

            public TestingErrorList(IErrorListRenderer renderer) :
                base(renderer)
            {
            }

            public void ResetFlags()
            {
                ITEM_ENTERED_NOTIFICATION = false;
                ITEM_LEAVED_NOTIFICATION = false;
            }

            public void FireClick(Point point)
            {
                OnClick(point);
            }

            public void FireMouseMove(Point point)
            {
                OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, point.X, point.Y, 0));
            }

            public void UseDefaultRenderer()
            {
                _renderer = new DefaultErrorListRenderer();
            }

            public int HoveredIndex
            {
                get { return (_hoveredIndex); }
                set { _hoveredIndex = value; }
            }

            public Graphics WorkingGraphics
            {
                get { return (_workingGraphics); }
            }

            public void FireOnPaint()
            {
                OnPaint(new PaintEventArgs(WorkingGraphics, ClientRectangle));

                return;
            }

            protected override void ItemEntered(int index)
            {
                base.ItemEntered(index);
                ITEM_ENTERED_NOTIFICATION = true;
            }

            protected override void ItemLeaved(int index)
            {
                base.ItemLeaved(index);
                ITEM_LEAVED_NOTIFICATION = true;
            }
        }
    }
}
#endif