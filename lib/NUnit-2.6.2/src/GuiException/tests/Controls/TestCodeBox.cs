// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if NET_3_5 || NET_4_0 || NET_4_5
using NSubstitute;
using NUnit.Framework;
using System.Drawing;
using NUnit.UiException.Controls;
using NUnit.UiException.CodeFormatters;
using System.Windows.Forms;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture, Platform("Net-3.5,Mono-3.5,Net-4.0")]
    public class TestCodeBox
    {
        private TestingCodeBox _box;

        private FormattedCode _someText;
        private FormattedCode _someCode;

        private IFormatterCatalog _mockFormatter;
        private ICodeRenderer _mockRenderer;

        [SetUp]
        public void SetUp()
        {
            _mockFormatter = Substitute.For<IFormatterCatalog>();
            _mockRenderer = Substitute.For<ICodeRenderer>();

            _box = new TestingCodeBox(_mockFormatter, _mockRenderer);
            _box.Width = 150;
            _box.Height = 150;

            _someText = Format("some C# code", "");
            _someCode = Format("some C# code", "C#");

            return;
        }

        FormattedCode Format(string text, string language)
        {
            ICodeFormatter formatter;

            if (language == "C#")
                formatter = new CSharpCodeFormatter();
            else
                formatter = new PlainTextCodeFormatter();

            return (formatter.Format(text));
        }

        TestingCodeBox SetupCodeBox(FormattedCode code, SizeF size)
        {
            TestingCodeBox box;

            box = new TestingCodeBox(_mockFormatter, _mockRenderer);

            _mockFormatter.Format(code.Text, "").Returns(code);
            _mockRenderer.GetDocumentSize(code, box.RenderingContext.Graphics, box.RenderingContext.Font).Returns(size);

            box.Text = code.Text;
            Assert.That(box.Text, Is.EqualTo(code.Text));

            return (box);
        }

        [Test]
        public void DefaultState()
        {
            CodeBox box = new CodeBox();

            Assert.That(box.Text, Is.EqualTo(""));

            Assert.That(box.Language, Is.EqualTo(""));

            Assert.True(box.AutoScroll);
            Assert.That(box.AutoScrollPosition, Is.EqualTo(new Point(0, 0)));
            Assert.That(box.AutoScrollMinSize, Is.EqualTo(new Size(0, 0)));

            Assert.False(box.ShowCurrentLine);
            Assert.That(box.CurrentLine, Is.EqualTo(-1));
            Assert.That(box.CurrentLineBackColor, Is.EqualTo(Color.Red));
            Assert.That(box.CurrentLineForeColor, Is.EqualTo(Color.White));

            Assert.That(box.BackColor, Is.EqualTo(Color.White));
            Assert.That(box.Font.Size, Is.EqualTo(8));

            return;
        }

        [Test]
        public void Format_Text_With_Language()
        {
            // when setting a text, the underlying textFormatter should be called
            // to format data in the current language mode.
            // The result should be assigned to the underlying display.

            _mockFormatter.Format(_someText.Text, "").Returns(_someText);
            _mockRenderer.GetDocumentSize(_someText, _box.RenderingContext.Graphics, _box.RenderingContext.Font).Returns(new SizeF(100, 100));

            _box.Text = _someText.Text;

            Assert.That(_box.Text, Is.EqualTo(_someText.Text));
            Assert.That(_box.AutoScrollMinSize, Is.EqualTo(new Size(100, 100)));

            // passing null to Text as same effect than passing ""

            _mockFormatter.Format("", "").Returns(FormattedCode.Empty);
            _mockRenderer.GetDocumentSize(FormattedCode.Empty, _box.RenderingContext.Graphics, _box.RenderingContext.Font).Returns(new SizeF(0, 0));

            _box.Text = null;

            Assert.That(_box.Text, Is.EqualTo(""));           

            return;
        }

        [Test]
        public void OnPaint()
        {
            TestingCodeBox box = SetupCodeBox(_someText, new SizeF(300, 400));

            box.Width = 150;
            box.Height = 150;

            box.FireOnPaint();

            _mockRenderer.Received().DrawToGraphics(_someText, box.RenderingContext, new Rectangle(0, 0, 150, 150));

            return;
        }

        [Test]
        public void Changing_Language_Causes_Reformatting()
        {
            _box = SetupCodeBox(_someCode, new SizeF(200, 400));

            _mockFormatter.Format(_someCode.Text, "C#").Returns(_someCode);
            _mockRenderer.GetDocumentSize(_someCode, _box.RenderingContext.Graphics, _box.RenderingContext.Font).Returns(new SizeF(200, 400));

            _box.Language = "C#";
            Assert.That(_box.Language, Is.EqualTo("C#"));

            // setting null in language is same as setting "" or "Plain text"

            _mockFormatter.Format(_someCode.Text, "").Returns(_someText);
            _mockRenderer.GetDocumentSize(_box.FormattedCode, _box.RenderingContext.Graphics, _box.RenderingContext.Font).Returns(new SizeF(100, 100));

            _box.Language = null;
            Assert.That(_box.Language, Is.EqualTo(""));

            return;
        }

        [Test]
        public void Changing_Font_Causes_Reformatting()
        {
            Font courier14 = new Font("Courier New", 14);

            _box = SetupCodeBox(_someCode, new SizeF(200, 400));

            _mockFormatter.Format(_someCode.Text, "").Returns(_someCode);
            _mockRenderer.GetDocumentSize(_someCode, _box.RenderingContext.Graphics, courier14).Returns(new SizeF(200, 400));

            _box.Font = courier14;

            Assert.That(_box.RenderingContext.Font, Is.SameAs(_box.Font));

            return;
        }
       
        [Test]
        public void CurrentLine()
        {
            FormattedCode data = Format(
                "line 0\r\nline 1\r\nline 2\r\nline 3\r\nline 4\r\nline 5\r\nline 6\r\nline 7\r\n", "");

            _box = SetupCodeBox(data, new SizeF(200, 400));
            _box.Height = 30;

            // CurrentLine: 0

            _mockRenderer.LineIndexToYCoordinate(0, _box.RenderingContext.Graphics, _box.RenderingContext.Font).Returns(0f);

            _box.CurrentLine = 0;

            Assert.That(_box.CurrentLine, Is.EqualTo(0));
            Assert.That(_box.AutoScrollPosition, Is.EqualTo(new Point(0, 0)));

            // CurrentLine: 7

            _mockRenderer.LineIndexToYCoordinate(7, _box.RenderingContext.Graphics, _box.RenderingContext.Font).Returns(390f);

            _box.CurrentLine = 7;

            Assert.That(_box.CurrentLine, Is.EqualTo(7));
            Assert.That(_box.AutoScrollPosition, Is.EqualTo(new Point(0, -375)));

            return;
        }

        [Test]
        public void Can_Disable_ShowCurrentLine()
        {
            TestingRenderer renderer = new TestingRenderer();

            _box = new TestingCodeBox(new GeneralCodeFormatter(), renderer);
            _box.Text = "line 1\r\nline 2\r\nline 3\r\n";
            _box.ShowCurrentLine = true;
            
            _box.CurrentLine = 1;
            _box.FireOnPaint();
            Assert.That(renderer.CURRENTLINE_INDEX, Is.EqualTo(1));            

            _box.ShowCurrentLine = false;
            _box.FireOnPaint();
            Assert.That(renderer.CURRENTLINE_INDEX, Is.EqualTo(-1));

            _box.ShowCurrentLine = true;
            _box.FireOnPaint();
            Assert.That(renderer.CURRENTLINE_INDEX, Is.EqualTo(1));

            return;
        }

        [Test]
        public void Can_Set_Back_And_Fore_Colors()
        {
            CodeBox box;

            box = new CodeBox();
            box.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

            box.CurrentLineBackColor = Color.Black;
            Assert.That(box.CurrentLineBackColor, Is.EqualTo(Color.Black));

            box.CurrentLineForeColor = Color.Blue;
            Assert.That(box.CurrentLineForeColor, Is.EqualTo(Color.Blue));

            return;
        }

        class TestingCodeBox : CodeBox
        {
            public TestingCodeBox()
            {
            }

            public TestingCodeBox(IFormatterCatalog formatter, ICodeRenderer renderer) :
                base(formatter, renderer) { }

            public CodeRenderingContext RenderingContext {
                get { return (_workingContext); }
            }

            public FormattedCode FormattedCode {
                get { return (_formattedCode); }
            }

            public new void OnScroll(ScrollEventArgs args)
            {
                base.OnScroll(args);
            }                        

            public void FireOnPaint()
            {
                OnPaint(new PaintEventArgs(_workingContext.Graphics,
                    new Rectangle(0, 0, Width, Height)));

                return;
            }
        }

        class TestingRenderer : ICodeRenderer
        {
            public int CURRENTLINE_INDEX;            

            #region ICodeRenderer Membres

            public void DrawToGraphics(FormattedCode code, CodeRenderingContext args, Rectangle viewport)
            {
                CURRENTLINE_INDEX = args.CurrentLine;
            }

            public SizeF GetDocumentSize(FormattedCode code, Graphics g, Font font)
            {
                return (new SizeF(200, 400));
            }

            public float LineIndexToYCoordinate(int lineIndex, Graphics g, Font font)
            {
                return (0);   
            }

            #endregion
        }
    }

