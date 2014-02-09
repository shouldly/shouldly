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
    /// Encapsulate data to draw a line of text.
    /// </summary>
    public class PaintLineLocation
    {
        /// <summary>
        /// Index of the current line.
        /// </summary>
        private int _lineIndex;

        /// <summary>
        /// The string value at this line.
        /// </summary>
        private string _text;

        /// <summary>
        /// A client coordinate from where beginning the drawing.
        /// </summary>
        private PointF _location;        

        /// <summary>
        /// Build a new instance of this object given some data.
        /// </summary>
        /// <param name="lineIndex">Index of the current line.</param>
        /// <param name="text">String value at this line.</param>
        /// <param name="location">Client coordinate where beginning the drawing.</param>
        public PaintLineLocation(int lineIndex, string text, PointF location)
        {
            SetLine(lineIndex);
            SetText(text);
            SetLocation(location);

            return;
        }

        /// <summary>
        /// Index of the current line.
        /// </summary>
        public int LineIndex
        {
            get { return (_lineIndex); }
        }

        /// <summary>
        /// String value at this line.
        /// </summary>
        public string Text
        {
            get { return (_text); }
        }

        /// <summary>
        /// Client coordinate where to beginning the drawing.
        /// </summary>
        public PointF Location
        {
            get { return (_location); }
        }
       
        public override bool Equals(object obj)
        {
            PaintLineLocation line;

            if (obj == null ||
                !(obj is PaintLineLocation))
                return (false);

            line = obj as PaintLineLocation;

            return (line.LineIndex == LineIndex &&
                line.Text == Text &&
                line.Location == Location);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return ("PaintLineLocation: {" + LineIndex + ":[" + Text + "]:(" +
                     Location.X + ", " + Location.Y + ")}");
        }

        #region private definitions

        protected void SetLine(int lineIndex)
        {
            _lineIndex = lineIndex;

            return;
        }

        protected void SetText(string text) 
        {
            UiExceptionHelper.CheckNotNull(text, "text");
            _text = text;
        }

        protected void SetLocation(PointF location) {
            _location = location;
        }

        #endregion
    }
}
