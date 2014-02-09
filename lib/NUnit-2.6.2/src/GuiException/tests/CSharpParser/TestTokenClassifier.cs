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
    public class TestTokenClassifier
    {
        private TestingClassifier _classifier;

        [SetUp]
        public void SetUp()
        {
            _classifier = new TestingClassifier();

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Classify_Can_Throw_ArgumentNullException()
        {
            _classifier.Classify(null); // throws exception
        }

        [Test]
        public void Test_NewState()
        {
            // STATE_CODE

            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CODE, LexerTag.EndOfLine),
                Is.EqualTo(TokenClassifier.SMSTATE_CODE));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CODE, LexerTag.Separator),
                Is.EqualTo(TokenClassifier.SMSTATE_CODE));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CODE, LexerTag.Text),
                Is.EqualTo(TokenClassifier.SMSTATE_CODE));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CODE, LexerTag.CommentC_Open),
                Is.EqualTo(TokenClassifier.SMSTATE_CCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CODE, LexerTag.CommentC_Close),
                Is.EqualTo(TokenClassifier.SMSTATE_CODE));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CODE, LexerTag.CommentCpp),
                Is.EqualTo(TokenClassifier.SMSTATE_CPPCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CODE, LexerTag.SingleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_CHAR));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CODE, LexerTag.DoubleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_STRING));

            // STATE_COMMENT_C

            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CCOMMENT, LexerTag.EndOfLine),
                Is.EqualTo(TokenClassifier.SMSTATE_CCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CCOMMENT, LexerTag.Separator),
                Is.EqualTo(TokenClassifier.SMSTATE_CCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CCOMMENT, LexerTag.Text),
                Is.EqualTo(TokenClassifier.SMSTATE_CCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CCOMMENT, LexerTag.CommentC_Open),
                Is.EqualTo(TokenClassifier.SMSTATE_CCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CCOMMENT, LexerTag.CommentC_Close),
                Is.EqualTo(TokenClassifier.SMSTATE_CODE));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CCOMMENT, LexerTag.CommentCpp),
                Is.EqualTo(TokenClassifier.SMSTATE_CCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CCOMMENT, LexerTag.SingleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_CCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CCOMMENT, LexerTag.DoubleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_CCOMMENT));

            // STATE_COMMENT_CPP

            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CPPCOMMENT, LexerTag.EndOfLine),
                Is.EqualTo(TokenClassifier.SMSTATE_CODE));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CPPCOMMENT, LexerTag.Separator),
                Is.EqualTo(TokenClassifier.SMSTATE_CPPCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CPPCOMMENT, LexerTag.Text),
                Is.EqualTo(TokenClassifier.SMSTATE_CPPCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CPPCOMMENT, LexerTag.CommentC_Open),
                Is.EqualTo(TokenClassifier.SMSTATE_CPPCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CPPCOMMENT, LexerTag.CommentC_Close),
                Is.EqualTo(TokenClassifier.SMSTATE_CPPCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CPPCOMMENT, LexerTag.CommentCpp),
                Is.EqualTo(TokenClassifier.SMSTATE_CPPCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CPPCOMMENT, LexerTag.SingleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_CPPCOMMENT));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CPPCOMMENT, LexerTag.DoubleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_CPPCOMMENT));

            // SMSTATE_CHAR

            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CHAR, LexerTag.EndOfLine),
                Is.EqualTo(TokenClassifier.SMSTATE_CHAR));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CHAR, LexerTag.Separator),
                Is.EqualTo(TokenClassifier.SMSTATE_CHAR));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CHAR, LexerTag.Text),
                Is.EqualTo(TokenClassifier.SMSTATE_CHAR));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CHAR, LexerTag.CommentC_Open),
                Is.EqualTo(TokenClassifier.SMSTATE_CHAR));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CHAR, LexerTag.CommentC_Close),
                Is.EqualTo(TokenClassifier.SMSTATE_CHAR));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CHAR, LexerTag.CommentCpp),
                Is.EqualTo(TokenClassifier.SMSTATE_CHAR));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CHAR, LexerTag.SingleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_CODE));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_CHAR, LexerTag.DoubleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_CHAR));

            // SMSTATE_STRING

            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_STRING, LexerTag.EndOfLine),
                Is.EqualTo(TokenClassifier.SMSTATE_STRING));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_STRING, LexerTag.Separator),
                Is.EqualTo(TokenClassifier.SMSTATE_STRING));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_STRING, LexerTag.Text),
                Is.EqualTo(TokenClassifier.SMSTATE_STRING));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_STRING, LexerTag.CommentC_Open),
                Is.EqualTo(TokenClassifier.SMSTATE_STRING));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_STRING, LexerTag.CommentC_Close),
                Is.EqualTo(TokenClassifier.SMSTATE_STRING));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_STRING, LexerTag.CommentCpp),
                Is.EqualTo(TokenClassifier.SMSTATE_STRING));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_STRING, LexerTag.SingleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_STRING));
            Assert.That(
                _classifier.GetSMSTATE(TokenClassifier.SMSTATE_STRING, LexerTag.DoubleQuote),
                Is.EqualTo(TokenClassifier.SMSTATE_CODE));

            return;
        }

        [Test]
        public void test_AcceptToken()
        {
            _checkTag(
                "\ncode/*comment*/\nint i;//comment2\nchar c='i';string s=\"test\";",
                new Couple[] {
                    new Couple(TokenClassifier.SMSTATE_CODE,            "\n"),
                    new Couple(TokenClassifier.SMSTATE_CODE,            "code"),
                    new Couple(TokenClassifier.SMSTATE_CCOMMENT,        "/*"),
                    new Couple(TokenClassifier.SMSTATE_CCOMMENT,        "comment"),
                    new Couple(TokenClassifier.SMSTATE_CCOMMENT,        "*/"),
                    new Couple(TokenClassifier.SMSTATE_CODE,            "\n"),
                    new Couple(TokenClassifier.SMSTATE_CODE,            "int"),
                    new Couple(TokenClassifier.SMSTATE_CODE,             " "),
                    new Couple(TokenClassifier.SMSTATE_CODE,             "i"),
                    new Couple(TokenClassifier.SMSTATE_CODE,             ";"),

                    new Couple(TokenClassifier.SMSTATE_CPPCOMMENT,       "//"),
                    new Couple(TokenClassifier.SMSTATE_CPPCOMMENT,       "comment2"),
                    new Couple(TokenClassifier.SMSTATE_CODE,             "\n"),
                    new Couple(TokenClassifier.SMSTATE_CODE,             "char"),
                    new Couple(TokenClassifier.SMSTATE_CODE,             " "),
                    new Couple(TokenClassifier.SMSTATE_CODE,             "c"),
                    new Couple(TokenClassifier.SMSTATE_CODE,             "="),
                    new Couple(TokenClassifier.SMSTATE_CHAR,             "'"),
                    new Couple(TokenClassifier.SMSTATE_CHAR,             "i"),
                    new Couple(TokenClassifier.SMSTATE_CHAR,             "'"),

                    new Couple(TokenClassifier.SMSTATE_CODE,             ";"),
                    new Couple(TokenClassifier.SMSTATE_CODE,             "string"),
                    new Couple(TokenClassifier.SMSTATE_CODE,             " "),
                    new Couple(TokenClassifier.SMSTATE_CODE,             "s"),
                    new Couple(TokenClassifier.SMSTATE_CODE,             "="),
                    new Couple(TokenClassifier.SMSTATE_STRING,           "\""),
                    new Couple(TokenClassifier.SMSTATE_STRING,           "test"),
                    new Couple(TokenClassifier.SMSTATE_STRING,           "\""),

                    new Couple(TokenClassifier.SMSTATE_CODE,             ";")
                });

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Classify_Throw_NullArgException()
        {
            _classifier.Classify(null); // throws exception
        }

        [Test]
        public void Test_Classify()
        {
            _checkClassification(
                "\ncode/*comment*/\nint i;//comment2\nchar c='i';string s=\"test\";",
                new Couple[] {
                new Couple(ClassificationTag.Code,         "\n"),
                new Couple(ClassificationTag.Code,         "code"),
                new Couple(ClassificationTag.Comment,      "/*"),
                new Couple(ClassificationTag.Comment,      "comment"),
                new Couple(ClassificationTag.Comment,      "*/"),
                new Couple(ClassificationTag.Code,         "\n"),
                new Couple(ClassificationTag.Keyword,      "int"),
                new Couple(ClassificationTag.Code,         " "),
                new Couple(ClassificationTag.Code,         "i"),
                new Couple(ClassificationTag.Code,         ";"),

                new Couple(ClassificationTag.Comment,      "//"),
                new Couple(ClassificationTag.Comment,      "comment2"),
                new Couple(ClassificationTag.Code,         "\n"),
                new Couple(ClassificationTag.Keyword,      "char"),
                new Couple(ClassificationTag.Code,         " "),
                new Couple(ClassificationTag.Code,         "c"),
                new Couple(ClassificationTag.Code,         "="),
                new Couple(ClassificationTag.String,       "'"),
                new Couple(ClassificationTag.String,       "i"),
                new Couple(ClassificationTag.String,       "'"),

                new Couple(ClassificationTag.Code,         ";"),
                new Couple(ClassificationTag.Keyword,      "string"),
                new Couple(ClassificationTag.Code,         " "),
                new Couple(ClassificationTag.Code,         "s"),
                new Couple(ClassificationTag.Code,         "="),
                new Couple(ClassificationTag.String,       "\""),
                new Couple(ClassificationTag.String,       "test"),
                new Couple(ClassificationTag.String,       "\""),

                new Couple(ClassificationTag.Code,         ";")
            });

            return;
        }

        [Test]
        public void Test_Classification_Cases()
        {
            _checkClassification("default:",
                new Couple[] {
                    new Couple(ClassificationTag.Keyword, "default"),
                    new Couple(ClassificationTag.Code, ":")
                });

            _checkClassification("List<string>",
                new Couple[]{
                    new Couple(ClassificationTag.Code, "List"),
                    new Couple(ClassificationTag.Code, "<"),
                    new Couple(ClassificationTag.Keyword, "string"),
                    new Couple(ClassificationTag.Code, ">")
                });

            _checkClassification("Dictionary<string, int>",
                new Couple[] {
                    new Couple(ClassificationTag.Code, "Dictionary"),
                    new Couple(ClassificationTag.Code, "<"),
                    new Couple(ClassificationTag.Keyword, "string"),
                    new Couple(ClassificationTag.Code, ","),
                    new Couple(ClassificationTag.Code, " "),
                    new Couple(ClassificationTag.Keyword, "int"),
                    new Couple(ClassificationTag.Code, ">")
                });

            return;
        }


        [Test]
        public void Test_Classify_As_Keyword()
        {
            TokenClassifier classifier;
            ClassificationTag result;
            string error;
            Lexer lexer;

            lexer = new Lexer();
            lexer.Parse(
                "abstract event new struct as explicit null switch " +
                "base extern object this bool false operator throw " +
                "break finally out true byte fixed override try case " +
                "float params typeof catch for private uint char " +
                "foreach protected ulong checked goto public unchecked " +
                "class if readonly unsafe const implicit ref ushort " +
                "continue in return using decimal int sbyte virtual " +
                "default interface sealed volatile delegate internal " +
                "short void do is sizeof while double lock stackalloc " +
                "else long static enum namespace string get set region " +
                "endregion ");

            classifier = new TokenClassifier();

            while (lexer.Next())
            {
                if (lexer.CurrentToken.Text.Trim() == "")
                    continue;

                result = classifier.Classify(lexer.CurrentToken);

                error = String.Format("Classification: [{0}] was expected for token [{1}] but [{2}] was returned.",
                    ClassificationTag.Keyword,
                    lexer.CurrentToken,
                    result);

                Assert.That(
                    result,
                    Is.EqualTo(ClassificationTag.Keyword),
                    error);
            }

            return;
        }

        [Test]
        public void Test_Reset()
        {
            Lexer lexer;

            lexer = new Lexer();
            lexer.Parse("/*int");

            lexer.Next();
            _classifier.Classify(lexer.CurrentToken);

            _classifier.Reset();
            lexer.Next();
            Assert.That(_classifier.Classify(lexer.CurrentToken), Is.EqualTo(ClassificationTag.Keyword));

            return;
        }

        [Test]
        public void Test_Escaping_sequence()
        {
            Lexer _lexer;

            _lexer = new Lexer();

            // this ensure that escaping can be set in string context only

            _lexer.Parse("\\\\");
            _classifier.Reset();
            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\\"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.Code));
            Assert.That(_classifier.Escaping, Is.False);
            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\\"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.Code));
            Assert.That(_classifier.Escaping, Is.False);


            // this ensure that parsing "\\\\" two times
            // set and unset Escaping flag correctly

            _lexer.Parse("\"\\\\\"");

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\""));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));

            Assert.That(_classifier.Escaping, Is.False);
            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\\"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));
            Assert.That(_classifier.Escaping, Is.True);

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\\"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));
            Assert.That(_classifier.Escaping, Is.False);

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\""));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));
            Assert.That(_classifier.Escaping, Is.False);


            // this ensure that first 'a' is considered as string, second as code

            _lexer.Parse("\"\\\"a\"a");
            _classifier.Reset();

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\""));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\\"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));
            Assert.That(_classifier.Escaping, Is.True);

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\""));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));
            Assert.That(_classifier.Escaping, Is.False);

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("a"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\""));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("a"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.Code));


            // another test, this time 'a' should be considered as code

            _lexer.Parse("\"\\\\\"a\"");
            _classifier.Reset();

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\""));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\\"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\\"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("\""));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.String));

            Assert.That(_lexer.Next(), Is.True);
            Assert.That(_lexer.CurrentToken.Text, Is.EqualTo("a"));
            Assert.That(_classifier.Classify(_lexer.CurrentToken), Is.EqualTo(ClassificationTag.Code));


            // this ensure that Reset() reset escaping to false

            _lexer.Parse("\"\\");
            _lexer.Next();
            _classifier.Classify(_lexer.CurrentToken);
            _lexer.Next();
            _classifier.Classify(_lexer.CurrentToken);
            Assert.That(_classifier.Escaping, Is.True);
            _classifier.Reset();
            Assert.That(_classifier.Escaping, Is.False);

            return;
        }

        #region utility methods

        private void _checkTag(string sequence, Couple[] expected)
        {
            Lexer lexer;
            int returned_tag;
            TestingClassifier classifier;
            StringBuilder recognized;
            string error;

            int i;

            classifier = new TestingClassifier();
            lexer = new Lexer();
            lexer.Parse(sequence);

            recognized = new StringBuilder();

            i = 0;
            while (lexer.Next())
            {
                recognized.Append(lexer.CurrentToken.Text);

                error = String.Format(
                    "Token [{0}] was expected, but [{1}] was returned instead, near: [{2}].",
                    expected[i].Text,
                    lexer.CurrentToken.Text,
                    recognized.ToString());
                Assert.That(lexer.CurrentToken.Text, Is.EqualTo(expected[i].Text), error);

                returned_tag = classifier.AcceptLexToken(lexer.CurrentToken);

                error = String.Format(
                    "Tag [{0}] was expected, but [{1}] was returned instead, near: [{2}].",
                    expected[i].Value,
                    returned_tag,
                    recognized.ToString());
                Assert.That(returned_tag, Is.EqualTo(expected[i].Value), error);

                i++;
            }

            Assert.That(lexer.Next(), Is.False, "Error, there are unvisited tokens left.");
            Assert.That(i == expected.Length, "Error, more tokens were expected.");

            return;
        }

        private void _checkClassification(string sequence, Couple[] expected)
        {
            Lexer lexer;
            ClassificationTag returned_tag;
            TestingClassifier classifier;
            StringBuilder recognized;
            string error;

            int i;

            classifier = new TestingClassifier();
            lexer = new Lexer();
            lexer.Parse(sequence);

            recognized = new StringBuilder();

            i = 0;
            while (lexer.Next())
            {
                recognized.Append(lexer.CurrentToken.Text);

                error = String.Format(
                    "Token [{0}] was expected, but [{1}] was returned instead, near: [{2}].",
                    expected[i].Text,
                    lexer.CurrentToken.Text,
                    recognized.ToString());
                Assert.That(lexer.CurrentToken.Text, Is.EqualTo(expected[i].Text), error);

                returned_tag = classifier.Classify(lexer.CurrentToken);

                error = String.Format(
                    "ClassificationTag [{0}] was expected, but [{1}] was returned instead, near: [{2}].",
                    expected[i].Value,
                    returned_tag,
                    recognized.ToString());
                Assert.That(returned_tag, Is.EqualTo(expected[i].Value), error);

                i++;
            }

            Assert.That(lexer.Next(), Is.False, "Error, there are unvisited tokens left.");
            Assert.That(i == expected.Length, "Error, more tokens were expected.");

            return;
        }

        #endregion

        #region TestingClassifier

        class TestingClassifier :
            TokenClassifier
        {
            public static string ClassString(int code)
            {
                switch (code)
                {
                    case TokenClassifier.SMSTATE_CODE:
                        return ("STATE_CODE");
                    case TokenClassifier.SMSTATE_CCOMMENT:
                        return ("STATE_COMMENT_C");
                    case TokenClassifier.SMSTATE_CPPCOMMENT:
                        return ("STATE_COMMENT_CPP");
                    case TokenClassifier.SMSTATE_CHAR:
                        return ("SMSTATE_CHAR");
                    case TokenClassifier.SMSTATE_STRING:
                        return ("SMSTATE_STRING");
                    default:
                        break;
                }

                return ("unknown code=" + code);
            }

            public new int AcceptLexToken(LexToken token)
            {
                return (base.AcceptLexToken(token));
            }

            public new int GetSMSTATE(int stateCode, LexerTag transition)
            {
                return (base.GetSMSTATE(stateCode, transition));
            }
        }

        #endregion

        #region Couple

        class Couple
        {
            public object Value;
            public string Text;

            public Couple(object value, string text)
            {
                Value = value;
                Text = text;
            }
        }

        #endregion
    }
}
