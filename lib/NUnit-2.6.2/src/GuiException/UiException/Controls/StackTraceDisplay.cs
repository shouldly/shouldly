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
    /// Implements IErrorDisplay to show the actual stack trace in a TextBox control.
    /// </summary>
    public class StackTraceDisplay :
        UserControl,
        IErrorDisplay
    {
        private TextBox _textContent;
        private ToolStripButton _btnPlugin;
        private ToolStripButton _btnCopy;

        /// <summary>
        /// Builds a new instance of StackTraceDisplay.
        /// </summary>
        public StackTraceDisplay()
        {
            _btnPlugin = ErrorToolbar.NewStripButton(true, "Display actual stack trace", Resources.ImageStackTraceDisplay, null);
            _btnCopy = ErrorToolbar.NewStripButton(false, "Copy stack trace to clipboard", Resources.ImageCopyToClipboard, OnClick);

            _textContent = new TextBox();
            _textContent.ReadOnly = true;
            _textContent.Multiline = true;
            _textContent.ScrollBars = ScrollBars.Both;

           return;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            _textContent.Font = this.Font;

            base.OnFontChanged(e);
        }

        /// <summary>
        /// Copies the actual stack trace to the clipboard.
        /// </summary>
        public void CopyToClipBoard()
        {
            if (String.IsNullOrEmpty(_textContent.Text))
            {
                Clipboard.Clear();
                return;
            }

            Clipboard.SetText(_textContent.Text);

            return;
        }

        #region IErrorDisplay Membres

        public ToolStripButton PluginItem
        {
            get { return (_btnPlugin); }
        }

        public ToolStripItem[] OptionItems
        {
            get { return (new ToolStripItem[] { _btnCopy }); }
        }
        
        public Control Content
        {
            get { return (_textContent); }
        }

        public void OnStackTraceChanged(string stackTrace)
        {
            _textContent.Text = stackTrace;
        }

        #endregion

        private void OnClick(object sender, EventArgs args)
        {
            CopyToClipBoard();
        }
    }
}
