// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using NUnit.Framework;
using NUnit.UiException.StackTraceAnalyzers;
using NUnit.UiException.StackTraceAnalysers;

namespace NUnit.UiException.Tests.StackTraceAnalyzers
{
    [TestFixture]
    public class TestWindowsPathParser :
        TestIErrorParser
    {
        private IErrorParser _parser;

        [SetUp]
        public new void SetUp()
        {
            _parser = new PathCompositeParser().WindowsPathParser;

            Assert.That(_parser, Is.Not.Null);

            return;                
        }
        
        [Test]
        public void Test_Ability_To_Parse_Regular_Windows_Path()
        {
            RawError res;

            // one basic sample
            res = AcceptValue(_parser, "à NUnit.UiException.TraceItem.get_Text() dans C:\\TraceItem.cs:ligne 43");
            Assert.That(res.Path, Is.EqualTo("C:\\TraceItem.cs"));

            // check parser is not confused by file with odd extensions
            res = AcceptValue(_parser, "à get_Text() dans C:\\TraceItem.cs.cs.cs.cs:ligne 43");
            Assert.That(res.Path, Is.EqualTo("C:\\TraceItem.cs.cs.cs.cs"));

            // check it supports white space in filePath
            res = AcceptValue(_parser, "à get_Text() dans C:\\my Document1\\my document2 containing space\\file.cs:line 1");
            Assert.That(res.Path, Is.EqualTo("C:\\my Document1\\my document2 containing space\\file.cs"));

            // check it supports odd folder names ending like C# file
            res = AcceptValue(_parser, "à get_Text() dans C:\\my doc\\my doc2.cs\\file.cs:line 1");
            Assert.That(res.Path, Is.EqualTo("C:\\my doc\\my doc2.cs\\file.cs"));

            // check it supports odd drive letters
            res = AcceptValue(_parser, "à get_Text() dans Z:\\work\\folder 1\\file1.cs:line 1");
            Assert.That(res.Path, Is.EqualTo("Z:\\work\\folder 1\\file1.cs"));

            // same with lower case
            res = AcceptValue(_parser, "à get_Text() dans z:\\work\\folder 1\\file1.cs:line 1");
            Assert.That(res.Path, Is.EqualTo("z:\\work\\folder 1\\file1.cs"));

            // check it doesn't rely upon a specific file language

            res = AcceptValue(_parser, "à get_Text() dans z:\\work\\folder 1\\file1.vb:line 1");
            Assert.That(res.Path, Is.EqualTo("z:\\work\\folder 1\\file1.vb"));

            res = AcceptValue(_parser, "à get_Text() dans z:\\work\\folder 1\\file1.cpp: line 1");
            Assert.That(res.Path, Is.EqualTo("z:\\work\\folder 1\\file1.cpp"));

            // check it doesn't rely upon language at all

            res = AcceptValue(_parser, "à get_Text() dans z:\\work\\folder 1\\file1:line 1");
            Assert.That(res.Path, Is.EqualTo("z:\\work\\folder 1\\file1"));

            // check it doesn't rely upon method or line number information

            res = AcceptValue(_parser, "z:\\work\\folder\\file:");
            Assert.That(res.Path, Is.EqualTo("z:\\work\\folder\\file"));

            return;
        }

        [Test]
        public void Test_Inability_To_Parse_Non_Windows_Like_Path_Values()
        {
            // check it fails to parse Unix like filePath values
            RejectValue(_parser, "à get_Text() dans /home/ihottier/work/file1:line1");

            // check it fails to parse ill-formed windows filePath values
            RejectValue(_parser, "à get_Text() dans C::line1");
            RejectValue(_parser, "à get_Text() dans C: :line1");
            RejectValue(_parser, "à get_Text() dans C:folder 1\\file1:line1");

            // check it fails to parse missing filePath value
            RejectValue(_parser, "à get_Text()");

            return;
        }
    }
}
