// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace NUnit.UiException.CodeFormatters
{
    /// <summary>
    /// TokenDictionary is responsible for defining and identifying a set of basic
    /// strings in a given text that have a particular meaning. For instance:
    ///  - Separator, (ex: "{" ";" "]" ...)
    ///  - comment markers, (ex: "//" "/*" "*/")
    ///  - string markers, (ex: '"' '\'')
    ///  - Other -> "Text" (all other strings but the ones aboves).
    /// To achieve this, TokenDictionary firstly defines methods to register and query which
    /// strings have been registered. Secondly it defines a convenient method: TryMatch()
    /// responsible for splitting a given string in one or two parts where the first one will
    /// fall in one of the above categories. When calling TryMatch() iteratively --see Lexer--,
    /// one can tag a text into a list of tokens that might server for a semantic analysis.
    ///
    /// TokenDictionary and Lexer are both responsible for dealing with the lexical analysis
    /// job that is the first step to make basic syntax coloring. 
    /// </summary>
    /// <see cref="Lexer">Front class for the lexical analysis.</see>
    public class TokenDictionary :
        IEnumerable
    {
        private List<InternalLexToken> _list;
        private List<LexToken> _working;

        /// <summary>
        /// Build an empty instance of TokenDictionary.
        /// </summary>
        public TokenDictionary()
        {
            _list = new List<InternalLexToken>();
            _working = new List<LexToken>();

            return;
        }

        /// <summary>
        /// Gets the token count defined in this instance.
        /// </summary>
        public int Count
        {
            get { return (_list.Count); }
        }

        /// <summary>
        /// Gets the token at the given index.
        /// </summary>
        /// <param name="index">Index of the token to be returned.</param>
        /// <returns>The token at the specified index.</returns>
        public LexToken this[int index]
        {
            get { return (_list[index]); }
        }

        /// <summary>
        /// Build a new token and add it to the list of tokens known by TokenDictionary.
        /// Tokens must be added from the longest text value to the shortest otherwise
        /// an exception will be raised.
        /// </summary>
        /// <param name="value">
        /// The token's text value. It must not be null nor empty. It must not be already
        /// defined neither. If there are tokens already defined, value's length must not
        /// be longer than the previous added token.
        /// </param>
        /// <param name="tag">The token's tag value.</param>
        public void Add(string value, LexerTag tag)
        {
            InternalLexToken newToken;

            UiExceptionHelper.CheckNotNull(value, "value");
            UiExceptionHelper.CheckFalse(value == "",
                "Token value must not be empty.", "value");
            UiExceptionHelper.CheckFalse(
                Contains(value),
                String.Format("Token '{0}' is already defined.", value),
                "value");
            if (Count > 0)
                UiExceptionHelper.CheckTrue(
                    _list[Count - 1].Text.Length >= value.Length,
                    "Tokens must be inserted from the longest to the shortest value.",
                    "value");

            newToken = new InternalLexToken(value, tag);

            // loop through each item to populate
            // newToken.StartingWith list.

            foreach (InternalLexToken item in _list)
                if (item.Text.StartsWith(value))
                    newToken.StartingWith.Add(item);

            _list.Add(newToken);

            return;
        }

        /// <summary>
        /// Tests whether the given string matches a token known by this instance.
        /// </summary>
        /// <param name="value">
        ///     A string to be identify with a token in this instance.
        /// </param>
        /// <returns>
        ///     True if the string matches a token's text
        ///     value in this instance, false otherwise.
        /// </returns>
        public bool Contains(string value)
        {
            foreach (LexToken item in _list)
                if (item.Text == value)
                    return (true);
            return (false);
        }

        /// <summary>
        /// Try to match in "text" + "prediction" a token previously defined with the Add() method.
        /// Since TryMatch() may return null, it should be called from a loop that scans iteratively
        /// all characters of an input text.
        ///
        /// TryMatch() can put the caller in the two following situations: 
        /// 1) if parameters "text"+"prediction" don't hold any token, null will be returned. In this
        ///    case caller is expected to append to "text" one character more and to shift "prediction"
        ///    by one character ahead before calling TryMatch() again.
        /// 2) if parameters "text"+"prediction" look like [data]TOKEN --where [data] is any other string
        ///    but the ones in tokens-- TryMatch() will return an instance of LexToken which LexToken.Text
        ///    and LexToken.Tag properties will be setup with identified data. In this case caller is
        ///    expected to shift its reading position by the lenght of text put in LexToken.Text. Besides
        ///    "text" parameter should reset its length to 1 again.
        /// </summary>
        /// <param name="text">
        /// At the very beginning, text should be of size 1 and set up with the first character from the
        /// input text. Each time TryMatch() return null, the following character from the input text
        /// should be appended to "text". Once a token is returned, this parameter should reset its size
        /// to 1 and be filled with the character coming just after the identified string.
        /// This parameter cannot be null.
        /// </param>
        /// <param name="prediction">
        /// This parameter represents a constant sized string that goes just before the data in "text".
        /// If the caller reach the end of the text and there are not enough character to fill "prediction"
        /// completely this parameter can be filled with remaining character and eventually becoming empty.
        /// The size of this string should be equal to the lenght of the longest token defined in
        /// this instance of TokenDictionary.
        /// This parameter cannot be null.
        /// </param>
        /// <returns>
        /// The first identifiable LexToken in "text"+"prediction". Returns may be null.
        /// </returns>
        /// <see cref="Lexer.Next()">
        /// To have a look on the loop implementation..
        /// </see>
        public LexToken TryMatch(string text, string prediction)
        {
            UiExceptionHelper.CheckNotNull(text, "text");
            UiExceptionHelper.CheckNotNull(prediction, "prediction");

            foreach (InternalLexToken token in _list)
            {
                if (text.EndsWith(token.Text))
                {
                    // text may look like [data]TOKEN
                    // where [data] is normal text possibly empty.

                    if (text.Length > token.Text.Length)
                    {
                        // get only [data] part
                        return (new LexToken(
                            text.Substring(0, text.Length - token.Text.Length),
                            LexerTag.Text, -1));
                    }

                    // text looks like TOKEN, however we can't return text at
                    // this stage before testing content of prediction. Since
                    // there is a possibility that a longer TOKEN be in the concatenated
                    // string: text + prediction. (note: longer TOKENs have higher
                    // priority over shorter ones)

                    if (prediction != "")
                    {
                        string pattern;
                        int i;

                        _working.Clear();
                        PopulateTokenStartingWith(token, _working);

                        for (i = 1; i < _working.Count; ++i)
                        {
                            if (_working[i].Text.Length <= text.Length ||
                                _working[i].Text.Length > text.Length + prediction.Length)
                                continue;
                            pattern = text + prediction.Substring(0,
                                _working[i].Text.Length - text.Length);
                            if (_working[i].Text == pattern)
                                return (_working[i]);
                        }
                    }

                    return (token);
                }
            }

            // no match found, if prediction is empty
            // this means we reach end of text and return
            // text as a LexerToken.Text

            if (prediction == "")
                return (new LexToken(text, LexerTag.Text, -1));

            return (null);
        }

        /// <summary>
        /// Builds the list of all LexToken which text value starts with the one in starter.
        /// </summary>
        /// <param name="starter">The token that the reference text.</param>
        /// <param name="output">The list of tokens which text starts with the one in starter.</param>
        protected void PopulateTokenStartingWith(LexToken starter, List<LexToken> output)
        {
            InternalLexToken token;

            UiExceptionHelper.CheckNotNull(starter, "starter");
            UiExceptionHelper.CheckNotNull(output, "output");

            output.Add(starter);

            token = (InternalLexToken)starter;
            foreach (LexToken item in token.StartingWith)
                output.Add(item);

            return;
        }

        #region InternalLexToken

        /// <summary>
        /// Inherits of LexToken and add a public array that holds the list of all other tokens
        /// which text values start with the one in the current instance.
        /// </summary>
        class InternalLexToken :
            LexToken
        {
            /// <summary>
            /// Holds the list of all other tokens which text values start like the one
            /// in this instance. This array is used to solve ambiguity when finding a
            /// string that could possibly represents more than one token.
            /// </summary>
            public List<LexToken> StartingWith;

            /// <summary>
            /// Build a new instance of InternalLexToken with the given data.
            /// </summary>
            /// <param name="value">The token's text value.</param>
            /// <param name="tag">The token's tag value.</param>
            public InternalLexToken(string value, LexerTag tag)
            {
                _start = -1;
                _text = value;
                _tag = tag;

                StartingWith = new List<LexToken>();

                return;
            }
        }

        #endregion

        #region IEnumerable Membres

        public IEnumerator GetEnumerator()
        {
            return (_list.GetEnumerator());
        }

        #endregion
    }
}
