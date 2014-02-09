// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.UiException.CodeFormatters;

namespace NUnit.UiException.Tests.CodeFormatters
{
    [TestFixture]
    public class TestCSharpCodeFormatter
    {
        private TestingCSharpCodeFormatter _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = new TestingCSharpCodeFormatter();

            return;
        }

        [Test]
        public void Test_Default()
        {
            Assert.That(_parser.CSCode, Is.Not.Null);
            Assert.That(_parser.CSCode.Text, Is.EqualTo(""));
            Assert.That(_parser.CSCode.LineCount, Is.EqualTo(0));

            Assert.That(_parser.Language, Is.EqualTo("C#"));

            return;
        }        

        [Test]
        public void Test_PreProcess()
        {
            // PreProcess is expected to remove '\t' sequences.
            // This test expects that normal strings are left untouched.

            Assert.That(_parser.PreProcess("hello world"), Is.EqualTo("hello world"));

            // This test expects to see differences
            Assert.That(_parser.PreProcess("hello\tworld"), Is.EqualTo("hello    world"));

            // test to fail: passing null has no effect.
            Assert.That(_parser.PreProcess(null), Is.Null);

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Format_Can_Throw_CSharpNullException()
        {
            _parser.Format(null); // throws exception
        }

        [Test]
        public void Test_Format()
        {
            FormattedCode exp;
            FormattedCode res;

            res = _parser.Format("line 1\n  line 2\nline 3\n");

            exp = new FormattedCode(
                "line 1\n  line 2\nline 3\n",
                new int[] { 0, 7, 16 },
                new byte[] { 0, 0, 0 },
                new int[] { 0, 1, 2 }
                );

            Assert.That(res, Is.EqualTo(exp));

            return;
        }

        [Test]
        public void Test_Format_2()
        {
            FormattedCode exp;
            FormattedCode res;

            res = _parser.Format(
                "int i; //comment\n" +
                "char c='a';\n");

            exp = new FormattedCode(
                "int i; //comment\n" +
                "char c='a';\n",
                new int[] { 0, 3, 7, 16, 17, 21, 24, 27 },
                new byte[] { 1, 0, 2, 0, 1, 0, 3, 0 },
                new int[] { 0, 4 }
            );

            Assert.That(res, Is.EqualTo(exp));

            return;
        }

        [Test]
        public void Test_Format_3()
        {
            FormattedCode exp;
            FormattedCode res;

            // Ensure that escaping sequences are
            // handled correctly
            //                    0  2           14   17    21        
            res = _parser.Format("s=\"<font class=\\\"cls\\\">hi, there!</font>");

            exp = new FormattedCode(
                "s=\"<font class=\\\"cls\\\">hi, there!</font>",
                new int[] { 0, 2 },
                new byte[] { 0, 3 },
                new int[] { 0 });

            Assert.That(res, Is.EqualTo(exp));

            _parser = new TestingCSharpCodeFormatter();

            //                   0  2              
            res = _parser.Format("s=\"<font class=\\\\\"cls\\\">hi, there!</font>");
            exp = new FormattedCode(
                "s=\"<font class=\\\\\"cls\\\">hi, there!</font>",
                new int[] { 0, 2, 18, 22 },
                new byte[] { 0, 3, 0, 3 },
                new int[] { 0 });

            Assert.That(res, Is.EqualTo(exp));

            return;
        }

        [Test]
        public void Test_Conserve_Intermediary_Spaces()
        {
            FormattedCode res;

            res = _parser.Format(
                         "{\r\n" + 
                         "    class A { }\r\n" +
                         "}\r\n");

            Assert.That(res.LineCount, Is.EqualTo(3));
            Assert.That(res[0].Text, Is.EqualTo("{"));
            Assert.That(res[1].Text, Is.EqualTo("    class A { }"));
            Assert.That(res[2].Text, Is.EqualTo("}"));

            Assert.That(res[0][0].Text, Is.EqualTo("{"));
            Assert.That(res[1][0].Text, Is.EqualTo("    "));
            Assert.That(res[1][1].Text, Is.EqualTo("class"));
            Assert.That(res[1][2].Text, Is.EqualTo(" A { }"));
            Assert.That(res[2][0].Text, Is.EqualTo("}"));

            return;
        }

        #region TestingCSharpCodeFormatter

        class TestingCSharpCodeFormatter :
            CSharpCodeFormatter
        {
            public new string PreProcess(string text)
            {
                return (base.PreProcess(text));
            }
        }

        #endregion
    }
}
