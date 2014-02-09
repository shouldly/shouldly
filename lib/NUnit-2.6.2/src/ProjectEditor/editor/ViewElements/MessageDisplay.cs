// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;

namespace NUnit.ProjectEditor.ViewElements
{
    public class MessageDisplay : IMessageDisplay
    {
        private string caption;

        public MessageDisplay(string caption)
        {
            this.caption = caption;
        }

        public void Error(string message)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool AskYesNoQuestion(string question)
        {
            return MessageBox.Show(question, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}