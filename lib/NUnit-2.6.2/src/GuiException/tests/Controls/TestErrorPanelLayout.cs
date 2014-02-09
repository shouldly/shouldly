// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using NUnit.Framework;
using NUnit.UiException.Controls;
using System.Windows.Forms;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture]
    public class TestErrorPanelLayout
    {
        private ErrorPanelLayout _panel;
        private Control _aToolbar;
        private Control _aContent;

        [SetUp]
        public void SetUp()
        {
            _panel = new ErrorPanelLayout();

            _aToolbar = new Panel();
            _aContent = new Panel();

            return;
        }

        public static void CheckLayout(Control control, int left, int top, int width, int height)
        {
            Assert.That(control.Left, Is.EqualTo(left), "invalid control.Left value");
            Assert.That(control.Top, Is.EqualTo(top), "invalid control.Top value");
            Assert.That(control.Width, Is.EqualTo(width), "invalid control.Width value");
            Assert.That(control.Height, Is.EqualTo(height), "invalid control.Height value");

            return;
        }

        [Test]
        public void DefaultState()
        {
            Assert.That(_panel.Toolbar, Is.Not.Null);
            Assert.That(_panel.Content, Is.Not.Null);

            Assert.That(_panel.Controls.Count, Is.EqualTo(2));

            Assert.That(_panel.Width, Is.EqualTo(200));
            Assert.That(_panel.Height, Is.EqualTo(200));

            // checks default position and docSize for child panels

            CheckLayout(_panel.Toolbar, 0, 0, 200, ErrorPanelLayout.TOOLBAR_HEIGHT);
            CheckLayout(_panel.Content, 0, ErrorPanelLayout.TOOLBAR_HEIGHT, 200,
                200 - ErrorPanelLayout.TOOLBAR_HEIGHT);

            return;
        }

        [Test]
        public void Setting_Toolbar()
        {
            Control prev;

            prev = _panel.Toolbar;

            // replacing toolbar

            _panel.Toolbar = _aToolbar;

            Assert.That(_panel.Toolbar, Is.EqualTo(_aToolbar));
            Assert.True(_panel.Controls.Contains(_aToolbar));
            Assert.False(_panel.Controls.Contains(prev));
            CheckLayout(_panel.Toolbar, 0, 0, 200, ErrorPanelLayout.TOOLBAR_HEIGHT);

            // restoring default state

            _panel.Toolbar = null; 

            Assert.That(_panel.Toolbar, Is.EqualTo(prev));
            Assert.False(_panel.Controls.Contains(_aToolbar));
            Assert.True(_panel.Controls.Contains(prev));
            CheckLayout(_panel.Toolbar, 0, 0, 200, ErrorPanelLayout.TOOLBAR_HEIGHT);

            return;
        }        

        [Test]
        public void Setting_Content()
        {
            Control prev;

            // replacing Content

            prev = _panel.Content;
            _panel.Content = _aContent;

            Assert.That(_panel.Content, Is.EqualTo(_aContent));
            Assert.True(_panel.Controls.Contains(_aContent));
            Assert.False(_panel.Controls.Contains(prev));
            CheckLayout(_panel.Content, 0, ErrorPanelLayout.TOOLBAR_HEIGHT, 200, 
                200 - ErrorPanelLayout.TOOLBAR_HEIGHT);

            // restoring Content to its default state

            _panel.Content = null; 

            Assert.That(_panel.Content, Is.EqualTo(prev));
            Assert.False(_panel.Controls.Contains(_aContent));
            Assert.True(_panel.Controls.Contains(prev));
            CheckLayout(_panel.Content, 0, ErrorPanelLayout.TOOLBAR_HEIGHT, 200,
                200 - ErrorPanelLayout.TOOLBAR_HEIGHT);

            _panel.Content = _panel.Content; // should not cause error

            Assert.That(_panel.Content, Is.EqualTo(_panel.Content));
            Assert.That(_panel.Controls.Contains(_panel.Content));
            Assert.That(_panel.Controls.Count, Is.EqualTo(2));
            CheckLayout(_panel.Content, 0, ErrorPanelLayout.TOOLBAR_HEIGHT, 200,
                200 - ErrorPanelLayout.TOOLBAR_HEIGHT);

            return;
        }

        [Test]
        public void Can_Layout_Child_Controls_When_Size_Changed()
        {
            _panel.Width = 300;

            CheckLayout(_panel.Toolbar, 0, 0, 300, ErrorPanelLayout.TOOLBAR_HEIGHT);
            CheckLayout(_panel.Content, 0, ErrorPanelLayout.TOOLBAR_HEIGHT, 300,
                200 - ErrorPanelLayout.TOOLBAR_HEIGHT);

            _panel.Height = 400;

            CheckLayout(_panel.Toolbar, 0, 0, 300, ErrorPanelLayout.TOOLBAR_HEIGHT);
            CheckLayout(_panel.Content, 0, ErrorPanelLayout.TOOLBAR_HEIGHT, 300,
                400 - ErrorPanelLayout.TOOLBAR_HEIGHT);

            return;
        }        
    }
}
