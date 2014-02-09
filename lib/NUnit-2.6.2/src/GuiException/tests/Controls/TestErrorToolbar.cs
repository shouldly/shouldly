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
using System.Drawing;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture, Platform("Net-3.5,Mono-3.5,Net-4.0")]
    public class TestErrorToolbar
    {
        private ErrorToolbar _emptyToolbar;
        private ErrorToolbar _filledToolbar;

        private IErrorDisplay _raw;
        private IErrorDisplay _browser;

        private bool _rendererChangeNotification;

        [SetUp]
        public void SetUp()
        {
            _emptyToolbar = new ErrorToolbar();
            _filledToolbar = new ErrorToolbar();

            _raw = Substitute.For<IErrorDisplay>();
            _raw.PluginItem.Returns(new ToolStripButton());
            
            _browser = Substitute.For<IErrorDisplay>();
            _browser.PluginItem.Returns(new ToolStripButton());

            _filledToolbar.Register(_raw);
            _filledToolbar.Register(_browser);

            _rendererChangeNotification = false;
            _filledToolbar.SelectedRendererChanged += new EventHandler(_filledToolbar_SelectedRendererChanged);

            return;
        }

        void _filledToolbar_SelectedRendererChanged(object sender, EventArgs e)
        {
            _rendererChangeNotification = true;
        }

        [Test]
        public void NewStripButton()
        {
            Assert.NotNull(ErrorToolbar.NewStripButton(true, "text", new Bitmap(10, 10), null));
        }

        [Test]
        public void DefaultState()
        {
            Assert.That(_emptyToolbar.Controls.Count, Is.EqualTo(0));
            Assert.That(_emptyToolbar.Count, Is.EqualTo(0));
            Assert.That(_emptyToolbar.SelectedDisplay, Is.Null);

            Assert.That(_filledToolbar.Count, Is.EqualTo(2));
            Assert.That(_filledToolbar[0], Is.Not.Null);
            Assert.That(_filledToolbar[1], Is.Not.Null);
            Assert.NotNull(_filledToolbar.SelectedDisplay);
            Assert.That(_filledToolbar.SelectedDisplay, Is.SameAs(_raw));

            return;
        }

        [Test]
        public void Cannot_Register_Null_Display()
        {
            try {
                _emptyToolbar.Register(null); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.True(e.Message.Contains("display"));
            }

            try {
                _raw.PluginItem.Returns((ToolStripButton) null); 
                _emptyToolbar.Register(_raw); // throws exception
                Assert.Fail();
            }
            catch (Exception e) {
                Assert.True(e.Message.Contains("PluginItem"));
            }

            return;
        }

        [Test,
         ExpectedException(typeof(ArgumentException),
             ExpectedMessage = "Cannot select unregistered display.",
             MatchType = MessageMatch.Contains)]
        public void Cannot_Select_UnRegistered_Display()
        {
            IErrorDisplay unknown = Substitute.For<IErrorDisplay>();

            _filledToolbar.SelectedDisplay = unknown; // throws exception

            return;
        }

        [Test]
        public void SelectedDisplay()
        {
            // clear selection if any

            _filledToolbar.SelectedDisplay = null;

            // check ability to select raw display
            
            _filledToolbar.SelectedDisplay = _raw;
            Assert.NotNull(_filledToolbar.SelectedDisplay);
            Assert.That(_filledToolbar.SelectedDisplay, Is.SameAs(_raw));
            Assert.True(_rendererChangeNotification);

            // check ability to select browser display

            _rendererChangeNotification = false;
            _filledToolbar.SelectedDisplay = _browser;
            Assert.NotNull(_filledToolbar.SelectedDisplay);
            Assert.That(_filledToolbar.SelectedDisplay, Is.SameAs(_browser));
            Assert.True(_rendererChangeNotification);

            // check ability to clear selection

            _rendererChangeNotification = false;
            _filledToolbar.SelectedDisplay = null;
            Assert.That(_filledToolbar.SelectedDisplay, Is.Null);
            Assert.True(_rendererChangeNotification);

            // event should be raised when a real
            // change occurs

            _rendererChangeNotification = false;
            _filledToolbar.SelectedDisplay = null;
            Assert.False(_rendererChangeNotification);

            return;
        }

        [Test]
        public void Registering_displays_adds_ToolStripItem()
        {
            ToolStripButton rawView = new ToolStripButton("raw display");
            ToolStripButton browserView = new ToolStripButton("code display");
            ToolStripItem[] btns = new ToolStripItem[] { new ToolStripButton("swap") };

            // add part            
            _raw.PluginItem.Returns(rawView);
            _raw.OptionItems.Returns((ToolStripItem[]) null);
            _emptyToolbar.Register(_raw);
            Assert.True(_emptyToolbar.Items.Contains(rawView));

            _browser.PluginItem.Returns(browserView);
            _browser.OptionItems.Returns(btns);
            _emptyToolbar.Register(_browser);
            Assert.True(_emptyToolbar.Items.Contains(rawView));
            Assert.True(_emptyToolbar.Items.Contains(browserView));
            Assert.True(_emptyToolbar.Items.Contains(btns[0]));

            // clear part

            _emptyToolbar.Clear();
            Assert.That(_emptyToolbar.Count, Is.EqualTo(0));
            Assert.False(_emptyToolbar.Items.Contains(rawView));
            Assert.False(_emptyToolbar.Items.Contains(browserView));
            Assert.False(_emptyToolbar.Items.Contains(btns[0]));
            
            return;
        }

        [Test]
        public void PluginItem_Click_Raises_SelectedRenderedChanged()
        {
            ErrorToolbar toolbar = new ErrorToolbar();
            StackTraceDisplay raw = new StackTraceDisplay();
            SourceCodeDisplay code = new SourceCodeDisplay();

            toolbar.Register(raw);
            toolbar.Register(code);

            raw.PluginItem.PerformClick();
            Assert.NotNull(toolbar.SelectedDisplay);
            Assert.That(toolbar.SelectedDisplay, Is.EqualTo(raw));

            code.PluginItem.PerformClick();
            Assert.NotNull(toolbar.SelectedDisplay);
            Assert.That(toolbar.SelectedDisplay, Is.EqualTo(code));

            return;
        }

        [Test]
        public void Set_Or_Unset_Check_Flag_On_Selection()
        {
            ErrorToolbar toolbar = new ErrorToolbar();
            StackTraceDisplay raw = new StackTraceDisplay();
            SourceCodeDisplay code = new SourceCodeDisplay();

            toolbar.Register(raw);
            toolbar.Register(code);

            toolbar.SelectedDisplay = raw;
            Assert.True(((ToolStripButton)raw.PluginItem).Checked);
            Assert.False(((ToolStripButton)code.PluginItem).Checked);

            toolbar.SelectedDisplay = code;
            Assert.False(((ToolStripButton)raw.PluginItem).Checked);
            Assert.True(((ToolStripButton)code.PluginItem).Checked);

            toolbar.SelectedDisplay = null;
            Assert.False(((ToolStripButton)raw.PluginItem).Checked);
            Assert.False(((ToolStripButton)code.PluginItem).Checked);

            return;
        }        
    }
}
#endif