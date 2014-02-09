// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.UiException.Controls;
using System.Windows.Forms;
using System.Drawing;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture]
    public class TestSplitterBox
    {
        private TestingSplitterBox _vertical;
        private TestingSplitterBox _horizontal;

        [SetUp]
        public void SetUp()
        {
            _vertical = new TestingSplitterBox();

            _horizontal = new TestingSplitterBox();
            _horizontal.Orientation = Orientation.Horizontal;

            return;
        }

        [Test]
        public void DefaultState()
        {
            Assert.That(_vertical.Orientation, Is.EqualTo(Orientation.Vertical));
            Assert.That(_vertical.Width, Is.EqualTo(150));
            Assert.That(_vertical.Height, Is.EqualTo(150));

            Assert.NotNull(_vertical.Control1);
            Assert.NotNull(_vertical.Control2);

            Assert.That(_vertical.Controls.Count, Is.EqualTo(2));
            Assert.That(_vertical.Controls[0], Is.SameAs(_vertical.Control1));
            Assert.That(_vertical.Controls[1], Is.SameAs(_vertical.Control2));

            Assert.That(_vertical.SplitterDistance, Is.EqualTo(0.5f));
            Assert.That(_horizontal.SplitterDistance, Is.EqualTo(0.5f));

            CheckVerticalLayout(_vertical, 0.5f);
            CheckVerticalRectangles(_vertical);

            CheckHorizontalLayout(_horizontal, 0.5f);
            CheckHorizontalRectangles(_horizontal);

            return;
        }

        void CheckVerticalLayout(TestingSplitterBox splitter, float x)
        {
            int left = (int)Math.Max(0, x * splitter.Width - SplitterBox.SPLITTER_HALFSIZE);
            left = Math.Min(left, splitter.Width - SplitterBox.SPLITTER_SIZE);

            Assert.That(splitter.Orientation, Is.EqualTo(Orientation.Vertical));
            Assert.False(splitter.SplitterRectangle.IsEmpty);

            Assert.That(splitter.SplitterRectangle.Left, Is.EqualTo(left));
            Assert.That(splitter.SplitterRectangle.Top, Is.EqualTo(0));
            Assert.That(splitter.SplitterRectangle.Width, Is.EqualTo(SplitterBox.SPLITTER_SIZE));
            Assert.That(splitter.SplitterRectangle.Height, Is.EqualTo(splitter.Height));

            Assert.That(splitter.Control1.Left, Is.EqualTo(0));
            Assert.That(splitter.Control1.Top, Is.EqualTo(0));
            Assert.That(splitter.Control1.Right, Is.EqualTo(splitter.SplitterRectangle.Left));
            Assert.That(splitter.Control1.Height, Is.EqualTo(splitter.Height));

            Assert.That(splitter.Control2.Left, Is.EqualTo(splitter.SplitterRectangle.Right));
            Assert.That(splitter.Control2.Top, Is.EqualTo(0));
            Assert.That(splitter.Control2.Right, Is.EqualTo(splitter.Width));
            Assert.That(splitter.Control2.Height, Is.EqualTo(splitter.Height));

            return;
        }

        void CheckHorizontalLayout(TestingSplitterBox splitter, float y)
        {
            int top = (int)Math.Max(0, y * splitter.Height - SplitterBox.SPLITTER_HALFSIZE);
            top = Math.Min(top, splitter.Height - SplitterBox.SPLITTER_SIZE);

            Assert.That(splitter.Orientation, Is.EqualTo(Orientation.Horizontal));
            Assert.False(splitter.SplitterRectangle.IsEmpty);
            Assert.That(splitter.SplitterRectangle,
                Is.EqualTo(new Rectangle(0, top, splitter.Width, SplitterBox.SPLITTER_SIZE)));

            Assert.That(splitter.Control1.Left, Is.EqualTo(0));
            Assert.That(splitter.Control1.Top, Is.EqualTo(0));
            Assert.That(splitter.Control1.Right, Is.EqualTo(splitter.Width));
            Assert.That(splitter.Control1.Height, Is.EqualTo(splitter.SplitterRectangle.Top));

            Assert.That(splitter.Control2.Left, Is.EqualTo(0));
            Assert.That(splitter.Control2.Top, Is.EqualTo(splitter.SplitterRectangle.Bottom));
            Assert.That(splitter.Control2.Right, Is.EqualTo(splitter.Width));
            Assert.That(splitter.Control2.Bottom, Is.EqualTo(splitter.Height));

            return;
        }

        void CheckVerticalRectangles(TestingSplitterBox splitter)
        {
            Assert.False(splitter.Collapse1Rectangle.IsEmpty);
            Assert.False(splitter.DirectionRectangle.IsEmpty);
            Assert.False(splitter.Collapse2Rectangle.IsEmpty);

            int y = (splitter.Height - 41) / 2;

            Assert.That(splitter.SplitterRectangle.Left, Is.GreaterThanOrEqualTo(0));
            Assert.That(splitter.SplitterRectangle.Right, Is.LessThanOrEqualTo(150));

            Assert.That(splitter.Collapse1Rectangle,
                Is.EqualTo(new Rectangle(splitter.SplitterRectangle.Left, y, SplitterBox.SPLITTER_SIZE, SplitterBox.BUTTON_SIZE)));

            Assert.That(splitter.DirectionRectangle,
                Is.EqualTo(new Rectangle(
                    splitter.SplitterRectangle.Left,
                    splitter.Collapse1Rectangle.Bottom + 2, SplitterBox.SPLITTER_SIZE, SplitterBox.BUTTON_SIZE)));

            Assert.That(splitter.Collapse2Rectangle,
                Is.EqualTo(new Rectangle(
                    splitter.SplitterRectangle.Left,
                    splitter.DirectionRectangle.Bottom + 2, SplitterBox.SPLITTER_SIZE, SplitterBox.BUTTON_SIZE)));
            
            return;
        }

        void CheckHorizontalRectangles(TestingSplitterBox splitter)
        {
            Assert.False(splitter.Collapse1Rectangle.IsEmpty);
            Assert.False(splitter.DirectionRectangle.IsEmpty);
            Assert.False(splitter.Collapse2Rectangle.IsEmpty);

            int x = (splitter.Width - 41) / 2;
            int y = splitter.SplitterRectangle.Top;

            Assert.That(splitter.SplitterRectangle.Top, Is.GreaterThanOrEqualTo(0));
            Assert.That(splitter.SplitterRectangle.Bottom, Is.LessThanOrEqualTo(splitter.Height));

            Assert.That(splitter.Collapse1Rectangle,
                Is.EqualTo(new Rectangle(x, y, SplitterBox.BUTTON_SIZE, SplitterBox.SPLITTER_SIZE)));

            Assert.That(splitter.DirectionRectangle,
                Is.EqualTo(new Rectangle(splitter.Collapse1Rectangle.Right + 2, y, SplitterBox.BUTTON_SIZE, SplitterBox.SPLITTER_SIZE)));

            Assert.That(splitter.Collapse2Rectangle,
                Is.EqualTo(new Rectangle(splitter.DirectionRectangle.Right + 2, y, SplitterBox.BUTTON_SIZE, SplitterBox.SPLITTER_SIZE)));

            return;
        }

        [Test]
        public void CanChangeDefaultControl1()
        {
            Control control1 = _vertical.Control1;
            Panel panel = new Panel();

            _vertical.Control1 = panel;
            Assert.False(_vertical.Controls.Contains(control1));
            Assert.True(_vertical.Controls.Contains(panel));
            CheckVerticalLayout(_vertical, 0.5f);

            _vertical.Control1 = null;
            Assert.True(_vertical.Controls.Contains(control1));
            Assert.False(_vertical.Controls.Contains(panel));
            CheckVerticalLayout(_vertical, 0.5f);

            _vertical.Control1 = null;
            Assert.True(_vertical.Controls.Contains(control1));
            Assert.False(_vertical.Controls.Contains(panel));
            CheckVerticalLayout(_vertical, 0.5f);

            return;
        }

        [Test]
        public void CanChangeDefaultControl2()
        {
            Control control2 = _vertical.Control2;
            Panel panel = new Panel();

            _vertical.Control2 = panel;
            Assert.False(_vertical.Controls.Contains(control2));
            Assert.True(_vertical.Controls.Contains(panel));
            CheckVerticalLayout(_vertical, 0.5f);

            _vertical.Control2 = null;
            Assert.True(_vertical.Controls.Contains(control2));
            Assert.False(_vertical.Controls.Contains(panel));
            CheckVerticalLayout(_vertical, 0.5f);

            _vertical.Control2 = null;
            Assert.True(_vertical.Controls.Contains(control2));
            Assert.False(_vertical.Controls.Contains(panel));
            CheckVerticalLayout(_vertical, 0.5f);

            return;
        }

        [Test]
        public void ChangingSizeInvokeDoLayout()
        {
            _vertical.Width = 100;
            CheckVerticalLayout(_vertical, 0.5f);

            _vertical.Height = 200;
            CheckVerticalLayout(_vertical, 0.5f);

            return;
        }

        [Test]
        public void OrientationAffectsLayout()
        {
            _vertical.Orientation = Orientation.Horizontal;
            CheckHorizontalLayout(_vertical, 0.5f);

            _vertical.Orientation = Orientation.Vertical;
            CheckVerticalLayout(_vertical, 0.5f);

            return;
        }

        [Test]
        public void SplitterDistance()
        {
            // vertical layout

            _vertical.SplitterDistance = 0.4f;
            Assert.That(_vertical.SplitterDistance, Is.EqualTo(0.4f));
            CheckVerticalLayout(_vertical, 0.4f);

            _vertical.SplitterDistance = -1f;
            Assert.That(_vertical.SplitterDistance, Is.EqualTo(0f));
            CheckVerticalLayout(_vertical, 0f);

            _vertical.SplitterDistance = 2f;
            Assert.That(_vertical.SplitterDistance, Is.EqualTo(1f));
            CheckVerticalLayout(_vertical, 1f);

            // horizontal layout

            _horizontal.SplitterDistance = 0.4f;
            Assert.That(_horizontal.SplitterDistance, Is.EqualTo(0.4f));
            CheckHorizontalLayout(_horizontal, 0.4f);

            _horizontal.SplitterDistance = -1;
            Assert.That(_horizontal.SplitterDistance, Is.EqualTo(0f));
            CheckHorizontalLayout(_horizontal, 0);

            _horizontal.SplitterDistance = 2f;
            Assert.That(_horizontal.SplitterDistance, Is.EqualTo(1f));
            CheckHorizontalLayout(_horizontal, 1);

            return;
        }

        [Test]
        public void PointToSplit()
        {
            // vertical layout

            _vertical.PointToSplit((int)(_vertical.Width * 0.4f), 0);
            CheckVerticalLayout(_vertical, 0.4f);            

            _vertical.PointToSplit(-1, 0);
            CheckVerticalLayout(_vertical, 0f);

            _vertical.PointToSplit(_vertical.Width * 2, 0);
            CheckVerticalLayout(_vertical, 1f);

            // horizontal layout

            _horizontal.PointToSplit(0, (int)(_horizontal.Height * 0.4f));
            CheckHorizontalLayout(_horizontal, 0.4f);

            _horizontal.PointToSplit(0, -1);
            CheckHorizontalLayout(_horizontal, 0);

            _horizontal.PointToSplit(0, _horizontal.Height * 2);
            CheckHorizontalLayout(_horizontal, 1);

            return;
        }

        [Test]
        public void CollapseControl()
        {
            // test with vertical layout

            _vertical.CollapseControl1();
            CheckVerticalRectangles(_vertical);
            CheckVerticalLayout(_vertical, 0);
            
            _vertical.CollapseControl2();
            CheckVerticalRectangles(_vertical);
            CheckVerticalLayout(_vertical, 1);

            // test with horizontal layout

            _horizontal.CollapseControl1();
            CheckHorizontalRectangles(_horizontal);
            CheckHorizontalLayout(_horizontal, 0);

            _horizontal.CollapseControl2();
            CheckHorizontalRectangles(_horizontal);
            CheckHorizontalLayout(_horizontal, 1);

            return;
        }

        [Test]
        public void MouseActions()
        {
            // test 1: check ability to move splitter

            _vertical = new TestingSplitterBox();
            _vertical.FireMouseDown(_vertical.SplitterRectangle.Left + 1, 1);
            _vertical.FireMouseMove(0, 1);
            _vertical.FireMouseUp(0, 1);

            Assert.That(_vertical.SplitterDistance, Is.EqualTo(0));

            // test 2: splitter doesn't move when mouse down occurs while
            //         the mouse is hovering Collapse1Rectangle

            _vertical = new TestingSplitterBox();
            _vertical.FireMouseDown(
                _vertical.Collapse1Rectangle.Left + 1,
                _vertical.Collapse1Rectangle.Top + 1);
            _vertical.FireMouseMove(0, 1);
            _vertical.FireMouseUp(0, 1);

            Assert.That(_vertical.SplitterDistance, Is.EqualTo(0.5f));            

            // test 3: mouse down occurs on SplitterRectangle area (except the buttons)
            //         mouse up occurs on Collapse1Rectangle
            //         CollapseControl1() should not be triggered and splitter
            //         should be at the last notified mouse position

            _vertical = new TestingSplitterBox();
            _vertical.FireMouseDown(_vertical.SplitterRectangle.Left + 1, 1);
            _vertical.FireMouseMove(150, 0);
            _vertical.FireMouseUp(_vertical.Collapse1Rectangle.Left + 1,
                _vertical.Collapse1Rectangle.Top + 1);
            Assert.That(_vertical.SplitterDistance, Is.EqualTo(1));
         
            // test 4: mouse down occurs on SplitterRectangle
            //         mouse up occurs on Collapse2Rectangle
            //         CollapseControl2 shouldn't be triggered and splitter
            //         should be at the last notified mouse position

            _vertical = new TestingSplitterBox();
            _vertical.FireMouseDown(_vertical.SplitterRectangle.Left + 1, 1);
            _vertical.FireMouseMove(0, 0);
            _vertical.FireMouseUp(_vertical.Collapse2Rectangle.Left + 1,
                _vertical.Collapse2Rectangle.Top + 1);
            Assert.That(_vertical.SplitterDistance, Is.EqualTo(0));

            // test 5: mouse down occurs on SplitterRectangle
            //         mouse up occurs on DirectionRectangle
            //         Orientation shouldn't be triggered and splitter
            //         should be at the last notified mouse position

            _vertical = new TestingSplitterBox();
            _vertical.FireMouseDown(_vertical.SplitterRectangle.Left + 1, 1);
            _vertical.FireMouseMove(0, 0);
            _vertical.FireMouseUp(_vertical.DirectionRectangle.Left + 1,
                _vertical.DirectionRectangle.Top + 1);
            Assert.That(_vertical.SplitterDistance, Is.EqualTo(0));
            Assert.That(_vertical.Orientation, Is.EqualTo(Orientation.Vertical));

            return;
        }

        class TestingSplitterBox : SplitterBox
        {
            public new Rectangle Collapse1Rectangle
            {
                get { return (base.Collapse1Rectangle); }
            }

            public new Rectangle Collapse2Rectangle
            {
                get { return (base.Collapse2Rectangle); }
            }

            public new Rectangle DirectionRectangle
            {
                get { return (base.DirectionRectangle); }
            }

            public void FireMouseDown(int x, int y)
            {
                OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, x, y, 0));
            }

            public void FireMouseMove(int x, int y)
            {
                OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
            }

            public void FireMouseUp(int x, int y)
            {
                OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, x, y, 0));
            }
        }
    }
}
