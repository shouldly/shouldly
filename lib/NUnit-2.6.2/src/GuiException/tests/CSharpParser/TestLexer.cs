// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Text;
using NUnit.Framework;
using NUnit.UiException.CodeFormatters;

namespace NUnit.UiException.Tests.CodeFormatters
{
    [TestFixture]
    public class TestLexer
    {
        private TestingLexer _lexer;

        [SetUp]
        public void SetUp()
        {
            _lexer = new TestingLexer();

            return;
        }

        [Test]
        public void Test_Default()
        {
            Assert.That(_lexer.CurrentToken, Is.Null);
            Assert.That(_lexer.HasNext(), Is.False);
            Assert.That(_lexer.Next(), Is.False);

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_SetText_Throws_NullArgumentException()
        {
            _lexer.Parse(null); // throws exception
        }

        private void _checkOutput(string sequence, LexToken[] expected)
        {
            Lexer lexer;
            StringBuilder recognized;
            string error;
            int i;
            int j;

            lexer = new Lexer();
            lexer.Parse(sequence);

            recognized = new StringBuilder();

            i = 0;
            while (lexer.Next())
            {
                recognized.Append(lexer.CurrentToken.Text);

                error = String.Format("Token [{0}] was expected, but lexer returned [{1}] instead, near: [{2}].",
                    expected[i],
                    lexer.CurrentToken,
                    recognized.ToString());

                Assert.That(lexer.CurrentToken, Is.EqualTo(expected[i]), error);

                i++;
            }

            Assert.That(lexer.Next(), Is.False, "Error, there are unvisited tokens left.");

            error = "missing ";
            j = i;
            while (j < expected.Length)
            {
                error += expected[j].ToString();
                error += ", ";
                ++j;
            }

            Assert.That(i == expected.Length, "Error, more tokens were expected. {0}", error);

            return;
        }

        [Test]
        public void Test_Split_Words()
        {
            _checkOutput("one two three\r\n'",
                new LexToken[] {
                    new TestingToken("one", 0, LexerTag.Text),
                    new TestingToken(" ", 3, LexerTag.Separator),
                    new TestingToken("two", 4, LexerTag.Text),
                    new TestingToken(" ", 7, LexerTag.Separator),
                    new TestingToken("three", 8, LexerTag.Text),
                    new TestingToken("\r", 13, LexerTag.Separator),
                    new TestingToken("\n", 14, LexerTag.EndOfLine),
                    new TestingToken("'", 15, LexerTag.SingleQuote)
                });

            return;
        }

        [Test]
        public void Test_Split_SingleQuote()
        {
            _checkOutput("'hello'",
                new LexToken[] {
                    new TestingToken("'", 0, LexerTag.SingleQuote),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken("'", 6, LexerTag.SingleQuote)
                });

            return;
        }

        [Test]
        public void Test_Split_DoubleQuote()
        {
            _checkOutput("\"hello\"",
                new LexToken[] {
                    new TestingToken("\"", 0, LexerTag.DoubleQuote),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken("\"", 6, LexerTag.DoubleQuote)
                });

            // test to fail
            // lexer should not be confused by escapment character \"
            // expecting:
            //  - \"
            //  - \\\"
            //  - hello
            //  - \\\"
            //  - \"

            _checkOutput("\"\"hello\"\"",
                new LexToken[] {
                    new TestingToken("\"", 0, LexerTag.DoubleQuote),
                    new TestingToken("\"", 1, LexerTag.DoubleQuote),
                    new TestingToken("hello", 2, LexerTag.Text),
                    new TestingToken("\"", 7, LexerTag.DoubleQuote),
                    new TestingToken("\"", 8, LexerTag.DoubleQuote)
                });

            // test to fail
            // lexer should not be confused by escapment character \'
            // expecting:
            //  - \"
            //  - \\\'
            //  - A
            //  - \\\'
            //  - \"

            _checkOutput("\"\'A\'\"",
                new LexToken[] {
                    new TestingToken("\"", 0, LexerTag.DoubleQuote),
                    new TestingToken("\'", 1, LexerTag.SingleQuote),
                    new TestingToken("A", 2, LexerTag.Text),
                    new TestingToken("\'", 3, LexerTag.SingleQuote),
                    new TestingToken("\"", 4, LexerTag.DoubleQuote)
                });

            return;
        }

        [Test]
        public void Test_Split_NumberSign()
        {
            _checkOutput("#hello#",
                new LexToken[] {
                    new TestingToken("#", 0, LexerTag.Separator),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken("#", 6, LexerTag.Separator)
                });

            return;
        }

        [Test]
        public void Test_Dot_Character()
        {
            _checkOutput("this.Something",
                new LexToken[] {
                    new TestingToken("this", 0, LexerTag.Text),
                    new TestingToken(".", 4, LexerTag.Separator),
                    new TestingToken("Something", 5, LexerTag.Text)
                });

            return;
        }

        [Test]
        public void Test_Split_ColonCharacter()
        {
            _checkOutput(":a:",
                new LexToken[] {
                    new TestingToken(":", 0, LexerTag.Separator),
                    new TestingToken("a", 1, LexerTag.Text),
                    new TestingToken(":", 2, LexerTag.Separator)
                });

            _checkOutput("<a<",
                new LexToken[] {
                    new TestingToken("<", 0, LexerTag.Separator),
                    new TestingToken("a", 1, LexerTag.Text),
                    new TestingToken("<", 2, LexerTag.Separator)
                });

            _checkOutput(">a>",
                new LexToken[] {
                    new TestingToken(">", 0, LexerTag.Separator),
                    new TestingToken("a", 1, LexerTag.Text),
                    new TestingToken(">", 2, LexerTag.Separator)
                });

            _checkOutput(",a,",
                new LexToken[] {
                    new TestingToken(",", 0, LexerTag.Separator),
                    new TestingToken("a", 1, LexerTag.Text),
                    new TestingToken(",", 2, LexerTag.Separator)
                });

            return;
        }

        [Test]
        public void Test_Split_Equals()
        {
            _checkOutput("=a=",
                new LexToken[] {
                    new TestingToken("=", 0, LexerTag.Separator),
                    new TestingToken("a", 1, LexerTag.Text),
                    new TestingToken("=", 2, LexerTag.Separator)
                });

            return;
        }

        [Test]
        public void Test_Split_New_Line()
        {
            _checkOutput("\none\n",
                new LexToken[] {
                    new TestingToken("\n", 0, LexerTag.EndOfLine),
                    new TestingToken("one", 1, LexerTag.Text),
                    new TestingToken("\n", 4, LexerTag.EndOfLine)
                });

            return;
        }

        [Test]
        public void Test_Split_WhiteSpaces()
        {
            // test with space

            _checkOutput(" hello ",
                new LexToken[] {
                    new TestingToken(" ", 0, LexerTag.Separator),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken(" ", 6, LexerTag.Separator)
                });

            /// test with '\r'

            _checkOutput("\rhello\r",
                new LexToken[] {
                    new TestingToken("\r", 0, LexerTag.Separator),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken("\r", 6, LexerTag.Separator)
            });

            // test with ';'

            _checkOutput(";hello;",
                new LexToken[] {
                    new TestingToken(";", 0, LexerTag.Separator),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken(";", 6, LexerTag.Separator)
                });

            // test with '['

            _checkOutput("[hello[",
                new LexToken[] {
                    new TestingToken("[", 0, LexerTag.Separator),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken("[", 6, LexerTag.Separator)
                });

            // test with ']'

            _checkOutput("]hello]",
                new LexToken[] {
                    new TestingToken("]", 0, LexerTag.Separator),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken("]", 6, LexerTag.Separator)
                });

            // test with '('

            _checkOutput("(hello(",
                new LexToken[] {
                    new TestingToken("(", 0, LexerTag.Separator),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken("(", 6, LexerTag.Separator)
                });

            // test with ')'

            _checkOutput(")hello)",
                new LexToken[] {
                    new TestingToken(")", 0, LexerTag.Separator),
                    new TestingToken("hello", 1, LexerTag.Text),
                    new TestingToken(")", 6, LexerTag.Separator)
                });

            return;
        }

        [Test]
        public void Test_Split_CommentC()
        {
            _checkOutput("/*plop/*",
                new LexToken[] {
                    new TestingToken("/*", 0, LexerTag.CommentC_Open),
                    new TestingToken("plop", 2, LexerTag.Text),
                    new TestingToken("/*", 6, LexerTag.CommentC_Open)
                });

            // test with */
            _checkOutput("*/plop*/",
                new LexToken[] {
                new TestingToken("*/", 0, LexerTag.CommentC_Close),
                new TestingToken("plop", 2, LexerTag.Text),
                new TestingToken("*/", 6, LexerTag.CommentC_Close)
                });

            return;
        }

        [Test]
        public void Test_Split_CommentCpp()
        {
            _checkOutput("//plop//",
                new LexToken[] {
                    new TestingToken("//", 0, LexerTag.CommentCpp),
                    new TestingToken("plop", 2, LexerTag.Text),
                    new TestingToken("//", 6, LexerTag.CommentCpp)
                });

            return;
        }

        #region TestingLexer

        /// <summary>
        /// Subclasses Lexer to access and test internal methods.
        /// </summary>
        class TestingLexer :
            Lexer
        {
            public TestingLexer()
            {
            }

            public new void Clear()
            {
                base.Clear();
            }
        }

        #endregion

        #region TestingToken

        class TestingToken :
            LexToken
        {
            public TestingToken(string text, int index, LexerTag tag)
            {
                _text = text;
                _start = index;
                _tag = tag;

                return;
            }
        }

        #endregion
    }
}
