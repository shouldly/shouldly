// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// Displays a control which implements IStackTraceView.
    /// </summary>
    public class ErrorList :
        UserControl,
        IStackTraceView
    {
        public event EventHandler SelectedItemChanged;

        private ErrorListOrderPolicy _listOrder;
        private ErrorItemCollection _items;
        private ErrorItem _selection;
        private string _stackTrace;
        protected IErrorListRenderer _renderer;
        protected Graphics _workingGraphics;
        protected int _hoveredIndex;

        private Point _mouse;
        private bool _autoSelectFirstItem;

        /// <summary>
        /// Builds a new instance of ErrorList.
        /// </summary>
        public ErrorList() :
            this(new DefaultErrorListRenderer())
        {
        }

        /// <summary>
        /// Gives access to the item collection.
        /// </summary>
        public ErrorItemCollection Items
        {
            get { return (_items); }
        }

        #region IStackTraceView Members

        public bool AutoSelectFirstItem
        {
            get { return (_autoSelectFirstItem); }
            set { _autoSelectFirstItem = value; }
        }

        public string StackTrace
        {
            get { return (_stackTrace); }
            set
            {
                ErrorItem candidate;

                candidate = PopulateList(value);

                if (!String.IsNullOrEmpty(value) &&
                    _items.Count == 0)
                    _items.Add(new ErrorItem(null, "Fail to parse stack trace", -1));

                AutoScrollMinSize = _renderer.GetDocumentSize(_items, _workingGraphics);

                _hoveredIndex = -1;
                SelectedItem = (AutoSelectFirstItem ? candidate : null);
                Invalidate();

                return;
            }
        }

        public ErrorItem SelectedItem
        {
            get { return (_selection); }
            set
            {
                bool fireEvent;

                if (value != null &&
                    (!_items.Contains(value) || !value.HasSourceAttachment))
                    return;

                fireEvent = (_selection != value);
                _selection = value;

                if (fireEvent && SelectedItemChanged != null)
                    SelectedItemChanged(this, new EventArgs());

                Invalidate();
            }
        }

        public ErrorListOrderPolicy ListOrderPolicy
        {
            get { return (_listOrder); }
            set
            {
                if (_listOrder == value)
                    return;
                _listOrder = value;
                _items.Reverse();
                Invalidate();
            }
        }

        #endregion

        protected ErrorList(IErrorListRenderer renderer)
        {
            UiExceptionHelper.CheckNotNull(renderer, "display");

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;

            _renderer = renderer;
            _items = new ErrorItemCollection();
            _stackTrace = null;
            _selection = null;
            _workingGraphics = CreateGraphics();
            _hoveredIndex = -1;

            _autoSelectFirstItem = false;
            _listOrder = ErrorListOrderPolicy.InitialOrder;

            return;
        }

        protected virtual void ItemEntered(int index)
        {
            Cursor = Cursors.Hand;
        }

        protected virtual void ItemLeaved(int index)
        {
            Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle viewport;

            base.OnPaint(e);

            viewport = new Rectangle(-AutoScrollPosition.X, -AutoScrollPosition.Y, 
                ClientRectangle.Width, ClientRectangle.Height);
            _renderer.DrawToGraphics(_items, _selection, e.Graphics, viewport);

            if (_hoveredIndex != -1)
                _renderer.DrawItem(_items[_hoveredIndex], _hoveredIndex, true,
                    _items[_hoveredIndex] == _selection, e.Graphics, viewport);

            return;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Focus();
        }
       
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _mouse = new Point(e.X, e.Y - AutoScrollPosition.Y);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            ErrorItem item;
            int itemIndex;

            base.OnMouseMove(e);

            item = _renderer.ItemAt(_items, _workingGraphics, new Point(e.X, e.Y - AutoScrollPosition.Y));

            itemIndex = -1;
            for (int i = 0; i < _items.Count; ++i)
                if (Object.ReferenceEquals(_items[i], item))
                {
                    itemIndex = i;
                    break;
                }            

            if (itemIndex != _hoveredIndex)
            {
                if (_hoveredIndex != -1)
                    ItemLeaved(_hoveredIndex);

                if (itemIndex != -1 && _items[itemIndex].HasSourceAttachment)
                {
                    ItemEntered(itemIndex);
                    _hoveredIndex = itemIndex;
                }
                else
                    _hoveredIndex = -1;
                Invalidate();
            }

            return;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            OnClick(_mouse);

            return;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            this._renderer.Font = this.Font;

            base.OnFontChanged(e);
        }

        protected void OnClick(Point point)
        {
            SelectedItem = _renderer.ItemAt(_items, _workingGraphics, point);

            return;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        private ErrorItem PopulateList(string stackTrace)
        {
            StackTraceParser parser = new StackTraceParser();
            ErrorItem candidate;

            _stackTrace = stackTrace;
            parser.Parse(stackTrace);
            if (_listOrder == ErrorListOrderPolicy.ReverseOrder)
                parser.Items.Reverse();

            candidate = null;
            _items.Clear();
            foreach (ErrorItem item in parser.Items)
            {
                if (candidate == null && item.HasSourceAttachment)
                    candidate = item;
                _items.Add(item);
            }

            return (candidate);
        }
    }
}
