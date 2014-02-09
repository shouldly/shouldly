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

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// Provides the panels and layout of ErrorBrowser as
    /// shown below:
    /// 
    /// +--------------------------------------------+
    /// |                  Toolbar                   |
    /// +--------------------------------------------+
    /// |                                            |
    /// |                                            |
    /// |                  Content                   |
    /// |                                            |
    /// |                                            |
    /// +--------------------------------------------+
    /// 
    /// Toolbar: the control which shows buttons
    ///          to alternate between the StackTraceDisplay
    ///          and BrowserDisplay back and forth.
    ///          The control collection of this control
    ///          never changes.
    ///               
    /// Option:  a free place holder to show subfeature
    ///          for a specific display (e.g: StackTraceDisplay
    ///          or BrowserDisplay). This control's
    ///          collection changes in relation with the
    ///          selected display.
    ///               
    /// Content: the place where to put the main content
    ///          for the current display. This control's 
    ///          collection changes in regard of the
    ///          selected display.
    /// </summary>
    public class ErrorPanelLayout : 
        UserControl
    {
        private static readonly int PANEL_LEFT = 0;
        private static readonly int PANEL_RIGHT = 1;
        public static readonly int TOOLBAR_HEIGHT = 26;

        private InternalSplitter _header;
        private Panel _contentDefault;
        private Control _contentCurrent;

        public ErrorPanelLayout()
        {
            _header = new InternalSplitter();
            _contentDefault = new Panel();
            _contentCurrent = _contentDefault;

            Controls.Add(_header[PANEL_LEFT]);
            //Controls.Add(_header[PANEL_RIGHT]);
            Controls.Add(_contentDefault);

            //_header[PANEL_LEFT].BackColor = Color.Yellow;
            //_header[PANEL_RIGHT].BackColor = Color.Violet;
            //_contentDefault.BackColor = Color.Green;

            SizeChanged += new EventHandler(ErrorPanelLayout_SizeChanged);

            _header[PANEL_RIGHT].ControlAdded += new ControlEventHandler(ErrorPanelLayout_ControlAddedOrRemoved);

            _header[PANEL_RIGHT].ControlRemoved += new ControlEventHandler(ErrorPanelLayout_ControlAddedOrRemoved);

            Width = 200;
            Height = 200;

            return;
        }

        void ErrorPanelLayout_ControlAddedOrRemoved(object sender, ControlEventArgs e)
        {
            doLayout();
        }

        void ErrorPanelLayout_SizeChanged(object sender, EventArgs e)
        {
            doLayout();
        }

        /// <summary>
        /// Gets or sets the control to be placed in Toolbar location.
        /// Pass null to reset Toolbar to its default state.
        /// 
        /// When setting a control, the control's hierarchy of
        /// ErrorPanelLayout is automatically updated with the
        /// passed component. Besides, the passed component is
        /// automatically positionned to the right coordinates.
        /// </summary>
        public Control Toolbar
        {
            get { return (_header[PANEL_LEFT]); }
            set {                                
                Controls.Remove(_header[PANEL_LEFT]);
                _header[PANEL_LEFT] = value;
                Controls.Add(_header[PANEL_LEFT]);
                doLayout();
            }
        }

        /// <summary>
        /// Gets or sets the control to be placed in Option location.
        /// Pass null to reset Option to its default state.
        /// 
        /// When setting a control, the control's hierarchy of
        /// ErrorPanelLayout is automatically updated with the
        /// passed component. Besides, the passed component is
        /// automatically positionned to the right coordinates.
        /// </summary>
        public Control Option
        {
            get { return (_header[PANEL_RIGHT]); }
            set {
                Controls.Remove(_header[PANEL_RIGHT]);
                _header[PANEL_RIGHT] = value;
                Controls.Add(_header[PANEL_RIGHT]);
                doLayout();
            }
        }

        /// <summary>
        /// Gets or sets the control to be placed in Content location.
        /// Pass null to reset content to its default state.
        /// 
        /// When setting a control, the control's hierarchy of
        /// ErrorPanelLayout is automatically updated with the
        /// passed component. Besides, the passed component is
        /// automatically positionned to the right coordinates.
        /// </summary>
        public Control Content
        {
            get { return (_contentCurrent); }
            set {
                if (value == null)
                    value = _contentDefault;
                Controls.Remove(_contentCurrent);
                _contentCurrent = value;
                Controls.Add(_contentCurrent);
                doLayout();
            }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    e.Graphics.DrawImage(Resources.ErrorBrowserHeader,
        //        new Rectangle(0, 0, Width, TOOLBAR_HEIGHT),
        //        new Rectangle(0, 0, Resources.ErrorBrowserHeader.Width - 1,
        //            Resources.ErrorBrowserHeader.Height),
        //        GraphicsUnit.Pixel);

        //    return;
        //}

        private void doLayout()
        {
//            int widthLeft;
            int widthRight;

            widthRight = _header.WidthAt(PANEL_RIGHT);
//            widthLeft = _header.WidthAt(PANEL_LEFT);

            _header[PANEL_LEFT].Width = Math.Max(0, Width - widthRight);
            _contentCurrent.Width = Width;

            _header[PANEL_LEFT].Height = TOOLBAR_HEIGHT;
            _header[PANEL_RIGHT].Height = Math.Min(TOOLBAR_HEIGHT, _header[PANEL_RIGHT].Height);
            _header[PANEL_RIGHT].Width = widthRight;
            _header[PANEL_RIGHT].Left = _header[PANEL_LEFT].Width;

            _contentCurrent.Height = Height - TOOLBAR_HEIGHT;
            _contentCurrent.Top = TOOLBAR_HEIGHT;

            return;
        }

        class InternalSplitter : UserControl
        {
            private Panel[] _panels;
            private Control[] _currents;

            public InternalSplitter()
            {
                _panels = new Panel[] { new Panel(), new Panel() };
                _currents = new Control[] { _panels[0], _panels[1] };

                _panels[0].Width = 0;
                _panels[1].Width = 0;

                return;
            }

            public Control this[int index]
            {
                get { return (_currents[index]); }
                set {
                    if (value == null)
                        value = _panels[index];
                    _currents[index] = value; 
                }
            }

            public int WidthAt(int index)
            {
                if (_currents[index] == _panels[index])
                    return (0);
                return (_currents[index].Width);
            }
        }
    }
}
