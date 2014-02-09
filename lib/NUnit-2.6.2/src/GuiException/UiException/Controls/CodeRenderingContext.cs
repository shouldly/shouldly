// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using NUnit.UiException.CodeFormatters;

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// Encapsulates basic colors settings to format a text according a language.
    /// </summary>
    public class CodeRenderingContext
    {
        private static readonly int INDEX_CODE = 0;
        private static readonly int INDEX_KEYWORD = 1;
        private static readonly int INDEX_COMMENT = 2;
        private static readonly int INDEX_STRING = 3;
        private static readonly int INDEX_BACKGROUND = 4;
        private static readonly int INDEX_CURRBACK = 5;
        private static readonly int INDEX_CURRFORE = 6;

        private Graphics _graphics;
        private Font _font;

        private int _currentLine;

        private ColorMaterial[] _colors;

        public CodeRenderingContext()
        {
            _colors = new ColorMaterial[] 
            {
                new ColorMaterial(Color.Black),        // code color
                new ColorMaterial(Color.Blue),         // keyword color
                new ColorMaterial(Color.Green),        // comment color
                new ColorMaterial(Color.Red),          // string color

                new ColorMaterial(Color.White),        // background
                new ColorMaterial(Color.Red),          // current line back color
                new ColorMaterial(Color.White),        // current line fore color                
            };

            return;
        }

        public Graphics Graphics {
            get { return (_graphics); }
            set { _graphics = value; }
        }       

        public Font Font {
            get { return (_font); }
            set { _font = value; }
        }

        public int CurrentLine {
            get { return (_currentLine); }
            set { _currentLine = value; }
        }

        public Color BackgroundColor {
            get { return (_colors[INDEX_BACKGROUND].Color); }
            set
            {
                _colors[INDEX_BACKGROUND].Dispose();
                _colors[INDEX_BACKGROUND] = new ColorMaterial(value);
            }
        }

        public Color CurrentLineBackColor {
            get { return (_colors[INDEX_CURRBACK].Color); }
            set {
                _colors[INDEX_CURRBACK].Dispose();
                _colors[INDEX_CURRBACK] = new ColorMaterial(value);
            }
        }

        public Color CurrentLineForeColor {
            get { return (_colors[INDEX_CURRFORE].Color); }
            set {
                _colors[INDEX_CURRFORE].Dispose();
                _colors[INDEX_CURRFORE] = new ColorMaterial(value);
            }
        }

        public Color KeywordColor {
            get { return (_colors[INDEX_KEYWORD].Color); }
            set {
                _colors[INDEX_KEYWORD].Dispose();
                _colors[INDEX_KEYWORD] = new ColorMaterial(value);
            }
        }

        public Color CommentColor {
            get { return (_colors[INDEX_COMMENT].Color); }
            set {
                _colors[INDEX_COMMENT].Dispose();
                _colors[INDEX_COMMENT] = new ColorMaterial(value);
            }
        }

        public Color CodeColor {
            get { return (_colors[INDEX_CODE].Color); }
            set {
                _colors[INDEX_CODE].Dispose();
                _colors[INDEX_CODE] = new ColorMaterial(value);
            }
        }

        public Color StringColor {
            get { return (_colors[INDEX_STRING].Color); }
            set {
                _colors[INDEX_STRING].Dispose();
                _colors[INDEX_STRING] = new ColorMaterial(value);
            }
        }

        public Color this[ClassificationTag tag]
        {
            get
            {
                int idx = (int)tag;
                return (_colors[idx].Color);
            }
        }    

        public Brush GetBrush(ClassificationTag tag) {
            return (_colors[(int)tag].Brush);
        }
      
        public Pen GetPen(ClassificationTag tag) {
            return (_colors[(int)tag].Pen);
        }

        #region Brushes

        public Brush BackgroundBrush {
            get { return (_colors[INDEX_BACKGROUND].Brush); }
        }

        public Brush CurrentLineBackBrush {
            get { return (_colors[INDEX_CURRBACK].Brush); }
        }

        public Brush CurrentLineForeBrush {
            get { return (_colors[INDEX_CURRFORE].Brush); }
        }

        public Brush KeywordBrush {
            get { return (_colors[INDEX_KEYWORD].Brush); }
        }

        public Brush CommentBrush {
            get { return (_colors[INDEX_COMMENT].Brush); }
        }

        public Brush CodeBrush {
            get { return (_colors[INDEX_CODE].Brush); }
        }

        public Brush StringBrush {
            get { return (_colors[INDEX_STRING].Brush); }
        }

        #endregion

        #region Pens

        public Pen BackgroundPen {
            get { return (_colors[INDEX_BACKGROUND].Pen); }
        }

        public Pen CurrentLineBackPen {
            get { return (_colors[INDEX_CURRBACK].Pen); }
        }

        public Pen CurrentLineForePen {
            get { return (_colors[INDEX_CURRFORE].Pen); }
        }

        public Pen KeywordPen {
            get { return (_colors[INDEX_KEYWORD].Pen); }
        }

        public Pen CommentPen {
            get { return (_colors[INDEX_COMMENT].Pen); }
        }

        public Pen CodePen {
            get { return (_colors[INDEX_CODE].Pen); }
        }

        public Pen StringPen {
            get { return (_colors[INDEX_STRING].Pen); }
        }

        #endregion

        class ColorMaterial
        {
            public Color Color;
            public Brush Brush;
            public Pen Pen;

            public ColorMaterial(Color color)
            {
                this.Color = color;
                this.Brush = new SolidBrush(color);
                this.Pen = new Pen(color);

                return;
            }

            public void Dispose()
            {
                this.Brush.Dispose();
                this.Pen.Dispose();
            }
        }
    }
}
