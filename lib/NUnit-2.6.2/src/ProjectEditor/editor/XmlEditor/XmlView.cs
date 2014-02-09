// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.ComponentModel;
using System.Windows.Forms;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor
{
    public partial class XmlView : UserControl, IXmlView
    {
        #region Instance Variables

        private ITextElement xml;
        private IMessageDisplay messageDisplay;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XmlView()
        {
            InitializeComponent();

            xml = new TextElement(richTextBox1);
            messageDisplay = new MessageDisplay("NUnit Project Editor");
        }

        #endregion

        #region IXmlView Members

        /// <summary>
        /// Gets or sets the XML text
        /// </summary>
        public ITextElement Xml 
        {
            get { return xml; }
        }

        public void DisplayError(string message)
        {
            errorMessageLabel.Visible = true;
            errorMessageLabel.Text = message;

            richTextBox1.Dock = DockStyle.Top;
            richTextBox1.Height = this.ClientSize.Height - errorMessageLabel.Height;
        }

        public void DisplayError(string message, int lineNumber, int linePosition)
        {
            DisplayError(message);

            if (lineNumber > 0 && linePosition > 0)
            {
                int offset = richTextBox1.GetFirstCharIndexFromLine(lineNumber - 1) + linePosition - 1;
                int length = 0;

                string text = richTextBox1.Text;
                if (char.IsLetterOrDigit(text[offset]))
                    while (char.IsLetterOrDigit(text[offset + length]))
                        length++;
                else
                    length = 1;

                richTextBox1.Select(offset, length);
            }
        }

        public void RemoveError()
        {
            errorMessageLabel.Visible = false;
            richTextBox1.Dock = DockStyle.Fill;
        }

        public IMessageDisplay MessageDisplay 
        {
            get { return messageDisplay; }
        }

        #endregion

        #region Event Handlers

        //private void richTextBox1_Validated(object sender, EventArgs e)
        //{
        //    if (XmlChanged != null)
        //        XmlChanged();
        //}

        #endregion
    }
}
