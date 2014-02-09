// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace NUnit.UiException
{
    /// <summary>
    /// This is a default implementation of ITextManager interface.
    /// </summary>
    public class DefaultTextManager :
        IEnumerable,
        ITextManager
    {
        /// <summary>
        /// Hold the text to be managed by this instance.
        /// </summary>
        private string _text;

        /// <summary>
        /// Array of strings where each string is a line in this text.
        /// </summary>
        private List<string> _lines;

        /// <summary>
        /// Stores the character count of the longest line in this text.
        /// </summary>
        private int _maxLength;

        /// <summary>
        /// Builds a new instance of TextManager.
        /// </summary>
        public DefaultTextManager()
        {
            _lines = new List<string>();
            Text = "";

            return;
        }

        /// <summary>
        /// Gets the number of lines in the text.
        /// </summary>
        public int LineCount
        {
            get { return (_lines.Count); }
        }

        /// <summary>
        /// Gets or sets the text to be managed by this object.
        /// </summary>
        public string Text
        {
            get { return (_text); }
            set
            {
                if (value == null)
                    value = "";

                _text = value;
                _populateLineCollection(value);
            }
        }

        /// <summary>
        /// Gets the line of text at the specified startingPosition.
        /// (zero based startingPosition).
        /// </summary>
        /// <param name="lineIndex">The startingPosition of the line to get.</param>
        /// <returns>A string that represents the content of the specified line without
        /// the trailing characters.</returns>
        public string GetTextAt(int lineIndex)
        {
            return (_lines[lineIndex]);
        }

        /// <summary>
        /// Gets the character count of the longest line in this text.
        /// </summary>
        public int MaxLength
        {
            get { return (_maxLength); }
        }

        #region private definitions

        /// <summary>
        /// setup member data with the input text.
        /// </summary>
        private void _populateLineCollection(string text)
        {
            string line;
            int textIndex;
            int newIndex;

            textIndex = 0;

            _lines.Clear();
            _maxLength = 0;

            while ((newIndex = text.IndexOf("\n", textIndex, StringComparison.Ordinal)) > textIndex)
            {
                line = text.Substring(textIndex, newIndex - textIndex).TrimEnd();
                _maxLength = Math.Max(_maxLength, line.Length);

                _lines.Add(line);
                textIndex = newIndex + 1;
            }

            if (textIndex < text.Length)
            {
                line = text.Substring(textIndex).TrimEnd();
                _maxLength = Math.Max(_maxLength, line.Length);
                _lines.Add(line);
            }

            return;
        }

        #endregion

        #region IEnumerable Membres

        /// <summary>
        /// Gets an IEnumerator that iterate through each line of the
        /// current text. 
        /// </summary>
        /// <returns>An IEnumerator that iterate through each line of this text.</returns>
        public IEnumerator GetEnumerator()
        {
            return (_lines.GetEnumerator());
        }

        #endregion      
    }
}
