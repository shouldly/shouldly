// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;

namespace NUnit.ProjectEditor.ViewElements
{
    public class RadioButtonGroup : ISelection
    {
        private string name;
        private bool enabled;
        private RadioButton[] buttons;

        public RadioButtonGroup(string name, params RadioButton[] buttons)
        {
            this.name = name;
            this.enabled = buttons.Length > 0 ? buttons[0].Enabled : false;
            this.buttons = buttons;

            foreach (RadioButton button in buttons)
                button.CheckedChanged += delegate
                {
                    if (SelectionChanged != null)
                        SelectionChanged();
                };
        }

        public string Name
        {
            get { return name; }
        }

        public bool Enabled
        {
            get 
            {
                return enabled;
            }
            set
            {
                enabled = value;

                foreach (RadioButton button in buttons)
                    button.Enabled = enabled;
            }
        }

        public int SelectedIndex
        {
            get
            {
                for (int index = 0; index < buttons.Length; index++)
                    if (buttons[index].Checked)
                        return index;

                return -1;
            }
            set 
            { 
                if (value >= 0 && value < buttons.Length)
                    buttons[value].Checked = true; 
            }
        }

        public event ActionDelegate SelectionChanged;
    }
}
