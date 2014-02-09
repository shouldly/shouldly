// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.UiException
{
    /// <summary>
    /// Provides an abstract way to manipulate a text as a whole and as separate
    /// sequences that can randomly be accessed one line at a time.
    /// </summary>
    public interface ITextManager
    {
        /// <summary>
        /// Gets the number of line in text managed by this object.
        /// </summary>
        int LineCount { get; }

        /// <summary>
        /// Gets the character count of the longest line in the text managed
        /// by this object.
        /// </summary>
        int MaxLength { get; }

        /// <summary>
        /// Gets the complete text managed by this object.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets a string filled with all characters in the line
        /// at the specified startingPosition without the trailing '\r\n' characters.
        /// </summary>
        /// <param name="lineIndex"></param>
        /// <returns></returns>
        string GetTextAt(int lineIndex);
    }
}
