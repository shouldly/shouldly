// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.UiException.CodeFormatters;

namespace NUnit.UiException.CodeFormatters
{
    /// <summary>
    /// Create a default FormattedCode for any string value.
    /// This should be used as a last resort when there is not ICodeFormatter
    /// that fit source code for an ErrorItem.
    /// </summary>
    public class PlainTextCodeFormatter :
        ICodeFormatter
    {
        #region ICodeFormatter Membres

        /// <summary>
        /// Returns "Plain text"
        /// </summary>
        public string Language
        {
            get { return ("Plain text"); }
        }

        public FormattedCode Format(string sourceCode)
        {
            DefaultTextManager text;
            byte[] byteArray;
            int[] strArray;
            int[] lineArray;
            int index;
            int posLineStart;
            int posChar;

            UiExceptionHelper.CheckNotNull(sourceCode, "sourceCode");

            sourceCode = PreProcess(sourceCode);

            text = new DefaultTextManager();
            text.Text = sourceCode;

            strArray = new int[text.LineCount];
            lineArray = new int[text.LineCount];

            index = 0;
            posLineStart = 0;
            posChar = 0;

            foreach (char c in sourceCode)
            {
                if (c == '\n')
                {
                    strArray[index] = posLineStart;
                    lineArray[index] = index;
                    posLineStart = posChar + 1;
                    index++;
                }

                posChar++;
            }

            if (index <= text.LineCount - 1)
            {
                strArray[index] = posLineStart;
                lineArray[index] = index;
            }

            byteArray = new byte[text.LineCount];

            return (new FormattedCode(sourceCode, strArray, byteArray, lineArray));
        }

        #endregion

        /// <summary>
        /// Prepare input text for the parsing stage.
        /// </summary>
        /// <param name="text">The text to be pre-processed.</param>
        /// <returns>A string ready to be parsed.</returns>
        public string PreProcess(string text)
        {
            if (text == null)
                return (null);

            // this replace tabulation by space sequences. The reason is
            // that the technique used to measure strings at rendering time
            // fail to measure '\t' correctly and lines of text containing those
            // characters are badly aligned.
            //  The simplest thing to fix that is to remove tabs altogether.

            return (text.Replace("\t", "    "));
        }
    }
}
