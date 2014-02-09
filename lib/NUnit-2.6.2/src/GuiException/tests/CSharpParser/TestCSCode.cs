// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.UiException.CodeFormatters;

namespace NUnit.UiException.Tests.CodeFormatters
{
    [TestFixture]
    public class TestFormattedCode
    {
        private FormattedCode _code;       

        [Test]
        public void Test_SimpleCollection()
        {
            _code = new TestingCSCode(
                "line 1\n  line 2\nline 3\n",
                new int[] { 0, 7, 16 },
                new byte[] { 0, 0,  0 },
                new int[] { 0, 1, 2 }
                );

            Assert.That(_code.Text, Is.EqualTo("line 1\n  line 2\nline 3\n"));
            Assert.That(_code.LineCount, Is.EqualTo(3));

            Assert.That(_code[0], Is.Not.Null);
            Assert.That(_code[0].Text, Is.EqualTo("line 1"));

            Assert.That(_code[1], Is.Not.Null);
            Assert.That(_code[1].Text, Is.EqualTo("  line 2"));

            Assert.That(_code[2], Is.Not.Null);
            Assert.That(_code[2].Text, Is.EqualTo("line 3"));

            // check internal data

            Assert.That(_code[0].Count, Is.EqualTo(1));
            Assert.That(_code[0][0].Text, Is.EqualTo("line 1"));
            Assert.That(_code[0][0].Tag, Is.EqualTo(ClassificationTag.Code));

            Assert.That(_code[1].Count, Is.EqualTo(1));
            Assert.That(_code[1][0].Text, Is.EqualTo("  line 2"));
            Assert.That(_code[1][0].Tag, Is.EqualTo(ClassificationTag.Code));

            Assert.That(_code[2].Count, Is.EqualTo(1));
            Assert.That(_code[2][0].Text, Is.EqualTo("line 3"));
            Assert.That(_code[2][0].Tag, Is.EqualTo(ClassificationTag.Code));

            return;
        }

        [Test]
        public void Empty()
        {
            Assert.NotNull(FormattedCode.Empty);
            Assert.That(FormattedCode.Empty, Is.EqualTo(new FormattedCode()));
        }

        [Test]
        public void Test_ComplexCollection()
        {
            _code = new TestingCSCode(
                "int i; //comment\n" +
                "char c='a';\n",
                new int[] { 0, 4, 7, 17, 22, 24, 27 },
                new byte[] { 1, 0, 2,  1,  0,  3,  0 },
                new int[] { 0, 3 }
            );

            Assert.That(_code.Text, Is.EqualTo("int i; //comment\nchar c='a';\n"));
            Assert.That(_code.LineCount, Is.EqualTo(2));

            Assert.That(_code[0], Is.Not.Null);
            Assert.That(_code[0].Text, Is.EqualTo("int i; //comment"));

            Assert.That(_code[1], Is.Not.Null);
            Assert.That(_code[1].Text, Is.EqualTo("char c='a';"));

            // check internal data

            Assert.That(_code[0].Count, Is.EqualTo(3));
            Assert.That(_code[0][0].Text, Is.EqualTo("int "));
            Assert.That(_code[0][0].Tag, Is.EqualTo(ClassificationTag.Keyword));
            Assert.That(_code[0][1].Text, Is.EqualTo("i; "));
            Assert.That(_code[0][1].Tag, Is.EqualTo(ClassificationTag.Code));
            Assert.That(_code[0][2].Text, Is.EqualTo("//comment"));
            Assert.That(_code[0][2].Tag, Is.EqualTo(ClassificationTag.Comment));

            Assert.That(_code[1].Count, Is.EqualTo(4));
            Assert.That(_code[1][0].Text, Is.EqualTo("char "));
            Assert.That(_code[1][0].Tag, Is.EqualTo(ClassificationTag.Keyword));
            Assert.That(_code[1][1].Text, Is.EqualTo("c="));
            Assert.That(_code[1][1].Tag, Is.EqualTo(ClassificationTag.Code));
            Assert.That(_code[1][2].Text, Is.EqualTo("'a'"));
            Assert.That(_code[1][2].Tag, Is.EqualTo(ClassificationTag.String));
            Assert.That(_code[1][3].Text, Is.EqualTo(";"));
            Assert.That(_code[1][3].Tag, Is.EqualTo(ClassificationTag.Code));

            return;
        }

