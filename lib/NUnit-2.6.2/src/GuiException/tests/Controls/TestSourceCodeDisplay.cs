// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if NET_3_5 || NET_4_0 || NET_4_5
using NSubstitute;
using NUnit.Framework;
using NUnit.UiException.Controls;
using System.Windows.Forms;
using NUnit.UiException.CodeFormatters;
using NUnit.UiException.Tests.data;
using System.Drawing;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture, Platform("Net-3.5,Mono-3.5,Net-4.0")]
    public class TestSourceCodeDisplay
    {
        private TestingCode _code;
        private IStackTraceView _mockStack;
        private ICodeView _mockCode;

        [SetUp]
        public void SetUp()
        {
            _mockStack = Substitute.For<IStackTraceView>();
            _mockCode = Substitute.For<ICodeView>();

            _code = new TestingCode(_mockStack, _mockCode);

            return;
        }

        [Test]
        public void DefaultState()
        {
            SourceCodeDisplay code = new SourceCodeDisplay();

            Assert.NotNull(code.PluginItem);
            Assert.That(code.PluginItem.Text, Is.EqualTo("Display source code context"));
            Assert.NotNull(code.OptionItems);
            Assert.That(code.OptionItems.Length, Is.EqualTo(1));
            Assert.NotNull(code.Content);
            Assert.That(code.Content, Is.TypeOf(typeof(SplitterBox)));

            SplitterBox splitter = code.Content as SplitterBox;
            Assert.That(splitter.Controls.Count, Is.EqualTo(2));

            CodeBox codeBox = splitter.Control2 as CodeBox;
            Assert.NotNull(codeBox);
            Assert.True(codeBox.ShowCurrentLine);            
            Assert.That(codeBox.CurrentLineBackColor, Is.EqualTo(Color.Red));
            Assert.That(codeBox.CurrentLineForeColor, Is.EqualTo(Color.White));

            Assert.True(code.AutoSelectFirstItem);
            Assert.That(code.ListOrderPolicy, Is.EqualTo(ErrorListOrderPolicy.InitialOrder));
            Assert.That(code.SplitOrientation, Is.EqualTo(Orientation.Vertical));
            Assert.That(code.SplitterDistance, Is.EqualTo(0.5f));

            return;
        }

        [Test]
        public void SplitOrientation()
        {
            _code.SplitOrientation = Orientation.Horizontal;
            Assert.That(_code.SplitOrientation, Is.EqualTo(Orientation.Horizontal));
            Assert.That(_code.Splitter.Orientation, Is.EqualTo(Orientation.Horizontal));

            _code.SplitOrientation = Orientation.Vertical;
            Assert.That(_code.SplitOrientation, Is.EqualTo(Orientation.Vertical));
            Assert.That(_code.Splitter.Orientation, Is.EqualTo(Orientation.Vertical));

            return;
        }

        [Test]
        public void SplitterDistance()
        {
            _code.SplitterDistance = 0.1f;
            Assert.That(_code.Splitter.SplitterDistance, Is.EqualTo(0.1f));

            _code.SplitterDistance = 0.4f;
            Assert.That(_code.Splitter.SplitterDistance, Is.EqualTo(0.4f));

            return;
        }

        [Test]
        public void SelectedItemChanged()
        {
            GeneralCodeFormatter formatter = new GeneralCodeFormatter();
            ErrorItem item;

            // test to pass:
            //
            // handle selection changed event when there
            // is a non null selected item

            using (TestResource resource = new TestResource("Basic.cs"))
            {
                item = new ErrorItem(resource.Path, 2);
                Assert.That(item.ReadFile(), Is.Not.Null);

                _mockStack.SelectedItem.Returns(item);
                _mockCode.Formatter.Returns(formatter);
                
                _code.RaiseSelectedItemChanged();

                Assert.That(_mockCode.Text, Is.EqualTo(item.ReadFile()));
                Assert.That(_mockCode.Language, Is.EqualTo("C#"));
                // CurrentLine is a based 0 index
                Assert.That(_mockCode.CurrentLine, Is.EqualTo(1));
            }

            // test to fail:
            //
            // should handle selection changed event even
            // if selection comes to null

            _mockStack.SelectedItem.Returns((ErrorItem) null); 

            _code.RaiseSelectedItemChanged();

            Assert.That(_mockCode.Text, Is.EqualTo(null));

            return;
        }

        [Test]
        public void ListOrderPolicy()
        {
            _code.ListOrderPolicy = ErrorListOrderPolicy.ReverseOrder;
            Assert.That(_mockStack.ListOrderPolicy, Is.EqualTo(ErrorListOrderPolicy.ReverseOrder));

            _mockStack.ListOrderPolicy = ErrorListOrderPolicy.ReverseOrder;
            Assert.That(_code.ListOrderPolicy, Is.EqualTo(ErrorListOrderPolicy.ReverseOrder));

            _mockStack.ListOrderPolicy = ErrorListOrderPolicy.InitialOrder;
            _code.ListOrderPolicy = ErrorListOrderPolicy.InitialOrder;

            return;
        }

        [Test]
        public void CanReportFileException()
        {
            SourceCodeDisplay sourceDisplay = new SourceCodeDisplay();

            sourceDisplay.AutoSelectFirstItem = true;

            sourceDisplay.OnStackTraceChanged(
                "à SomeClass.SomeMethod() dans C:\\unknownFolder\\unknownFile.cs:ligne 1");

            SplitterBox splitter = sourceDisplay.Content as SplitterBox;
            CodeBox box = splitter.Control2 as CodeBox;

            Assert.IsTrue(box.Text.Contains("Cannot open file: 'C:\\unknownFolder\\unknownFile.cs'"));
            Assert.IsTrue(box.Text.Contains("Error:"));

            return;
        }

        class TestingCode : SourceCodeDisplay
        {
            public TestingCode(IStackTraceView stack, ICodeView code)
            {
                _stacktraceView = stack;
                _codeView = code;
            }

            public void RaiseSelectedItemChanged()
            {
                base.SelectedItemChanged(null, null);
            }

            public SplitterBox Splitter
            {
                get { return (_splitter); }
            }
        }
    }
}
#endif