using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Collections.Generic;
using System.IO;

namespace TagPortal.Report.Pdf
{
    public abstract class PdfSharpBase
    {
        protected const double PortraitMaxPosX = 21 - LeftRightPadding;
        protected const double PortraitMaxPosY = 29.3 - TopBottomPadding;

        protected const double LandscapeMaxPosX = 29.71 - LeftRightPadding;
        protected const double LandscapeMaxPosY = 20.6 - TopBottomPadding;

        protected const double TopBottomPadding = 1;
        protected const double LeftRightPadding = .5;

        protected int PageNumber;
        protected XGraphics Graphics;
        protected XTextFormatter TextFormatter;
        protected readonly XFont FontTitle = new XFont("Calibri Light", 0.63);
        protected readonly XFont FontLabel = new XFont("Times New Roman", 0.3, XFontStyle.Bold);
        protected readonly XFont FontText = new XFont("Times New Roman", 0.3, XFontStyle.Regular);
        protected readonly XFont FontLabelLarge = new XFont("Times New Roman", 0.35, XFontStyle.Bold);
        protected readonly XFont FontTextLarge = new XFont("Times New Roman", 0.35, XFontStyle.Regular);
        protected readonly XFont FontLabelSmall = new XFont("Times New Roman", 0.27, XFontStyle.Bold);
        protected readonly XFont FontTextSmall = new XFont("Times New Roman", 0.27, XFontStyle.Regular);
        protected byte[] Logo;
        private const double LogoMaxWidth = 3.5;
        protected PageOrientation PageOrientation = PageOrientation.Portrait;

        protected bool DeveloperMode = false;
        protected XPen DefaultPen = new XPen(XColors.Red, .01);

        protected const double GutterX = 0.15;
        protected const double GutterY = 0.48;
        protected double CurrentPosX;
        protected double CurrentPosY;
        protected const double DefaultRectHeight = .4;

        protected List<Layout> TableColumns = new List<Layout>();
        protected List<Layout> HeaderFields = new List<Layout>();
        protected List<Layout> HeaderSummaryFileds = new List<Layout>();
        protected List<Layout> FooterFields = new List<Layout>();

        protected PdfSharpBase()
        {
            PageNumber = 0;
            CurrentPosY = 7.5;
        }

        protected class Line
        {
            public XPen Pen { get; set; }

            public Line(XColor color)
            {
                Pen = new XPen(color, .01);
            }
        }

        protected class Rectangle
        {
            public XPen Pen { get; set; }

            public Rectangle(XColor color)
            {
                Pen = new XPen(color, .01);
            }
        }

        protected class Layout
        {
            public string ColName { get; set; }
            public string Text { get; set; }
            public XParagraphAlignment Alignment { get; set; }
            public XRect Rectangle { get; set; }
            public XFont Font { get; set; }
            public Line LineToDraw { get; set; }
            public Rectangle RectangleToDraw { get; set; }
        }

        /// <param name="font">Default font if not set is PdfSharp.FontText</param>
        protected void AddTableColumn(string colName, string text, XParagraphAlignment alignment, double width, XFont font = null, double height = DefaultRectHeight)
        {
            if (font == null)
                font = FontText;

            TableColumns.Add(new Layout()
            {
                ColName = colName,
                Text = text,
                Alignment = alignment,
                Rectangle = new XRect(CurrentPosX, CurrentPosY, width, height),
                Font = font
            });
        }

        /// <param name="font">Default font if not set is PdfSharp.FontText</param>
        protected void AddHeaderField(string colName, string text, XParagraphAlignment alignment, double width, XFont font = null, double height = DefaultRectHeight, Line lineToDraw = null, Rectangle rectangleToDraw = null)
        {
            if (font == null)
                font = FontText;

            HeaderFields.Add(new Layout()
            {
                ColName = colName,
                Text = text,
                Alignment = alignment,
                Rectangle = new XRect(CurrentPosX, CurrentPosY, width, height),
                Font = font,
                LineToDraw = lineToDraw,
                RectangleToDraw = rectangleToDraw
            });
        }