        [Test]
        public void Test_MaxLength()
        {
            _code = new TestingCSCode(
                "", new int[] { }, new byte[] { }, new int[] { });
            Assert.That(_code.MaxLength, Is.EqualTo(0));

            _code = new TestingCSCode(
                "a\r\nabc\r\nab",
                new int[] { 0, 3, 8 },
                new byte[] { 0, 0, 0 },
                new int[] { 0, 1, 2 });
            Assert.That(_code.MaxLength, Is.EqualTo(3));

            _code = new TestingCSCode(
                "a\r\nab\r\nabc",
                new int[] { 0, 3, 7 },
                new byte[] { 0, 0, 0 },
                new int[] { 0, 1, 2 });
            Assert.That(_code.MaxLength, Is.EqualTo(3));

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckData_Can_Throw_NullDataException()
        {
            FormattedCode.CheckData(null); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "IndexArray.Count and TagArray.Count must match.",
            MatchType = MessageMatch.Contains)]
        public void CheckData_IndexArray_And_TagArray_Count_Must_Match()
        {
            FormattedCode.CheckData(
                new FormattedCode("hello", new int[] { 0 }, new byte[0], new int[] { 0 })); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "Bad LineArray value at index 0, value was: 1, expected to be in: [0-1[.",
            MatchType = MessageMatch.Contains)]
        public void CheckData_LineArray_Values_Must_Be_In_IndexArray_Count()
        {
            FormattedCode.CheckData(
                new FormattedCode("hi there!", new int[] { 0 }, new byte[] { 0 }, new int[] { 1 })); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "Bad LineArray[1], value was: 0, expected to be > than LineArray[0]=0.",
            MatchType = MessageMatch.Contains)]
        public void CheckData_LineArray_Values_Must_Always_Grow_Up()
        {
            FormattedCode.CheckData(
                new FormattedCode("hi\r\nthere\r\n",
                    new int[] { 0, 3 },
                    new byte[] { 0, 0 },
                    new int[] { 0, 0 })); // throws exception
        }

        [Test]
        public void Test_Equals()
        {
            _code = new TestingCSCode(
               "line",
               new int[] { 0 },
               new byte[] { 0 },
               new int[] { 0 }
               );

            // Tests to fail

            Assert.That(_code.Equals(null), Is.False);
            Assert.That(_code.Equals("hello"), Is.False);
            Assert.That(_code.Equals(
                new TestingCSCode("a", new int[] { 0 }, new byte[] { 0 }, new int[] { 0 })),
                Is.False);
            Assert.That(_code.Equals(
                new TestingCSCode("line", new int[] { 1 }, new byte[] { 0 }, new int[] { 0 })),
                Is.False);
            Assert.That(_code.Equals(
                new TestingCSCode("line", new int[] { 0 }, new byte[] { 1 }, new int[] { 0 })),
                Is.False);
            Assert.That(_code.Equals(
                new TestingCSCode("line", new int[] { 0 }, new byte[] { 0 }, new int[] { 1 })),
                Is.False);

            Assert.That(_code.Equals(
                new TestingCSCode("line", new int[] { 0, 0 }, new byte[] { 0 }, new int[] { 0 })),
                Is.False);
            Assert.That(_code.Equals(
                new TestingCSCode("line", new int[] { 0 }, new byte[] { 0, 0 }, new int[] { 0 })),
                Is.False);
            Assert.That(_code.Equals(
                new TestingCSCode("line", new int[] { 0 }, new byte[] { 0 }, new int[] { 0, 0 })),
                Is.False);

            // NUnit.UiException.Tests to pass

            Assert.That(_code.Equals(
                new TestingCSCode("line", new int[] { 0 }, new byte[] { 0 }, new int[] { 0 })),
                Is.True);

            return;
        }

        #region TestingCSCode

        class TestingCSCode :
            FormattedCode
        {
            public TestingCSCode(string csharpText, int[] strIndexes, byte[] tagValues, int[] lineIndexes)
            {
                _codeInfo = new CodeInfo();

                _codeInfo.Text = csharpText;

                _codeInfo.IndexArray = new List<int>();
                foreach (int index in strIndexes)
                    _codeInfo.IndexArray.Add(index);

                _codeInfo.TagArray = new List<byte>();
                foreach (byte tag in tagValues)
                    _codeInfo.TagArray.Add(tag);

                _codeInfo.LineArray = new List<int>();
                foreach (int line in lineIndexes)
                    _codeInfo.LineArray.Add(line);

                return;
            }         
        }

        #endregion
    }
}
