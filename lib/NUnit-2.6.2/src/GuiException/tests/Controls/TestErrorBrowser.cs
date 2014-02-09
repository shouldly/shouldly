// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if NET_3_5 || NET_4_0 || NET_4_5
using System;
using NSubstitute;
using NUnit.Framework;
using NUnit.UiException.Controls;
using System.Windows.Forms;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture, Platform("Net-3.5,Mono-3.5,Net-4.0")]
    public class TestErrorBrowser
    {
        private TestingErrorBrowser _errorBrowser;
        private bool _stackTraceChanged;

        [SetUp]
        public void Setup()
        {
            _errorBrowser = new TestingErrorBrowser();
            _stackTraceChanged = false;
            _errorBrowser.StackTraceSourceChanged += new EventHandler(_errorBrowser_StackTraceSourceChanged);

            return;
        }

        void _errorBrowser_StackTraceSourceChanged(object sender, EventArgs e)
        {
            _stackTraceChanged = true;
        }

        [Test]
        public void DefaultState()
        {
            Assert.That(_errorBrowser.LayoutPanel, Is.Not.Null);
            Assert.That(_errorBrowser.Toolbar, Is.Not.Null);
            Assert.That(_errorBrowser.SelectedDisplay, Is.Null);
            Assert.That(_errorBrowser.StackTraceSource, Is.Null);

            Assert.That(_errorBrowser.Controls.Contains(_errorBrowser.LayoutPanel), Is.True);
            Assert.That(_errorBrowser.LayoutPanel.Left, Is.EqualTo(0));
            Assert.That(_errorBrowser.LayoutPanel.Top, Is.EqualTo(0));
            Assert.That(_errorBrowser.LayoutPanel.Toolbar.Height, Is.GreaterThan(0));

            Assert.That(_errorBrowser.LayoutPanel.Toolbar, Is.SameAs(_errorBrowser.Toolbar));
            Assert.That(_errorBrowser.Toolbar.Count, Is.EqualTo(0));

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException),
            ExpectedMessage = "display",
            MatchType = MessageMatch.Contains)]
        public void Cannot_Register_Null_Display()
        {
            _errorBrowser.RegisterDisplay(null); // throws exception
        }        

        [Test]
        public void ErrorDisplay_Plugins_life_cycle_events()
        {
            // test #1: Asks ErrorBrowser to register an instance of IErrorDisplay
            //
            // - check the process calls successively IErrorDisplay's
            //   properties & methods.
            //
            // - when registering an IErrorDisplay for the first time, ErrorBrowser
            //   should select the instance automatically.

            IErrorDisplay mockTraceDisplay = Substitute.For<IErrorDisplay>();
            IErrorDisplay mockSourceDisplay = Substitute.For<IErrorDisplay>();

            ToolStripButton tracePlugin = new ToolStripButton();
            Control traceContent = new TextBox();
            
            mockTraceDisplay.PluginItem.Returns(tracePlugin);
            mockTraceDisplay.Content.Returns(traceContent);

            _errorBrowser.RegisterDisplay(mockTraceDisplay);

            mockTraceDisplay.Received().OnStackTraceChanged(_errorBrowser.StackTraceSource);

            Assert.That(_errorBrowser.SelectedDisplay, Is.Not.Null);
            Assert.That(_errorBrowser.SelectedDisplay, Is.SameAs(mockTraceDisplay));
            Assert.That(_errorBrowser.LayoutPanel.Content, Is.EqualTo(traceContent));

            // test #2: Asks ErrorBrowser to register another instance of IErrorDisplay
            //
            // - Selection should not change

            ToolStripItem sourcePluginItem = new ToolStripButton();
            Control sourceContent = new Button();

            mockSourceDisplay.PluginItem.Returns(sourcePluginItem);

            _errorBrowser.RegisterDisplay(mockSourceDisplay);

            Assert.That(_errorBrowser.SelectedDisplay, Is.Not.Null);
            Assert.That(_errorBrowser.SelectedDisplay, Is.SameAs(mockTraceDisplay));

            // test #3: changes current selection

            mockTraceDisplay.PluginItem.Returns(tracePlugin);
            mockSourceDisplay.Content.Returns(sourceContent);

            _errorBrowser.Toolbar.SelectedDisplay = mockSourceDisplay;

            Assert.That(_errorBrowser.Toolbar.SelectedDisplay, Is.SameAs(mockSourceDisplay));
            Assert.That(_errorBrowser.LayoutPanel.Content, Is.EqualTo(sourceContent));

            // test #4: changing ErrorSource update all renderers

            string stack = "à test() C:\\file.cs:ligne 1";

            _errorBrowser.StackTraceSource = stack;
            Assert.That(_errorBrowser.LayoutPanel.Content, Is.TypeOf(typeof(Button)));

            mockTraceDisplay.Received().OnStackTraceChanged(stack);
            mockSourceDisplay.Received().OnStackTraceChanged(stack);
            
            // clears all renderers

            _errorBrowser.ClearAll();
            Assert.That(_errorBrowser.Toolbar.Count, Is.EqualTo(0));

            Assert.That(_errorBrowser.LayoutPanel.Option, Is.Not.Null);
            Assert.That(_errorBrowser.LayoutPanel.Option, Is.TypeOf(typeof(Panel)));

            Assert.That(_errorBrowser.LayoutPanel.Content, Is.Not.Null);
            Assert.That(_errorBrowser.LayoutPanel.Content, Is.TypeOf(typeof(Panel)));          
            
            return;
        }

        [Test]
        public void Can_Raise_ErrorSourceChanged()
        {
            _errorBrowser.StackTraceSource = "à test() C:\\file.cs:ligne 1";
            Assert.That(_stackTraceChanged, Is.True);

            _stackTraceChanged = false;
            _errorBrowser.StackTraceSource = null;
            Assert.That(_stackTraceChanged, Is.True);

            _stackTraceChanged = false;
            _errorBrowser.StackTraceSource = null;
            Assert.That(_stackTraceChanged, Is.False);

            return;
        }

        [Test]
        public void LayoutPanel_Auto_Resizes_When_Parent_Sizes_Change()
        {
            _errorBrowser.Width = 250;
            _errorBrowser.Height = 300;

            Assert.That(_errorBrowser.LayoutPanel.Width, Is.EqualTo(_errorBrowser.Width));
            Assert.That(_errorBrowser.LayoutPanel.Height, Is.EqualTo(_errorBrowser.Height));

            _errorBrowser.Width = 50;
            _errorBrowser.Height = 70;

            Assert.That(_errorBrowser.LayoutPanel.Width, Is.EqualTo(_errorBrowser.Width));
            Assert.That(_errorBrowser.LayoutPanel.Height, Is.EqualTo(_errorBrowser.Height));

            return;
        }

        class TestingErrorBrowser :
            ErrorBrowser
        {
            public new ErrorToolbar Toolbar
            {
                get { return (base.Toolbar); }
            }

            public new ErrorPanelLayout LayoutPanel
            {
                get { return (base.LayoutPanel); }
            }
        }
    }
}
#endif