/*    [TestFixture]
    public class TestCodeBox
    {
        private InternalCodeBox _empty;
        private InternalCodeBox _filled;

        private int _repaintNotification;
        private int _textChangedNotification;

        [SetUp]
        public void SetUp()
        {
            _empty = new InternalCodeBox();

            _filled = new InternalCodeBox();
            _filled.Text = "111\r\n" +
                           "222\r\n" +
                           "333\r\n";
            _filled.HighlightedLine = 1;

            _filled.Repainted += new RepaintEventArgs(_filled_Repainted);
            _filled.TextChanged += new EventHandler(_filled_TextChanged);

            _repaintNotification = 0;            

            return;
        }

        void _filled_TextChanged(object sender, EventArgs e)
        {
            _textChangedNotification++;
        }

        void _filled_Repainted(object sender, EventArgs e)
        {
            _repaintNotification++;
        }

        [Test]
        public void Test_Default()
        {
            Assert.That(_empty.Text, Is.EqualTo(""));
            Assert.That(_empty.HighlightedLine, Is.EqualTo(0));
            Assert.That(_empty.Viewport, Is.Not.Null);
            Assert.That(_empty.FirstLine, Is.EqualTo(""));
            Assert.That(_empty.CurrentLineNumber, Is.EqualTo(1));
            Assert.That(_empty.MouseWheelDistance, Is.EqualTo(OLD_CodeBox.DEFAULT_MOUSEWHEEL_DISTANCE));

            Assert.That(_empty.Viewport.CharHeight, Is.GreaterThan(1));
            Assert.That(_empty.Viewport.CharWidth, Is.GreaterThan(1));
            Assert.That(_empty.Viewport.Width, Is.GreaterThan(1));
            Assert.That(_empty.Viewport.Height, Is.GreaterThan(1));

            return;
        }

        [Test]
        public void Test_Filled()
        {
            Assert.That(_filled.Text, Is.EqualTo("111\r\n222\r\n333\r\n"));
            Assert.That(_filled.HighlightedLine, Is.EqualTo(1));
            Assert.That(_filled.FirstLine, Is.EqualTo("111"));
            Assert.That(_filled.CurrentLineNumber, Is.EqualTo(1));

            return;
        }

        [Test]
        public void Test_Setting_MouseWheelDistance()
        {
            _filled.MouseWheelDistance = 4;
            Assert.That(_filled.MouseWheelDistance, Is.EqualTo(4));

            _filled.MouseWheelDistance = 6;
            Assert.That(_filled.MouseWheelDistance, Is.EqualTo(6));

            return;
        }

        [Test]
        public void Test_Setting_Text()
        {
            _textChangedNotification = 0;

            _filled.Text = "hello world";
            Assert.That(_repaintNotification, Is.EqualTo(1));
            Assert.That(_textChangedNotification, Is.EqualTo(1));

            // test to fail
            _filled.Text = null;
            Assert.That(_filled.Text, Is.EqualTo(""));

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetFormattedCode_Can_Throw_FormatNullException()
        {
            _filled.SetFormattedCode(null); // throws exception
        }

        [Test]
        public void Test_Setting_FormattedCode()
        {
            CSharpCodeFormatter textFormatter;
            FormattedCode format;

            textFormatter = new CSharpCodeFormatter();
            format = textFormatter.Format("namespace test { class MyClass { } }\r\n");

            _filled.SetFormattedCode(format);
            Assert.That(_filled.Text, Is.EqualTo("namespace test { class MyClass { } }\r\n"));           

            return;
        }

        [Test]
        public void Test_Setting_Size_Invalidate_Box()
        {
            _filled.Width = 200;
            Assert.That(_repaintNotification, Is.EqualTo(1));

            _filled.Height = 400;
            Assert.That(_repaintNotification, Is.EqualTo(2));

            Assert.That(_filled.Viewport.Width, Is.EqualTo(200));
            Assert.That(_filled.Viewport.Height, Is.EqualTo(400));

            return;
        }

        [Test]
        public void Test_Setting_HighlighedLine_Invalidate_Box()
        {
            _filled.HighlightedLine = 2;
            Assert.That(_repaintNotification, Is.EqualTo(1));
        }

        [Test]
        public void Test_Changing_Location_Invalidate_Box()
        {
            _filled.Viewport.Location = new PointF(0, 1);
            Assert.That(_repaintNotification, Is.EqualTo(1));
        }

        [Test]
        public void Test_TranslateView()
        {
            _filled.Text = "******\r\n******\r\n******\r\n******\r\n******\r\n";
            _filled.Viewport.SetCharSize(1, 1);
            _filled.Viewport.SetViewport(1, 1);

            _filled.TranslateView(0, 0);
            Assert.That(_filled.Viewport.Location, Is.EqualTo(new PointF(0, 0)));

            _filled.TranslateView(2, 1);
            Assert.That(_filled.Viewport.Location, Is.EqualTo(new PointF(2, 1)));

            _filled.TranslateView(3, 1);
            Assert.That(_filled.Viewport.Location, Is.EqualTo(new PointF(5, 2)));

            return;
        }

        [Test]
        public void Test_CurrentLineNumber()
        {
            _filled.Viewport.SetViewport(1, 1);
            _filled.Viewport.SetCharSize(1, 1);

            Assert.That(_filled.CurrentLineNumber, Is.EqualTo(1));

            _filled.TranslateView(0, 1000);

            Assert.That(_filled.CurrentLineNumber,
                Is.EqualTo(_filled.Viewport.TextSource.LineCount));

            _filled.TranslateView(0, -2000);
            Assert.That(_filled.CurrentLineNumber, Is.EqualTo(1));

            return;
        }

        [Test]
        public void Test_MouseWheel_Up()
        {
            _filled.Viewport.SetViewport(1, 1);
            _filled.Viewport.SetCharSize(1, 1);

            _filled.Viewport.SetPosition(0, 2);

            _filled.MouseWheelDistance = 1;           

            _filled.HandleMouseWheelUp();
            Assert.That(_filled.Viewport.Location, Is.EqualTo(new PointF(0, 1)));

            _filled.HandleMouseWheelUp();
            Assert.That(_filled.Viewport.Location, Is.EqualTo(new PointF(0, 0)));

            return;
        }

        [Test]
        public void Test_MouseWheel_Down()
        {
            _filled.Viewport.SetViewport(1, 1);
            _filled.Viewport.SetCharSize(1, 1);

            _filled.Viewport.SetPosition(0, 0);

            _filled.MouseWheelDistance = 1;

            _filled.HandleMouseWheelDown();
            Assert.That(_filled.Viewport.Location, Is.EqualTo(new PointF(0, 1)));

            _filled.HandleMouseWheelDown();
            Assert.That(_filled.Viewport.Location, Is.EqualTo(new PointF(0, 2)));

            return;
        }

        #region InternalCodeBox

        delegate void RepaintEventArgs(object sender, EventArgs e);

        class InternalCodeBox :
            OLD_CodeBox
        {
            public event RepaintEventArgs Repainted;

            protected override void Repaint()
            {
                base.Repaint();

                if (Repainted != null)
                    Repainted(this, new EventArgs());

                return;
            }

            public new void HandleMouseWheelUp()
            {
                base.HandleMouseWheelUp();
            }

            public new void HandleMouseWheelDown()
            {
                base.HandleMouseWheelDown();
            }
        }

        #endregion
    } */
}
#endif