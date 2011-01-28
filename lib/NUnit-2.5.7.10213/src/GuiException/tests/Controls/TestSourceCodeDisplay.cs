// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.UiException.Controls;
using NUnit.Mocks;
using System.Windows.Forms;
using NUnit.UiException.CodeFormatters;
using NUnit.UiException.Tests.data;
using System.Drawing;

namespace NUnit.UiException.Tests.Controls
{
    [TestFixture]
    public class TestSourceCodeDisplay
    {
        private TestingCode _code;
        private DynamicMock _mockStack;
        private DynamicMock _mockCode;

        [SetUp]
        public void SetUp()
        {
            Panel fakeStackControl = new Panel();
            Panel fakeCodeControl = new Panel();

            _mockStack = new DynamicMock(typeof(IStackTraceView));
            _mockCode = new DynamicMock(typeof(ICodeView));

            _mockStack.SetReturnValue("ToControl", fakeStackControl);
            _mockCode.SetReturnValue("ToControl", fakeCodeControl);

            _code = new TestingCode(
                (IStackTraceView)_mockStack.MockInstance,
                (ICodeView)_mockCode.MockInstance);

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

            using (new TestResource("Basic.cs"))
            {
                item = new ErrorItem("Basic.cs", 2);
                Assert.That(item.ReadFile(), Is.Not.Null);

                _mockStack.ExpectAndReturn("get_SelectedItem", item, null);
                _mockCode.ExpectAndReturn("get_Formatter", formatter, null);
                _mockCode.Expect("set_Text", new object[] { item.ReadFile() });
                _mockCode.Expect("set_Language", new object[] { "C#" });

                // CurrentLine is a based 0 index
                _mockCode.Expect("set_CurrentLine", new object[] { 1 });
                
                _code.RaiseSelectedItemChanged();
                _mockStack.Verify();
                _mockCode.Verify();
            }

            // test to fail:
            //
            // should handle selection changed event even
            // if selection comes to null

            _mockStack.ExpectAndReturn("get_SelectedItem", null, null);
            _mockCode.Expect("set_Text", new object[] { null });

            _code.RaiseSelectedItemChanged();
            _mockStack.Verify();
            _mockCode.Verify();

            return;
        }

        [Test]
        public void ListOrderPolicy()
        {
            _mockStack.Expect("set_ListOrderPolicy", ErrorListOrderPolicy.ReverseOrder);
            _code.ListOrderPolicy = ErrorListOrderPolicy.ReverseOrder;
            _mockStack.Verify();

            _mockStack.ExpectAndReturn("get_ListOrderPolicy", ErrorListOrderPolicy.ReverseOrder);
            Assert.That(_code.ListOrderPolicy, Is.EqualTo(ErrorListOrderPolicy.ReverseOrder));
            _mockStack.Verify();

            _mockStack.Expect("set_ListOrderPolicy", ErrorListOrderPolicy.InitialOrder);
            _code.ListOrderPolicy = ErrorListOrderPolicy.InitialOrder;
            _mockStack.Verify();

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
