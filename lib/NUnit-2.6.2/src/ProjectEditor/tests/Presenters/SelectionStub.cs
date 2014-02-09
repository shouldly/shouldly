// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if NET_3_5 || NET_4_0 || NET_4_5
using System;
using System.Collections.Generic;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor.Tests
{
    public class SelectionStub : ISelectionList, IComboBox
    {
        private int selectedIndex = -1;
        private string[] selectionList;
        private string text;

        public SelectionStub(string name)
        {
            this.Name = name;
        }

        #region IComboBox Members

        public string Text
        {
            get { return text; }
            set 
            { 
                text = value;

                int index = IndexOf(text);
                selectedIndex = index >= 0 ? index : -1;

                if (TextValidated != null)
                    TextValidated();

                if (SelectionChanged != null)
                    SelectionChanged();
            }
        }

        public event ActionDelegate TextValidated;

        #endregion

        #region ISelectionList Members

        /// <summary>
        /// Gets or sets the currently selected item
        /// </summary>
        public string SelectedItem
        {
            get
            {
                return selectedIndex >= 0 && selectedIndex < selectionList.Length
                    ? selectionList[selectedIndex]
                    : null;
            }
            set
            {
                int index = IndexOf(value);
                
                if (index >= 0)
                {
                    text = value;
                    selectedIndex = index;

                    if (TextValidated != null)
                        TextValidated();

                    if (SelectionChanged != null)
                        SelectionChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the contents of the selection list
        /// </summary>
        public string[] SelectionList 
        {
            get { return selectionList; }
            set
            {
                selectionList = value;
                if (selectionList.Length == 0)
                    SelectedIndex = -1;
                else
                    SelectedIndex = 0;
            }
        }

        #endregion

        #region ISelection Members

        /// <summary>
        /// Gets or sets the index of the currently selected item
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set 
            { 
                selectedIndex = value < 0 || value >= SelectionList.Length ? -1 : value;

                if (SelectionChanged != null)
                    SelectionChanged();
            }
        }
        

        /// <summary>
        /// Event raised when the selection is changed by the user
        /// </summary>
        public event ActionDelegate SelectionChanged;

        #endregion

        #region IViewElement Members

        /// <summary>
        /// Gets the name of the element in the xmlView
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the enabled status of the element
        /// </summary>
        public bool Enabled { get; set; }

        #endregion 

        #region Helper Methods

        private int IndexOf(string item)
        {
            for (int index = 0; index < selectionList.Length; index++)
                if (item == selectionList[index])
                    return index;

            return -1;
        }

        #endregion
    }
}
#endif
