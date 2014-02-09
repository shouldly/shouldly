// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// The interface through which ErrorList interacts with a painter to paint itself.
    /// 
    /// Direct implementation is:
    ///     - DefaultErrorListRenderer
    /// </summary>
    public interface IErrorListRenderer
    {
        /// <summary>
        /// Draws the list on the given graphics.
        /// </summary>
        /// <param name="items">The item collection to paint on the graphics object</param>
        /// <param name="selected">The item to paint with selection feature</param>
        /// <param name="g">The target graphics object</param>
        /// <param name="viewport">The viewport location</param>
        void DrawToGraphics(ErrorItemCollection items, ErrorItem selected, Graphics g, Rectangle viewport);

        /// <summary>
        /// Draw the given item on the given graphics object.
        /// </summary>
        /// <param name="item">The item to be painted</param>
        /// <param name="index">The item's index</param>
        /// <param name="hovered">If true, this item can display hover feature</param>
        /// <param name="selected">If true, this item can display selection feature</param>
        /// <param name="g">The target graphics object</param>
        /// <param name="viewport">The current viewport</param>
        void DrawItem(ErrorItem item, int index, bool hovered, bool selected, Graphics g, Rectangle viewport);

        /// <summary>
        /// Given a collection of items and a graphics object, this method
        /// measures in pixels the size of the collection.
        /// </summary>
        /// <param name="items">The collection</param>
        /// <param name="g">The target graphics object</param>
        /// <returns>The size in pixels of the collection</returns>
        Size GetDocumentSize(ErrorItemCollection items, Graphics g);

        /// <summary>
        /// Gets the Item right under point.
        /// </summary>
        /// <param name="items">A collection of items</param>
        /// <param name="g">The target graphics object</param>
        /// <param name="point">Some client coordinate values</param>
        /// <returns>One item in the collection or null the location doesn't match any item</returns>
        ErrorItem ItemAt(ErrorItemCollection items, Graphics g, Point point);

        /// <summary>
        /// Gets and sets the font for this renderer
        /// </summary>
        Font Font { get; set; }
    }
}
