// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using NUnit.UiException.Properties;
using NUnit.UiException.CodeFormatters;

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// Implements IErrorDisplay and displays data about failures and error
    /// after a test suite run. SourceCodeDisplay is a control composed of two
    /// views. 
    /// 
    /// The first view displays the stack trace in an ordered list of items
    /// where each item contains the context about a specific failure (file, class
    /// method, line number).
    /// 
    /// The second view displays a CodeBox control and shows the source code
    /// of one element in this list when the localization is available.
    /// </summary>
    public class SourceCodeDisplay :
        IErrorDisplay
    {
        protected IStackTraceView _stacktraceView;
        protected ICodeView _codeView;
        protected SplitterBox _splitter;
        private CodeBox _codeBox;

        private ToolStripButton _btnPlugin;
        private ToolStripButton _btnSwap;

        public event EventHandler SplitOrientationChanged;
        public event EventHandler SplitterDistanceChanged;

        /// <summary>
        /// Builds a new instance of SourceCodeDisplay.
        /// </summary>
        public SourceCodeDisplay()
        {
            ErrorList errorList = new ErrorList();
            _codeBox = new CodeBox();

            _stacktraceView = errorList;
            _stacktraceView.AutoSelectFirstItem = true;
            _stacktraceView.SelectedItemChanged += new EventHandler(SelectedItemChanged);
            _codeView = _codeBox;

            _btnPlugin = ErrorToolbar.NewStripButton(true, "Display source code context", Resources.ImageSourceCodeDisplay, null);
            _btnSwap = ErrorToolbar.NewStripButton(false, "ReverseOrder item order", Resources.ImageReverseItemOrder, OnClick);

            SplitterBox splitter = new SplitterBox();
            _splitter = splitter;
            _splitter.SplitterDistanceChanged += new EventHandler(_splitter_DistanceChanged);
            _splitter.OrientationChanged += new EventHandler(_splitter_OrientationChanged);

            splitter.Control1 = errorList;
            splitter.Control2 = _codeBox;

            _codeBox.ShowCurrentLine = true;

            return;
        }

        void _splitter_DistanceChanged(object sender, EventArgs e)
        {
            if (SplitterDistanceChanged != null)
                SplitterDistanceChanged(sender, e);
        }

        void _splitter_OrientationChanged(object sender, EventArgs e)
        {
            if (SplitOrientationChanged != null)
                SplitOrientationChanged(sender, e);
        }

        public Font CodeDisplayFont
        {
            get { return _codeBox.Font; }
            set { _codeBox.Font = value; }
        }

        /// <summary>
        /// Gets or sets a value telling whether or not to select automatically
        /// the first localizable item each time the stack trace changes.
        ///   When set to true, the first localizable item will be selected 
        /// and the source code context for this item displayed automatically.
        /// Default is True.
        /// </summary>
        public bool AutoSelectFirstItem
        {
            get { return (_stacktraceView.AutoSelectFirstItem); }
            set { _stacktraceView.AutoSelectFirstItem = value; }
        }

        /// <summary>
        /// Gets or sets a value defining the order of the item in the error list.
        /// </summary>
        public ErrorListOrderPolicy ListOrderPolicy
        {
            get { return (_stacktraceView.ListOrderPolicy); }
            set { _stacktraceView.ListOrderPolicy = value; }
        }

        /// <summary>
        /// Gets or sets the splitter orientation in the SourceCodeDisplay.
        /// </summary>
        public Orientation SplitOrientation
        {
            get { return (_splitter.Orientation); }
            set { _splitter.Orientation = value; }
        }

        /// <summary>
        /// Gets or sets the splitter distance in the SourceCodeDisplay.
        /// </summary>
        public float SplitterDistance
        {
            get { return (_splitter.SplitterDistance); }
            set { _splitter.SplitterDistance = value; }
        }

        private void OnClick(object sender, EventArgs e)
        {
            ListOrderPolicy = ListOrderPolicy == ErrorListOrderPolicy.InitialOrder ?
                ErrorListOrderPolicy.ReverseOrder :
                ErrorListOrderPolicy.InitialOrder;

            return;
        }

        protected void SelectedItemChanged(object sender, EventArgs e)
        {
            ErrorItem item;
            IFormatterCatalog formatter;

            item = _stacktraceView.SelectedItem;

            if (item == null)
            {
                _codeView.Text = null;
                return;
            }

            formatter = _codeView.Formatter;
            _codeView.Language = formatter.LanguageFromExtension(item.FileExtension);

            try
            {
                _codeView.Text = item.ReadFile();
            }
            catch (Exception ex)
            {
                _codeView.Text = String.Format(
                    "Cannot open file: '{0}'\r\nError: '{1}'\r\n",
                    item.Path, ex.Message);
            }

            _codeView.CurrentLine = item.LineNumber - 1;

            return;
        }

        #region IErrorDisplay Membres

        public ToolStripButton PluginItem
        {
            get { return (_btnPlugin); }
        }

        public ToolStripItem[] OptionItems
        {
            get { return (new ToolStripItem[] { _btnSwap }); }
        }             

        public Control Content
        {
            get { return (_splitter); }
        }

        public void OnStackTraceChanged(string stackTrace)
        {
            _stacktraceView.StackTrace = stackTrace;
        }

        #endregion        
    }
}
