// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using NUnit.UiException.Properties;
using System.Diagnostics;

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// Implements IErrorListRenderer.
    /// </summary>
    public class DefaultErrorListRenderer :
        IErrorListRenderer
    {
        //private static readonly int ITEM_HEIGHT = 54;
        private static readonly int TEXT_MARGIN_X = 16;

        private Font _font;
        private Font _fontUnderlined;
        private int _itemHeight;
        private Brush _brushBlue;
        private Brush _brushGray;
        private float _offsetLine;
        
        private Rectangle _rectListShadow;
        private Rectangle _rectListBackground;
        private Rectangle _rectItemGray;
        private Rectangle _rectItemWhite;
        private Rectangle _rectSelectionMiddle;
        private Rectangle _rectIconDll;
        private Rectangle _rectIconCSharp;
        private Rectangle _rectIconArrow;
//        private Rectangle _rectShadow;

        private PaintData _paintData;

        public DefaultErrorListRenderer()
        {
            this.Font = new Font(FontFamily.GenericSansSerif, 8.25f);
            //_fontUnderlined = new Font(_font, FontStyle.Underline);
            //_itemHeight = _font.Height * 4 + 6;

            _brushBlue = new SolidBrush(Color.FromArgb(0, 43, 114));
            _brushGray = new SolidBrush(Color.FromArgb(64, 64, 64));

            _rectListShadow = new Rectangle(0, 0, 48, 9);
            _rectListBackground = new Rectangle(0, 10, 48, 48);
            _rectItemGray = new Rectangle(71, 0, 9, 54);
            _rectItemWhite = new Rectangle(60, 0, 9, 54);
            _rectSelectionMiddle = new Rectangle(49, 0, 9, 54);
            _rectIconDll = new Rectangle(1, 59, 16, 15);
            _rectIconCSharp = new Rectangle(18, 59, 14, 15);
            _rectIconArrow = new Rectangle(35, 60, 9, 5);
//            _rectShadow = new Rectangle(49, 60, 4, 8);

            _paintData = new PaintData();

            return;
        }

        public Font Font
        {
            get { return (_font); }
            set 
            { 
                _fontUnderlined = _font = value;
                if (_font.FontFamily.IsStyleAvailable(FontStyle.Underline))
                    _fontUnderlined = new Font(_font, FontStyle.Underline);
                _itemHeight = _font.Height * 4 + 6;
            }
        }

        #region IErrorListRenderer Membres

        public void DrawToGraphics(ErrorItemCollection items,
            ErrorItem selected, Graphics g, Rectangle viewport)
        {
            SizeF sizeLineSource;
            int last;
            int i;

            UiExceptionHelper.CheckNotNull(items, "items");
            UiExceptionHelper.CheckNotNull(g, "g");

            if (!_paintData.Equals(items, selected, viewport))
            {
                _paintData.Dispose();
                _paintData = new PaintData(items, selected, viewport, g);

                PaintBackground(Resources.ImageErrorList, _paintData.WorkingGraphics,
                    _rectListBackground, viewport);

                sizeLineSource = g.MeasureString("Line 9999", _font);
                _offsetLine = viewport.Width - sizeLineSource.Width;

                last = LastIndexVisible(items.Count, viewport);
                for (i = FirstIndexVisible(items.Count, viewport); i <= last; ++i)
                    DrawItem(items[i], i, selected == items[i], i == items.Count - 1, false, 
                        _paintData.WorkingGraphics, viewport);

                //_paintData.WorkingGraphics.DrawImage(Resources.ErrorList,
                //new Rectangle(0, 0, viewport.Width, _rectShadow.Height),
                //_rectShadow, GraphicsUnit.Pixel);
            }
            
            _paintData.PaintTo(g);           

            return;
        }

        public void DrawItem(ErrorItem item, int index, bool hovered, bool selected, Graphics g, Rectangle viewport)
        {
            DrawItem(item, index, selected, false, hovered, g, viewport);
        }

        public Size GetDocumentSize(ErrorItemCollection items, Graphics g)
        {
            SizeF current;
            float w;

            _paintData = new PaintData();

            if (items.Count == 0)
                return (new Size());

            w = 0;
            foreach (ErrorItem item in items)
            {
                current = MeasureItem(g, item);
                w = Math.Max(w, current.Width);
            }            

            return (new Size((int)w, items.Count * _itemHeight));
        }

        public ErrorItem ItemAt(ErrorItemCollection items, Graphics g, Point point)
        {
            int idx = point.Y / _itemHeight;

            if (items == null || point.Y < 0 || idx >= items.Count)
                return (null);

            return (items[idx]);
        }

        #endregion

        protected bool IsDirty(ErrorItemCollection items, ErrorItem selection, Rectangle viewport)
        {
            return (!_paintData.Equals(items, selection, viewport));
        }

        protected SizeF MeasureItem(Graphics g, ErrorItem item)
        {
            SizeF sizeMethod;
            SizeF sizeClass;
            SizeF sizeFile;

            UiExceptionHelper.CheckNotNull(g, "g");
            UiExceptionHelper.CheckNotNull(item, "item");

            sizeClass = g.MeasureString(item.ClassName, _font);
            sizeMethod = g.MeasureString(item.MethodName, _font);
            sizeFile = g.MeasureString(item.FileName, _font);

            return (new SizeF(
                Math.Max(sizeClass.Width, Math.Max(sizeMethod.Width, sizeFile.Width)) + TEXT_MARGIN_X,
                _itemHeight));
        }

        private void DrawItem(ErrorItem item, int index, bool selected, bool last, bool hover, Graphics g, Rectangle viewport)
        {
            Rectangle src;
            Font font;

            int x = -viewport.X;
            int y = _itemHeight * index - viewport.Y;

            src = (index % 2 == 0) ? _rectItemWhite : _rectItemGray ;
            font = (hover == true) ? _fontUnderlined : _font;

            g.DrawImage(Resources.ImageErrorList,
                new Rectangle(0, y, viewport.Width, _itemHeight), src,
                GraphicsUnit.Pixel);

            if (selected)
            {
                g.DrawImage(Resources.ImageErrorList,
                    new Rectangle(0, y + 1, viewport.Width, _itemHeight ),
                    _rectSelectionMiddle, GraphicsUnit.Pixel);
            }

            if (item.HasSourceAttachment)
            {
                g.DrawImage(Resources.ImageErrorList, new Rectangle(x + 1, y + 2 + font.Height, 14, 15),
                   _rectIconCSharp, GraphicsUnit.Pixel);
                g.DrawImage(Resources.ImageErrorList, 
                    new Rectangle(TEXT_MARGIN_X - 3 + x, y + 5 + 2 * font.Height, 9, 5),
                    _rectIconArrow, GraphicsUnit.Pixel);

                g.DrawString(String.Format("Line {0}", item.LineNumber),
                    font, _brushGray, _offsetLine, y + 2);
                g.DrawString(item.ClassName, font, _brushBlue, x + TEXT_MARGIN_X, y + 2 + font.Height);
                g.DrawString(item.BaseMethodName + "()", font, _brushBlue,
                    x + TEXT_MARGIN_X + 5, y + 2 + 2 * font.Height);
                g.DrawString(item.FileName, font, _brushGray,
                    x + TEXT_MARGIN_X, y + 2 + 3 * _font.Height);
            }
            else
            {
                g.DrawImage(Resources.ImageErrorList, new Rectangle(x + 1, y + 2 + font.Height, 16, 15),
                   _rectIconDll, GraphicsUnit.Pixel);

                g.DrawString("N/A", font, _brushGray, _offsetLine, y + 2);
                g.DrawString(item.ClassName, font, _brushGray, 
                    x + TEXT_MARGIN_X, y + 2 + font.Height);
                g.DrawString(item.BaseMethodName + "()", font, _brushGray,
                    x + TEXT_MARGIN_X, y + 2 + 2 * font.Height);                
            }

            if (!last)
                return;

            PaintTile(Resources.ImageErrorList, g, _rectListShadow,
                new Rectangle(0, y + _itemHeight, viewport.Width, 9));

            return;
        }

        private static void PaintBackground(Image img, Graphics g, Rectangle bkg, Rectangle viewport)
        {
            Rectangle destTile;
            int x;
            int y;
            int startY;
            int startX;

            startY = -viewport.Y % viewport.Height;
            startX = -viewport.X % viewport.Width;

            for (y = startY; y < viewport.Height; y += bkg.Height)
                for (x = startX; x < viewport.Width; x += bkg.Width)
                {
                    destTile = new Rectangle(x, y, bkg.Width, bkg.Height);
                    g.DrawImage(img, destTile, bkg, GraphicsUnit.Pixel);
                }

            return;
        }

        private static void PaintTile(Image tile, Graphics g, Rectangle src, Rectangle dst)
        {
            Rectangle destTile;
            int x;
            int y;

            for (y = dst.Top; y < dst.Bottom; y += src.Height)
                for (x = dst.Left; x < dst.Right; x += src.Width)
                {
                    destTile = new Rectangle(x, y, src.Width, src.Height);
                    g.DrawImage(tile, destTile, src, GraphicsUnit.Pixel);
                }

            return;
        }

        private int FirstIndexVisible(int count, Rectangle viewport)
        {
            return (Math.Max(0, viewport.Y / _itemHeight));
        }

        private int LastIndexVisible(int count, Rectangle viewport)
        {
            return (Math.Min(count - 1,
                FirstIndexVisible(count, viewport) + 1 + viewport.Height / _itemHeight));
        }

        class PaintData
        {
            public Graphics WorkingGraphics;

            private ErrorItem _firstItem;
            private ErrorItem selection;
            private Rectangle viewport;
            private Image _workingImage;

            public PaintData() { }

            public PaintData(ErrorItemCollection items, ErrorItem item, Rectangle rectangle, Graphics g)
            {
                if (item == null)
                    item = new ErrorItem();
                selection = item;

                _firstItem = ((items.Count > 0) ? items[0] : null);

                viewport = rectangle;

                _workingImage = new Bitmap(rectangle.Width, rectangle.Height, g);
                WorkingGraphics = Graphics.FromImage(_workingImage);

                return;
            }

            public void Dispose()
            {
                if (_workingImage != null)
                {
                    _workingImage.Dispose();
                    WorkingGraphics.Dispose();
                }

                return;
            }

            public void PaintTo(Graphics g)
            {
                g.DrawImage(_workingImage, 0, 0);
            }

            public bool Equals(ErrorItemCollection items, ErrorItem item, Rectangle rectangle)
            {               
                ErrorItem first = ((items.Count > 0) ? items[0] : null);

                return (viewport.Equals(rectangle) &&
                        object.ReferenceEquals(item, selection) &&
                        object.ReferenceEquals(first, _firstItem));
            }
        }
    }
}
