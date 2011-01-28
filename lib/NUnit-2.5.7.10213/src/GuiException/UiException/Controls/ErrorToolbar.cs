// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
using NUnit.UiException.Properties;

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// A specialization of a ToolStrip to show instances of IErrorDisplay.
    /// </summary>
    public class ErrorToolbar :
        ToolStrip,
        IEnumerable
    {
        public event EventHandler SelectedRendererChanged;

        private List<IErrorDisplay> _displays;

        private ToolStripItem _separator;
        private int _selection;

        public ErrorToolbar()
        {
            _displays = new List<IErrorDisplay>();

            _separator = CreateDefaultItem("-", null, null);
            Items.Add(_separator);

            _selection = -1;
            
            BackgroundImage = Resources.ImageErrorBrowserHeader;
            BackgroundImageLayout = ImageLayout.Tile;

            return;
        }

        /// <summary>
        /// Create and configure a ToolStripButton.
        /// </summary>
        public static ToolStripButton NewStripButton(
            bool canCheck, string text, Image image, EventHandler onClick)
        {
            ToolStripButton button;

            button = new ToolStripButton(text, image, onClick);
            button.CheckOnClick = canCheck;
            button.Image = image;
            button.ImageScaling = ToolStripItemImageScaling.None;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.DisplayStyle = ToolStripItemDisplayStyle.Image;

            return (button);
        }

        /// <summary>
        /// Gets the count of IErrorDisplay instances.
        /// </summary>
        public int Count
        {
            get { return (_displays.Count); }
        }

        /// <summary>
        /// Gets the display at the given index.
        /// </summary>
        public IErrorDisplay this[int index]
        {
            get { return (_displays[index]); }
        }

        /// <summary>
        /// Gets or sets the IErrorDisplay to be selected.
        /// </summary>
        public IErrorDisplay SelectedDisplay
        {
            get {
                if (_selection == -1)
                    return (null);
                return ((IErrorDisplay)Items[_selection].Tag); 
            }
            set
            {
                int index = IndexOf(value);

                UiExceptionHelper.CheckFalse(index == -1 && value != null,
                    "Cannot select unregistered display.", "SelectedDisplay");

                if (index == _selection)
                    return;

                _selection = index;
                SetOrUnsetCheckedFlag(_selection);
                ShowOrHideOptionItems(_selection);
                
                if (SelectedRendererChanged != null)
                    SelectedRendererChanged(this, new EventArgs());

                return;
            }
        }

        /// <summary>
        /// Register a new IErrorDisplay in the toolbar.
        /// </summary>
        public void Register(IErrorDisplay display)
        {
            ToolStripItem item;
            int sepIndex;

            UiExceptionHelper.CheckNotNull(display, "display");
            UiExceptionHelper.CheckNotNull(display.PluginItem, "display.PluginItem");

            item = display.PluginItem;
            item.Tag = display;
            item.Click += new EventHandler(item_Click);

            _displays.Add(display);
            sepIndex = Items.IndexOf(_separator);
            Items.Insert(sepIndex, item);

            if (display.OptionItems != null)
            {
                ToolStripItem[] array = display.OptionItems;
                foreach (ToolStripItem value in array)
                {
                    value.Visible = false;
                    Items.Add(value);
                }
            }

            if (_displays.Count == 1)
                SelectedDisplay = display;

            return;
        }

        /// <summary>
        /// Clears all IErrorDisplay in the toolbar.
        /// </summary>
        public void Clear()
        {
            _displays.Clear();
            Items.Clear();
            Items.Add(_separator);

            return;
        }        

        private void ShowOrHideOptionItems(int selectedIndex)
        {
            int index;

            foreach (IErrorDisplay item in _displays)
            {
                if ((index = IndexOf(item)) == -1)                     
                    continue;

                if (item.OptionItems == null)
                    continue;

                foreach (ToolStripItem stripItem in item.OptionItems)
                    stripItem.Visible = (index == selectedIndex);
            }

            return;
        }

        private void SetOrUnsetCheckedFlag(int selectedIndex)
        {
            int index;

            foreach (IErrorDisplay item in _displays)
            {
                index = IndexOf(item);
                if (index == -1)
                    continue;
                item.PluginItem.Checked = (index == selectedIndex);
            }

            return;
        }

        private int IndexOf(IErrorDisplay renderer)
        {
            int i;

            if (renderer == null)
                return (-1);

            for (i = 0; i < Items.Count; ++i)
                if (object.ReferenceEquals(Items[i].Tag, renderer))
                    return (i);

            return (-1);
        }       

        private void item_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            IErrorDisplay renderer;

            if (item == null || item.Tag == null)
                return;

            renderer = item.Tag as IErrorDisplay;
            if (renderer == null)
                return;

            SelectedDisplay = renderer;

            return;
        }                

        #region IEnumerable Membres

        public IEnumerator GetEnumerator()
        {
            return (_displays.GetEnumerator());
        }

        #endregion
    }      
}
