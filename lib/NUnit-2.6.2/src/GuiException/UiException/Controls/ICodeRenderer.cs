// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.UiException.CodeFormatters;
using System.Drawing;

namespace NUnit.UiException.Controls
{   
    /// <summary>
    /// The interface through which CodeBox interacts with a display to display itself.
    /// 
    /// Direct implementation is:
    ///     - DefaultCodeRenderer
    /// </summary>
    public interface ICodeRenderer
    {
        /// <summary>
        /// Draw the given code to be displayed in the actual viewport.
        /// </summary>
        /// <param name="code">The code to draw</param>
        /// <param name="args">Encapsulate graphic information about how to display the code</param>
        /// <param name="viewport">The portion of interest</param>
        void DrawToGraphics(FormattedCode code, CodeRenderingContext args, Rectangle viewport);

        /// <summary>
        /// Measures the code size in pixels.
        /// </summary>
        /// <param name="code">The code to measure</param>
        /// <param name="g">The target graphics object</param>
        /// <param name="font">The font with which displaying the code</param>
        /// <returns>The size in pixels</returns>
        SizeF GetDocumentSize(FormattedCode code, Graphics g, Font font);

        /// <summary>
        /// Converts a line index to its matching Y client coordinate.
        /// </summary>
        /// <param name="lineIndex">The line index to convert</param>
        /// <param name="g">The target graphics object</param>
        /// <param name="font">The font with which displaying the code</param>
        /// <returns>The Y client coordinate</returns>
        float LineIndexToYCoordinate(int lineIndex, Graphics g, Font font);
    }
}
