// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.UiException.CodeFormatters;

namespace NUnit.UiException.Tests.CodeFormatters
{
    [TestFixture]
    public class TestTokenDictionary
    {
        private TestingTokenDictionary _emptyDictionary;
        private TokenDictionary _filledDictionary;

        [SetUp]
        public void SetUp()
        {
            Lexer lexer;

            _emptyDictionary = new TestingTokenDictionary();

            lexer = new Lexer();
            _filledDictionary = lexer.Dictionary;
            Assert.That(_filledDictionary, Is.Not.Null);
            Assert.That(_filledDictionary.Count, Is.GreaterThan(0));

            return;
        }

        [Test]
        public void test_default()
        {
            Assert.That(_emptyDictionary.Count, Is.EqualTo(0));

            return;
        }

        [Test]
        public void add_token()
        {
            _emptyDictionary.Add("=", LexerTag.Separator);

            Assert.That(_emptyDictionary.Count, Is.EqualTo(1));

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_can_throw_NullValueException()
        {
            _emptyDictionary.Add(null, LexerTag.Separator); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "Token 'one' is already defined.",
            MatchType = MessageMatch.Contains)]
        public void Add_can_throw_AlreadyDefinedException()
        {
            _emptyDictionary.Add("one", LexerTag.Text);
            _emptyDictionary.Add("one", LexerTag.Text); // throws exception

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "Token value must not be empty.",
            MatchType = MessageMatch.Contains)]
        public void Add_can_throw_EmptySequenceException()
        {
            _emptyDictionary.Add("", LexerTag.Text); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "Tokens must be inserted from the longest to the shortest value.",
            MatchType = MessageMatch.Contains)]
        public void Add_can_throw_InvalidSortException()
        {
            _emptyDictionary.Add("one", LexerTag.CommentC_Close);
            _emptyDictionary.Add("to", LexerTag.CommentC_Close);
            _emptyDictionary.Add("four", LexerTag.CommentC_Close); // throws exception

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PopulateTokenStartingWith_can_throw_NullStarterException()
        {
            _emptyDictionary.PopulateTokenStartingWith(
                null, new List<LexToken>()); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PopulateTokenStartingWith_can_throw_NullOutputException()
        {
            LexToken token;

            _emptyDictionary.Add("=", LexerTag.Separator);
            token = _emptyDictionary[0];

            _emptyDictionary.PopulateTokenStartingWith(token, null); // throws exception

            return;
        }

        [Test]
        public void PopulateTokenStartingWith()
        {
            List<LexToken> list;
            LexToken token_0;
            LexToken token_1;
            LexToken token_2;
            LexToken token_3;
            LexToken token_4;

            _emptyDictionary.Add("==", LexerTag.Separator);
            _emptyDictionary.Add("<=", LexerTag.Separator);
            _emptyDictionary.Add("=", LexerTag.Separator);
            _emptyDictionary.Add("<", LexerTag.Separator);
            _emptyDictionary.Add(";", LexerTag.Separator);

            token_0 = _emptyDictionary[0]; // ==
            token_1 = _emptyDictionary[1]; // <=
            token_2 = _emptyDictionary[2]; // =
            token_3 = _emptyDictionary[3]; // <
            token_4 = _emptyDictionary[4]; // ;

            list = new List<LexToken>();

            // there is only one token starting with text: "=="

            list.Clear();
            _emptyDictionary.PopulateTokenStartingWith(token_0, list);
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list[0], Is.EqualTo(token_0));

            // there is only one token starting with text: "<="

            list.Clear();
            _emptyDictionary.PopulateTokenStartingWith(token_1, list);
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list[0], Is.EqualTo(token_1));

            // but, two tokens start with: "="

            list.Clear();
            _emptyDictionary.PopulateTokenStartingWith(token_2, list);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0], Is.EqualTo(token_2));
            Assert.That(list[1], Is.EqualTo(token_0));

            // two tokens start with: "<"

            list.Clear();
            _emptyDictionary.PopulateTokenStartingWith(token_3, list);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0], Is.EqualTo(token_3));
            Assert.That(list[1], Is.EqualTo(token_1));

            // only one token starts with: ";"

            list.Clear();
            _emptyDictionary.PopulateTokenStartingWith(token_4, list);
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list[0], Is.EqualTo(token_4));

            return;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryMatch_can_throw_NullTextException()
        {
            _emptyDictionary.TryMatch(null, ""); // throws exception
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryMatch_can_throw_NullPredictionException()
        {
            _emptyDictionary.TryMatch("", null); // throws exception
        }

        [Test]
        public void TryMatch_no_prediction()
        {
            LexToken match;
            string[] tab;

            tab = new string[] {
                "\\\"", "\\\'", "/*", "*/", "//", "\\", " ", "\t", "\r", 
                ".", ";", "[", "]", "(", ")", "#", ":", "<", ">", "=", 
                ",", "\n", "'", "\""};

            foreach (string item in tab)
                _emptyDictionary.Add(item, LexerTag.Separator);

            // this test checks that TryMatch() doesn't fail
            // to identify tokens when they are passed to the method
            // as they are declared

            foreach (string item in tab)
            {
                match = _emptyDictionary.TryMatch(item, "");
                Assert.That(match, Is.Not.Null);
                Assert.That(match.Text, Is.EqualTo(item));
                Assert.That(match.Tag, Is.EqualTo(LexerTag.Separator));
            }

            foreach (string item in tab)
            {
                match = _emptyDictionary.TryMatch("123" + item, "");
                Assert.That(match, Is.Not.Null);
                Assert.That(match.Text, Is.EqualTo("123"));
                Assert.That(match.Tag, Is.EqualTo(LexerTag.Text));
            }

            return;
        }

        [Test]
        public void TryMatch_prediction()
        {
            LexToken match;

            _emptyDictionary.Add("===", LexerTag.Separator);
            _emptyDictionary.Add("=", LexerTag.Separator);

            // Use TryMatch as it would be by Lexer when analyzing "a=a" sequence.

            Assert.That(_emptyDictionary.TryMatch("a", "=a"), Is.Null);

            match = _emptyDictionary.TryMatch("a=", "a");      // the first 'a' should be consummed
            Assert.That(match, Is.Not.Null);
            Assert.That(match.Text, Is.EqualTo("a"));
            Assert.That(match.Tag, Is.EqualTo(LexerTag.Text));

            match = _emptyDictionary.TryMatch("=", "a");       // this should consume '='
            Assert.That(match, Is.Not.Null);
            Assert.That(match.Text, Is.EqualTo("="));
            Assert.That(match.Tag, Is.EqualTo(LexerTag.Separator));

            match = _emptyDictionary.TryMatch("a", "");       // this should consume last 'a'
            Assert.That(match, Is.Not.Null);
            Assert.That(match.Text, Is.EqualTo("a"));
            Assert.That(match.Tag, Is.EqualTo(LexerTag.Text));

            // Use TryMatch as it would be by Lexer when analyzing "a===a" sequence

            Assert.That(_emptyDictionary.TryMatch("a", "==="), Is.Null);

            match = _emptyDictionary.TryMatch("a=", "==a");  // this should consume 'a'
            Assert.That(match, Is.Not.Null);
            Assert.That(match.Text, Is.EqualTo("a"));

            match = _emptyDictionary.TryMatch("=", "==a");
            Assert.That(match, Is.Not.Null);
            Assert.That(match.Text, Is.EqualTo("==="));

            match = _emptyDictionary.TryMatch("a", "");
            Assert.That(match, Is.Not.Null);
            Assert.That(match.Text, Is.EqualTo("a"));       // this should consume last 'a'

            return;
        }

        #region TestingTokenDictionary

        class TestingTokenDictionary :
            TokenDictionary
        {
            public new void PopulateTokenStartingWith(LexToken starter, List<LexToken> output)
            {
                base.PopulateTokenStartingWith(starter, output);
            }
        }

        #endregion
    }
}
