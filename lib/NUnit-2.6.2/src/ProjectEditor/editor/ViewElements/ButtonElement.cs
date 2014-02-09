// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;

namespace NUnit.ProjectEditor.ViewElements
{
    /// <summary>
    /// ControlWrapper is a general wrapper for controls used
    /// by the view. It implements several different interfaces
    /// so that the view may choose which one to expose, based
    /// on the type of textBox and how it is used.
    /// </summary>
    public class ButtonElement : ControlElement, ICommand
    {
        public ButtonElement(Button button) : base(button)
        {
            button.Click += delegate
            {
                if (Execute != null)
                    Execute();
            };
        }

        public event CommandDelegate Execute;
    }
}