        /// <param name="font">Default font if not set is PdfSharp.FontText</param>
        protected void AddHeaderSummaryField(string colName, string text, XParagraphAlignment alignment, double width, XFont font = null, double height = DefaultRectHeight, Line lineToDraw = null, Rectangle rectangleToDraw = null)
        {
            if (font == null)
                font = FontText;

            HeaderSummaryFileds.Add(new Layout()
            {
                ColName = colName,
                Text = text,
                Alignment = alignment,
                Rectangle = new XRect(CurrentPosX, CurrentPosY, width, height),
                Font = font,
                LineToDraw = lineToDraw,
                RectangleToDraw = rectangleToDraw
            });
        }

        /// <param name="font">Default font if not set is PdfSharp.FontText</param>
        protected void AddFooterField(string colName, string text, XParagraphAlignment alignment, double width, XFont font = null, double height = DefaultRectHeight, Line lineToDraw = null, Rectangle rectangleToDraw = null)
        {
            if (font == null)
                font = FontText;

            FooterFields.Add(new Layout()
            {
                ColName = colName,
                Text = text,
                Alignment = alignment,
                Rectangle = new XRect(CurrentPosX, CurrentPosY, width, height),
                Font = font,
                LineToDraw = lineToDraw,
                RectangleToDraw = rectangleToDraw
            });
        }

        protected void NewPage(PdfDocument document, bool drawHeader = false, bool drawHeaderSummary = false, bool drawFooter = false)
        {
            PageNumber += 1;
            if (Graphics != null)
                Graphics.Dispose();
            PdfPage page = document.AddPage();
            page.Orientation = PageOrientation;

            Graphics = XGraphics.FromPdfPage(page, XGraphicsUnit.Centimeter);
            TextFormatter = new XTextFormatter(Graphics);

            if (drawHeader) DrawHeader();
            if (drawHeaderSummary) DrawHeaderSummary();
            if (drawFooter) DrawFooter();
        }

        protected void DrawLogo()
        {

            XImage image;
            using (MemoryStream ms = new MemoryStream(Logo, false))
            {
                image = XImage.FromStream(ms);
            }
            if (image != null)
            {
                double imageWidth = image.PixelWidth;
                double imageHeight = image.PixelHeight;
                double imageRatio = imageWidth > imageHeight ? imageHeight / imageWidth : imageWidth / imageHeight;

                if (imageWidth > LogoMaxWidth)
                    imageWidth = LogoMaxWidth;

                imageHeight = imageWidth * imageRatio;

                XRect imageRect = new XRect(LeftRightPadding, TopBottomPadding, imageWidth, imageHeight);
                Graphics.DrawImage(image, imageRect);
            }
        }

        public void DrawPageNumber(PdfDocument document)
        {
            int currentPageNo = 1;
            foreach (var p in document.Pages)
            {
                DrawPageNumber(p, currentPageNo, document.PageCount);
                currentPageNo++;
            }
        }

        private void DrawPageNumber(PdfPage page, int currentPageNo, int pageCount)
        {
            XRect tmpRect = new XRect(PortraitMaxPosX - 1.6, .5, 1.5, DefaultRectHeight);
            if (PageOrientation == PageOrientation.Landscape)
                tmpRect = new XRect(LandscapeMaxPosX - 1.6, .5, 1.5, DefaultRectHeight);
            if (Graphics != null)
                Graphics.Dispose();

            var pageGraphics = XGraphics.FromPdfPage(page, XGraphicsUnit.Centimeter);
            var pageTextFormatter = new XTextFormatter(pageGraphics);
            pageTextFormatter.Alignment = XParagraphAlignment.Right;
            pageTextFormatter.DrawString("Page " + currentPageNo + " of " + pageCount.ToString(), FontText, XBrushes.Black, tmpRect);

            if (DeveloperMode)
                pageGraphics.DrawRectangle(DefaultPen, tmpRect);
        }

