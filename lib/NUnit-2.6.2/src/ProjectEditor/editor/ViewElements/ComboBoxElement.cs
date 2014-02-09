// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor
{
    /// <summary>
    /// ComboBoxElement is used to wrap a ComboBox. If the
    /// text is editable by the user, the view should expose
    /// the element using the IComboBox interface. Otherwise,
    /// the ISelectionInterface provides all the needed
    /// functionality.
    /// </summary>
    public class ComboBoxElement : ControlElement, ISelectionList, IComboBox
    {
        private ComboBox comboBox;

        public ComboBoxElement(ComboBox comboBox)
            : base(comboBox)
        {
            this.comboBox = comboBox;

            comboBox.SelectedIndexChanged += delegate
            {
                if (SelectionChanged != null)
                    SelectionChanged();
            };

            comboBox.Validated += delegate
            {
                if (TextValidated != null)
                    TextValidated();
            };
        }

        /// <summary>
        /// Gets or sets the SelectedIndex property of the associated ComboBox
        /// </summary>
        public int SelectedIndex
        {
            get { return comboBox.SelectedIndex; }
            set { comboBox.SelectedIndex = value; }
        }

        /// <summary>
        /// Gets or sets the SelectedItem property of the associated ComboBox
        /// </summary>
        public string SelectedItem
        {
            get { return (string)comboBox.SelectedItem; }
            set { comboBox.SelectedItem = value; }
        }

        /// <summary>
        /// Gets or sets the list of items displayed in the associated ComboBox
        /// </summary>
        public string[] SelectionList
        {
            get
            {
                string[] list = new string[comboBox.Items.Count];
                    
                int index = 0;
                foreach (string item in comboBox.Items)
                    list[index++] = item;

                return list;
            }
            set
            {
                comboBox.Items.Clear();
                foreach (string item in value)
                    comboBox.Items.Add(item);
            }
        }

        /// <summary>
        /// Event raised when the selection in the associated ComboBox changes
        /// </summary>
        public event ActionDelegate SelectionChanged;

        /// <summary>
        /// Event raised when the Text of the associated ComboBox is validated
        /// </summary>
        public event ActionDelegate TextValidated;
    }
}
