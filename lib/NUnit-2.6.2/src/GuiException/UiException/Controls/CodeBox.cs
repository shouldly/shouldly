// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NUnit.UiException.CodeFormatters;
using System.Diagnostics;

/// This control could have been replaced by a standard RichTextBox control, but
/// it turned out that RichTextBox:
///     - was hard to configure
///     - was hard to set the viewport
///     - doesn't use double buffer optimization
///     - scrolls text one line at a time without be configurable.
/// 
/// CodeBox has been written to address these specific issues in order to display
/// C# source code where exceptions occured.

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// A control that implements ICodeView.
    /// </summary>
    public class CodeBox : UserControl, ICodeView
    {
        protected CodeRenderingContext _workingContext;
        protected FormattedCode _formattedCode;

        private IFormatterCatalog _formatter;
        private ICodeRenderer _renderer;
        private string _language;

        private bool _showCurrentLine;
        private int _currentLine;
        
        public CodeBox() :
            this(new GeneralCodeFormatter(), new DefaultCodeRenderer()) { }

        #region ICodeView Members

        public override string Text
        {
            get { return (_formattedCode.Text); }
            set
            {
                if (value == null)
                    value = "";

                SizeF docSize;

                _formattedCode = _formatter.Format(value, _language);
                docSize = _renderer.GetDocumentSize(
                    _formattedCode, _workingContext.Graphics, _workingContext.Font);
                AutoScrollMinSize = new Size((int)docSize.Width, (int)docSize.Height);

                Invalidate();

                return;
            }
        }

        public string Language
        {
            get { return (_language); }
            set
            {
                if (value == null)
                    value = "";
                if (_language == value)
                    return;

                _language = value;
                Text = Text;
            }
        }

        public int CurrentLine
        {
            get { return (_currentLine); }
            set
            {
                float y = _renderer.LineIndexToYCoordinate(value,
                    _workingContext.Graphics, _workingContext.Font);

                y -= Height / 2;

                _currentLine = value;
                AutoScrollPosition = new Point(0, (int)y);

                Invalidate();
            }
        }

        public IFormatterCatalog Formatter
        {
            get { return (_formatter); }
        }

        #endregion

        /// <summary>
        /// Gets or sets a value telling whether or not displaying a special
        /// feature for the current line at drawing time.
        /// </summary>        
        public bool ShowCurrentLine
        {
            get { return (_showCurrentLine); }
            set { _showCurrentLine = value; }
        }

        /// <summary>
        /// If ShowCurrentLine is set, this set the current line's background color.
        /// </summary>
        public Color CurrentLineBackColor
        {
            get { return (_workingContext.CurrentLineBackColor); }
            set { 
                _workingContext.CurrentLineBackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// If ShowCurrentLine is set, this set current line's foreground color.
        /// </summary>
        public Color CurrentLineForeColor
        {
            get { return (_workingContext.CurrentLineForeColor); }
            set { 
                _workingContext.CurrentLineForeColor = value;
                Invalidate();
            }
        }
       
        protected CodeBox(IFormatterCatalog formatter, ICodeRenderer renderer)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            _formatter = formatter;
            _formattedCode = FormattedCode.Empty;

            _renderer = renderer;

            _currentLine = -1;
            _showCurrentLine = false;

            _language = "";

            this.Font = new Font(FontFamily.GenericMonospace, 8);
            this.BackColor = Color.White;

            createGraphics();
            AutoScroll = true;

            return;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Focus();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics backup;

            base.OnPaint(e);

            backup = _workingContext.Graphics;
            _workingContext.Graphics = e.Graphics;
            _workingContext.CurrentLine = (_showCurrentLine ? _currentLine : -1);

            _renderer.DrawToGraphics(_formattedCode, _workingContext,
                new Rectangle(-AutoScrollPosition.X, -AutoScrollPosition.Y, Width, Height));

            _workingContext.Graphics = backup;

            return;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            if (_workingContext != null)
            {
                _workingContext.Font = Font;
                Text = Text;
            }

            return;
        }

        private void createGraphics()
        {
            Graphics gCurrent = CreateGraphics();
            Image img = new Bitmap(10, 10, gCurrent);
            Graphics gImg = Graphics.FromImage(img);

            gCurrent.Dispose();

            _workingContext = new CodeRenderingContext();
            _workingContext.Graphics = gImg;
            _workingContext.Font = Font;

            _workingContext.CurrentLine = -1;
            _workingContext.BackgroundColor = Color.White;
            _workingContext.CurrentLineBackColor = Color.Red;
            _workingContext.CurrentLineForeColor = Color.White;
            _workingContext.CodeColor = Color.Black;
            _workingContext.CommentColor = Color.Green;
            _workingContext.KeywordColor = Color.Blue;
            _workingContext.StringColor = Color.Red;

            return;
        }
    }    
}