        private void DrawHeader()
        {
            foreach (var field in HeaderFields)
            {
                TextFormatter.Alignment = field.Alignment;
                TextFormatter.DrawString(field.Text, field.Font, XBrushes.Black, field.Rectangle, XStringFormats.TopLeft);

                if (field.LineToDraw != null)
                {
                    Graphics.DrawLine(field.LineToDraw.Pen, new XPoint(field.Rectangle.X, field.Rectangle.Y), new XPoint(field.Rectangle.X + field.Rectangle.Width, field.Rectangle.Y));
                }

                if (field.RectangleToDraw != null)
                {
                    Graphics.DrawRectangle(field.RectangleToDraw.Pen, field.Rectangle);
                }

                if (DeveloperMode)
                    Graphics.DrawRectangle(DefaultPen, field.Rectangle);
            }
        }
        
        public void DrawHeaderWithMargin(double xMargin, double yMargin)
        {
            foreach (var field in HeaderFields)
            {
                TextFormatter.Alignment = field.Alignment;
                var rect = new XRect(field.Rectangle.X + xMargin, field.Rectangle.Y + yMargin, field.Rectangle.Width - xMargin, field.Rectangle.Height - yMargin);

                TextFormatter.DrawString(field.Text, field.Font, XBrushes.Black, rect, XStringFormats.TopLeft);

                if (field.LineToDraw != null)
                {
                    Graphics.DrawLine(field.LineToDraw.Pen, new XPoint(field.Rectangle.X, field.Rectangle.Y), new XPoint(field.Rectangle.X + field.Rectangle.Width, field.Rectangle.Y));
                }

                if (field.RectangleToDraw != null)
                {
                    Graphics.DrawRectangle(field.RectangleToDraw.Pen, field.Rectangle);
                }

                if (DeveloperMode)
                    Graphics.DrawRectangle(DefaultPen, field.Rectangle);
            }
        }

        private void DrawHeaderSummary()
        {
            foreach (var field in HeaderSummaryFileds)
            {
                TextFormatter.Alignment = field.Alignment;
                TextFormatter.DrawString(field.Text, field.Font, XBrushes.Black, field.Rectangle, XStringFormats.TopLeft);

                if (field.LineToDraw != null)
                {
                    Graphics.DrawLine(field.LineToDraw.Pen, new XPoint(field.Rectangle.X, field.Rectangle.Y), new XPoint(field.Rectangle.X + field.Rectangle.Width, field.Rectangle.Y));
                }

                if (field.RectangleToDraw != null)
                {
                    Graphics.DrawRectangle(field.RectangleToDraw.Pen, field.Rectangle);
                }

                if (DeveloperMode)
                    Graphics.DrawRectangle(DefaultPen, field.Rectangle);
            }
        }

        private void DrawFooter()
        {
            foreach (var field in FooterFields)
            {
                TextFormatter.Alignment = field.Alignment;
                TextFormatter.DrawString(field.Text, field.Font, XBrushes.Black, field.Rectangle, XStringFormats.TopLeft);

                if (field.LineToDraw != null)
                {
                    Graphics.DrawLine(field.LineToDraw.Pen, new XPoint(field.Rectangle.X, field.Rectangle.Y), new XPoint(field.Rectangle.X + field.Rectangle.Width, field.Rectangle.Y));
                }

                if (field.RectangleToDraw != null)
                {
                    Graphics.DrawRectangle(field.RectangleToDraw.Pen, field.Rectangle);
                }

                if (DeveloperMode)
                    Graphics.DrawRectangle(DefaultPen, field.Rectangle);
            }
        }

        protected void DrawTableHeaders()
        {
            XRect tmpRect;
            foreach (var col in TableColumns)
            {
                tmpRect = col.Rectangle;
                tmpRect.Y = CurrentPosY;

                TextFormatter.Alignment = col.Alignment;
                TextFormatter.DrawString(col.Text, col.Font, XBrushes.Black, tmpRect, XStringFormats.TopLeft);

                if (DeveloperMode)
                    Graphics.DrawRectangle(DefaultPen, tmpRect);
            }
        }
    }
}
