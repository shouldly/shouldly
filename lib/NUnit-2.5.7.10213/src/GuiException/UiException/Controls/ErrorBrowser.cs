// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// A control that encapsulates a collection of IErrorDisplay instances
    /// and which shows relevant information about failures & errors after
    /// a test suite run.
    ///     By default, ErrorBrowser is empty and should be populated with
    /// IErrorDisplay instances at loading time. The example below shows how
    /// to achieve this:
    /// <code>
    /// ErrorBrowser errorBrowser = new ErrorBrowser();
    /// 
    /// // configure and register a SourceCodeDisplay
    /// // that will display source code context on failure
    /// 
    /// SourceCodeDisplay sourceCode = new SourceCodeDisplay();
    /// sourceCode.AutoSelectFirstItem = true;
    /// sourceCode.ListOrderPolicy = ErrorListOrderPolicy.ReverseOrder;
    /// sourceCode.SplitOrientation = Orientation.Vertical;
    /// sourceCode.SplitterDistance = 0.3f;
    ///
    /// errorBrowser.RegisterDisplay(sourceCode);
    /// 
    /// // configure and register a StackTraceDisplay
    /// // that will display the stack trace details on failure
    /// 
    /// errorBrowser.RegisterDisplay(new StackTraceDisplay());
    /// [...]
    /// // set the stack trace information
    /// errorBrowser.StackTraceSource = [a stack trace here]
    /// </code>
    /// </summary>
    public class ErrorBrowser : UserControl
    {
        public event EventHandler StackTraceSourceChanged;
        public event EventHandler StackTraceDisplayChanged;
        private ErrorPanelLayout _layout;

        private string _stackStace;

        /// <summary>
        /// Builds a new instance of ErrorBrowser.
        /// </summary>
        public ErrorBrowser()
        {            
            _layout = new ErrorPanelLayout();
            _layout.Toolbar = new ErrorToolbar();            
            Toolbar.SelectedRendererChanged += new EventHandler(Toolbar_SelectedRendererChanged);

            Controls.Add(_layout);
            _layout.Left = 0;
            _layout.Top = 0;
            _layout.Width = Width;
            _layout.Height = Height;

            _layout.Anchor = AnchorStyles.Top |
                             AnchorStyles.Left |
                             AnchorStyles.Bottom |
                             AnchorStyles.Right;

            return;
        }        

        /// <summary>
        /// Use this property to get or set the new stack trace details.
        /// The changes are repercuted on the registered displays.
        /// </summary>
        public string StackTraceSource
        {
            get { return (_stackStace); }
            set {
                if (_stackStace == value)
                    return;

                _stackStace = value;

                foreach (IErrorDisplay item in Toolbar)
                    item.OnStackTraceChanged(value);

                if (StackTraceSourceChanged != null)
                    StackTraceSourceChanged(this, new EventArgs());

                return;
            }
        }

        /// <summary>
        /// Gets the selected display.
        /// </summary>
        public IErrorDisplay SelectedDisplay
        {
            get { return (Toolbar.SelectedDisplay); }
            set { Toolbar.SelectedDisplay = value; }
        }

        /// <summary>
        /// Populates ErrorBrowser with the new display passed in parameter.
        /// If ErrorBrowser is empty, the display becomes automatically the
        /// new selected display.
        /// </summary>
        /// <param name="display"></param>
        public void RegisterDisplay(IErrorDisplay display)
        {
            UiExceptionHelper.CheckNotNull(display, "display");

            Toolbar.Register(display);
            display.OnStackTraceChanged(_stackStace);

            if (Toolbar.SelectedDisplay == null)
                Toolbar.SelectedDisplay = display;

            return;
        }

        /// <summary>
        /// Removes all display from ErrorBrowser.
        /// </summary>
        public void ClearAll()
        {
            Toolbar.Clear();

            LayoutPanel.Option = null;
            LayoutPanel.Content = null;

            return;
        }

        void Toolbar_SelectedRendererChanged(object sender, EventArgs e)
        {
            LayoutPanel.Content = Toolbar.SelectedDisplay.Content;

            if (StackTraceDisplayChanged != null)
                StackTraceDisplayChanged(this, EventArgs.Empty);

            return;
        }

        protected ErrorPanelLayout LayoutPanel
        {
            get { return (_layout); }
        }

        protected ErrorToolbar Toolbar
        {
            get { return ((ErrorToolbar)_layout.Toolbar); }
        }
    }
}
