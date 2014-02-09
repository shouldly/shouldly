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
    public class ControlElement : IViewElement
    {
        private Control control;

        public ControlElement(Control control)
        {
            this.control = control;
        }
       
        public string Name
        {
            get { return control.Name; }
        }

        public bool Enabled
        {
            get { return control.Enabled; }
            set { control.Enabled = value; }
        }

        public bool Visible
        {
            get { return control.Visible; }
            set { control.Visible = value; }
        }

        public string Text
        {
            get { return control.Text; }
            set { control.Text = value; }
        }
    }
}
