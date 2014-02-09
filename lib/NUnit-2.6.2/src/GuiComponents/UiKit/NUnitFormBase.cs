// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;

namespace NUnit.UiKit
{
    public class NUnitFormBase : Form
    {
        private IMessageDisplay messageDisplay;
        private string caption;

        public NUnitFormBase() { }

        public NUnitFormBase(string caption)
        {
            this.caption = caption;
        }

        public IMessageDisplay MessageDisplay
        {
            get
            {
                if (messageDisplay == null)
                    messageDisplay = new MessageDisplay(caption == null ? Text : caption);

                return messageDisplay;
            }
        }
    }
}
