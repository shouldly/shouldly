// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using NUnit.UiException.Properties;
using System.Diagnostics;

//
// This re-implements SplitContainer. Why re-inventing the wheel?
// Well... I faced some strange behaviors in SplitContainer in particular
// when I started to provide a custom paint method. It seems to me
// that there is a kind of defect that affects how the Invalidate or
// paint event is called. In some situations I faced a SplitContainer
// that didn't redraw itself while having some parts of its window
// dirty. I didn't found out the cause of the problem.
//
// Another feature that is quite annoying is the unability to change
// the mouse cursor while hovering some special areas of the splitter
// bar. Maybe there is a trick or something but the normal way doesn't
// look like to work.
//

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// Implements a place holder that can be splitted either horizontally or vertically.
    /// The SplitterBox is layouted with two place holders, respectively named Control1
    /// and Control2 where clients can put their controls.
    /// 
    /// Unlike SplitContainer, the place holders in SplitterBox are the client controls
    /// itself. The direct consequence is the layout policy will be to dock the client
    /// controls in filling the maximum possible space.
    /// 
    /// SplitterBox also add three buttons on the splitter bar that to change the split
    /// orientation and collapse either Control1 or Control2. The example below shows
    /// how to intialize and set up SplitterBox with two controls.
    /// <code>
    /// // creates a new SplitterBox, with a vertical split
    /// // and position splitter to appear in the middle of the window
    /// SplitterBox splitter = new SplitterBox();
    /// splitter.Orientation = Orientation.Vertical;
    /// splitter.SplitterDistance = 0.5f;
    /// splitter.Control1 = oneControl;
    /// splitter.Control2 = anotherControl;
    /// </code>
    /// </summary>
    public class SplitterBox : Control
    {
        public static readonly int SPLITTER_SIZE = 9;
        public static readonly int SPLITTER_HALFSIZE = SPLITTER_SIZE / 2;
        public static readonly int BUTTON_SIZE = 13;
        
        private Control _emptyControl1;
        private Control _emptyControl2;

        private Control _control1;
        private Control _control2;

        private Orientation _orientation;        
        private float _x;
        private float _y;

        private Rectangle _splitterRectangle;
        private Rectangle _collapse1Rectangle;
        private Rectangle _collapse2Rectangle;
        private Rectangle _directionRectangle;

        private bool _movingSplitter;

        private Brush _brush;
        private Pen _pen;

        private Rectangle _rVerticalCollapse1;
        private Rectangle _rVerticalDirection;
        private Rectangle _rVerticalCollapse2;

        private Rectangle _rHorizontalCollapse1;
        private Rectangle _rHorizontalDirection;
        private Rectangle _rHorizontalCollapse2;

        public event EventHandler OrientationChanged;
        public event EventHandler SplitterDistanceChanged;

        /// <summary>
        /// Creates a new SplitterBox.
        /// </summary>
        public SplitterBox()
        {
            _brush = new SolidBrush(Color.FromArgb(146, 180, 224));
            _pen = new Pen(Color.FromArgb(103, 136, 190));

            _rVerticalCollapse1 = new Rectangle(0, 0, 9, 13);
            _rVerticalDirection = new Rectangle(10, 0, 9, 13);
            _rVerticalCollapse2 = new Rectangle(20, 0, 9, 13);

            _rHorizontalCollapse1 = new Rectangle(0, 24, 13, 9);
            _rHorizontalDirection = new Rectangle(14, 14, 13, 9);
            _rHorizontalCollapse2 = new Rectangle(0, 14, 13, 9);     

            _emptyControl1 = new Control();
            _emptyControl2 = new Control();

            Width = 150;
            Height = 150;

            _control1 = _emptyControl1;
            _control2 = _emptyControl2;

            Controls.Add(_control1);
            Controls.Add(_control2);

            _x = _y = 0.5f;

            Orientation = Orientation.Vertical;

            DoLayout();

            return;
        }

        /// <summary>
        /// Gets or sets the orientation of the splitter in the SplitterBox.
        /// </summary>
        public Orientation Orientation
        {
            get { return (_orientation); }
            set { 
                _orientation = value;
                DoLayout();
            }
        }

        /// <summary>
        /// Gets or sets the splitter distance expressed as a float number in the
        /// range [0 - 1]. A value of 0 collapses Control1 and makes Control2 take
        /// the whole space in the window. A value of 1 collapses Control2 and makes
        /// Control1 take the whole space in the window. A value of 0.5 makes the
        /// splitter appear in the middle of the window.
        /// 
        /// Values that don't fall in [0 - 1] are automatically clipped to this range.
        /// </summary>
        public float SplitterDistance
        {
            get { return (_orientation == Orientation.Vertical ? _x : _y); }
            set {
                value = Math.Max(0, Math.Min(1, value));
                if (_orientation == Orientation.Vertical)
                    _x = value;
                else
                    _y = value;
                DoLayout();
            }
        }

        /// <summary>
        /// Gets or sets the "first" control to be shown. This control will appear
        /// either at the top or on the left when the orientation is respectively
        /// vertical or horizontal.
        ///   If the value is not null, the control will automatically be added
        /// to the SplitterBox's hierarchy of controls.
        ///   If the value is null, the former control is removed and replaced
        /// by a default and empty area.
        /// </summary>
        public Control Control1
        {
            get { return (_control1); }
            set 
            {
                if (_control1 == value)
                    return;

                Controls.Remove(_control1);
                if (value == null)
                    value = _emptyControl1;
                _control1 = value;
                Controls.Add(value);
                DoLayout();

                return;
            }
        }

        /// <summary>
        /// Gets or sets the "second" control to be shown. This control will appear
        /// either at the bottom or on the right when the orientation is respectively
        /// vertical or horizontal.
        ///   If the value is not null, the control will automatically be added
        /// to the SplitterBox's hierarchy of controls.
        ///   If the value is null, the former control is removed and replaced
        /// by a default and empty area.
        /// </summary>
        public Control Control2
        {
            get { return (_control2); }
            set 
            {
                if (_control2 == value)
                    return;

                if (value == null)
                    value = _emptyControl2;

                Controls.Remove(_control2);
                _control2 = value;
                Controls.Add(value);
                DoLayout();

                return;
            }
        }

        /// <summary>
        /// Gets the rectangle occupied with the splitter.
        /// </summary>
        public Rectangle SplitterRectangle
        {
            get { return (_splitterRectangle); }            
        }

        /// <summary>
        /// Sets a new location for the splitter expressed as client coordinate.
        /// </summary>
        /// <param name="x">The new location in pixels when orientation is set to Vertical.</param>
        /// <param name="y">The new location in pixels when orientation is set to Horizontal.</param>
        public void PointToSplit(int x, int y)
        {
            if (_orientation == Orientation.Vertical)
            {
                x = Math.Max(0, Math.Min(Width, x));
                _x = (float)x / (float)Width;
            }
            else
            {
                y = Math.Max(0, Math.Min(Height, y));
                _y = (float)y / (float)Height;
            }

            DoLayout();

            return;
        }

        /// <summary>
        /// Collapses Control1.
        /// </summary>
        public void CollapseControl1()
        {
            PointToSplit(0, 0);
        }

        /// <summary>
        /// Collapses Control2.
        /// </summary>
        public void CollapseControl2()
        {
            PointToSplit(Width, Height);
        }

        protected Rectangle Collapse1Rectangle
        {
            get { return (_collapse1Rectangle); }
        }

        protected Rectangle Collapse2Rectangle
        {
            get { return (_collapse2Rectangle); }
        }

        protected Rectangle DirectionRectangle
        {
            get { return (_directionRectangle); }
        }        

        private void HorizontalLayout()
        {
            int x = (Width - 41) / 2;
            int y;
            int top;

            top = (int)Math.Max(0, _y * Height - SPLITTER_HALFSIZE);
            top = Math.Min(top, Height - SPLITTER_SIZE);

            _splitterRectangle = new Rectangle(0, top, Width, SPLITTER_SIZE);

            y = _splitterRectangle.Top;

            _collapse1Rectangle = new Rectangle(x, y, BUTTON_SIZE, SPLITTER_SIZE);
            _directionRectangle = new Rectangle(_collapse1Rectangle.Right + 2, y, BUTTON_SIZE, SPLITTER_SIZE);
            _collapse2Rectangle = new Rectangle(_directionRectangle.Right + 2, y, BUTTON_SIZE, SPLITTER_SIZE);

            _control1.SetBounds(0, 0, Width, _splitterRectangle.Top);
            _control2.SetBounds(0, _splitterRectangle.Bottom, Width, Height - _splitterRectangle.Bottom);

            return;
        }

        private void VerticalLayout()
        {
            int y = (Height - 41) / 2;
            int left;
            int x;

            left = (int)Math.Max(0, _x * Width - SPLITTER_HALFSIZE);
            left = Math.Min(left, Width - SPLITTER_SIZE);

            _splitterRectangle = new Rectangle(left, 0, SPLITTER_SIZE, Height);

            x = _splitterRectangle.Left;

            _collapse1Rectangle = new Rectangle(x, y, SPLITTER_SIZE, BUTTON_SIZE);
            _directionRectangle = new Rectangle(x, _collapse1Rectangle.Bottom + 2, SPLITTER_SIZE, BUTTON_SIZE);
            _collapse2Rectangle = new Rectangle(x, _directionRectangle.Bottom + 2, SPLITTER_SIZE, BUTTON_SIZE);

            _control1.SetBounds(0, 0, _splitterRectangle.Left, Height);
            _control2.SetBounds(_splitterRectangle.Right, 0, Width - _splitterRectangle.Right, Height);

            return;
        }

        private void DoLayout()
        {
            if (_orientation == Orientation.Vertical)
                VerticalLayout();
            else
                HorizontalLayout();

            Invalidate();

            return;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_control1 != null)
                DoLayout();
            base.OnSizeChanged(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor = Cursors.Default;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            UpdateCursor(e.X, e.Y);

            if (_splitterRectangle.Contains(e.X, e.Y))
            {
                if (HoveringButtons(e.X, e.Y))
                    return;

                _movingSplitter = true;
            }

            base.OnMouseDown(e);

            return;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            UpdateCursor(e.X, e.Y);

            if (_movingSplitter == true)
            {                
                PointToSplit(e.X, e.Y);
                Invalidate();
            }

            base.OnMouseMove(e);

            return;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            bool wasMovingSplitter;

            UpdateCursor(e.X, e.Y);

            wasMovingSplitter = _movingSplitter;
            _movingSplitter = false;

            if (wasMovingSplitter)
                FireSplitterDistanceChanged();
            else
            {
                if (_collapse1Rectangle.Contains(e.X, e.Y))
                {
                    CollapseControl1();
                    FireSplitterDistanceChanged();
                    return;
                }

                if (_collapse2Rectangle.Contains(e.X, e.Y))
                {
                    CollapseControl2();
                    FireSplitterDistanceChanged();
                    return;
                }

                if (_directionRectangle.Contains(e.X, e.Y))
                {
                    Orientation = (_orientation == Orientation.Vertical) ?
                            Orientation.Horizontal :
                            Orientation.Vertical;

                    FireOrientationChanged();

                    return;
                }
            }

            base.OnMouseUp(e);

            return;
        }

        private void FireOrientationChanged()
        {
            if (OrientationChanged != null)
                OrientationChanged(this, EventArgs.Empty);
        }

        private void FireSplitterDistanceChanged()
        {
            if (SplitterDistanceChanged != null)
                SplitterDistanceChanged(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(_brush, _splitterRectangle);

            if (Orientation == Orientation.Vertical)
            {
                e.Graphics.DrawLine(_pen, _splitterRectangle.Left, 0,
                    SplitterRectangle.Left, _splitterRectangle.Height);
                e.Graphics.DrawLine(_pen, _splitterRectangle.Right - 1, 0,
                    SplitterRectangle.Right - 1, _splitterRectangle.Height);

                e.Graphics.DrawImage(Resources.ImageSplitterBox,
                    _collapse1Rectangle,
                    _rVerticalCollapse1,
                    GraphicsUnit.Pixel);

                e.Graphics.DrawImage(Resources.ImageSplitterBox,
                    _directionRectangle,
                    _rVerticalDirection,
                    GraphicsUnit.Pixel);

                e.Graphics.DrawImage(Resources.ImageSplitterBox,
                    _collapse2Rectangle,
                    _rVerticalCollapse2,
                    GraphicsUnit.Pixel);
            }
            else
            {
                e.Graphics.DrawLine(_pen, 0, _splitterRectangle.Top,
                    Width, _splitterRectangle.Top);
                e.Graphics.DrawLine(_pen, 0, _splitterRectangle.Bottom - 1,
                    Width, _splitterRectangle.Bottom - 1);

                e.Graphics.DrawImage(Resources.ImageSplitterBox,
                    _collapse1Rectangle,
                    _rHorizontalCollapse1,
                    GraphicsUnit.Pixel);

                e.Graphics.DrawImage(Resources.ImageSplitterBox,
                    _directionRectangle,
                    _rHorizontalDirection,
                    GraphicsUnit.Pixel);

                e.Graphics.DrawImage(Resources.ImageSplitterBox,
                    _collapse2Rectangle,
                    _rHorizontalCollapse2,
                    GraphicsUnit.Pixel);
            }

            base.OnPaint(e);

            return;
        }

        private bool HoveringButtons(int x, int y)
        {
            if (!SplitterRectangle.Contains(x, y))
                return (false);

            return (_collapse1Rectangle.Contains(x, y) ||
                    _collapse2Rectangle.Contains(x, y) ||
                    _directionRectangle.Contains(x, y));
        }

        private void UpdateCursor(int x, int y)
        {
            if (!SplitterRectangle.Contains(x, y) ||
                HoveringButtons(x, y))
            {
                Cursor = Cursors.Default;
                return;
            }

            Cursor = (Orientation == Orientation.Vertical ? Cursors.VSplit : Cursors.HSplit);

            return;
        }
    }
}
