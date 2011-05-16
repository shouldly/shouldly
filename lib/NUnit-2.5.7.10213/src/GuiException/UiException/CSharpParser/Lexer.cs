// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.UiException.CodeFormatters
{
    /// <summary>
    /// Splits a text formatted as C# code into a list of identified tokens.
    /// </summary>
    public class Lexer
    {
        /// <summary>
        /// Reading position in the current text.
        /// </summary>
        private int _position;

        /// <summary>
        /// Text where to fetch tokens.
        /// </summary>
        private string _text;

        /// <summary>
        /// Last identified token.
        /// </summary>
        private InternalToken _token;

        /// <summary>
        /// Holds pre-defined sequences.
        /// </summary>
        private TokenDictionary _dictionary;

        /// <summary>
        /// Builds a new instance of Lexer.
        /// </summary>
        public Lexer()
        {
            _position = 0;
            _text = "";

            _dictionary = new TokenDictionary();
            _dictionary.Add("/*", LexerTag.CommentC_Open);
            _dictionary.Add("*/", LexerTag.CommentC_Close);
            _dictionary.Add("//", LexerTag.CommentCpp);

            // Here: definition of one lengthed sequences
            _dictionary.Add("\\", LexerTag.Text);
            _dictionary.Add(" ", LexerTag.Separator);
            _dictionary.Add("\t", LexerTag.Separator);
            _dictionary.Add("\r", LexerTag.Separator);
            _dictionary.Add(".", LexerTag.Separator);
            _dictionary.Add(";", LexerTag.Separator);
            _dictionary.Add("[", LexerTag.Separator);
            _dictionary.Add("]", LexerTag.Separator);
            _dictionary.Add("(", LexerTag.Separator);
            _dictionary.Add(")", LexerTag.Separator);
            _dictionary.Add("#", LexerTag.Separator);
            _dictionary.Add(":", LexerTag.Separator);
            _dictionary.Add("<", LexerTag.Separator);
            _dictionary.Add(">", LexerTag.Separator);
            _dictionary.Add("=", LexerTag.Separator);
            _dictionary.Add(",", LexerTag.Separator);
            _dictionary.Add("\n", LexerTag.EndOfLine);
            _dictionary.Add("'", LexerTag.SingleQuote);
            _dictionary.Add("\"", LexerTag.DoubleQuote);

            return;
        }

        public TokenDictionary Dictionary
        {
            get { return (_dictionary); }
        }

        /// <summary>
        /// Clear all previously defined sequences.
        /// </summary>
        protected void Clear()
        {
            _dictionary = new TokenDictionary();

            return;
        }

        /// <summary>
        /// Setup the text to be splitted in tokens. 
        /// 
        /// Client code must call Next() first before getting
        /// the first available token (if any).
        /// </summary>
        public void Parse(string codeCSharp)
        {
            UiExceptionHelper.CheckNotNull(codeCSharp, "text");

            _text = codeCSharp;
            _position = 0;

            return;
        }

        /// <summary>
        /// Gets the token identifed after a call to Next().
        /// The value may be null if the end of the text has been reached.
        /// </summary>
        public LexToken CurrentToken
        {
            get { return (_token); }
        }

        /// <summary>
        /// Checks whether there are none visited tokens.
        /// </summary>
        public bool HasNext()
        {
            return (_position < _text.Length);
        }

        /// <summary>
        /// Call this method to visit iteratively all tokens in the source text.
        /// Each time a token has been identifier, the method returns true and the
        /// identified Token is place under the CurrentToken property.
        ///   When there is not more token to visit, the method returns false
        /// and null is set in CurrentToken property.
        /// </summary>
        public bool Next()
        {
            char c;
            LexToken token;
            string prediction;
            int pos;
            int count;
            int prediction_length;

            _token = null;

            if (!HasNext())
                return (false);

            pos = _position;
            _token = new InternalToken(pos);
            prediction_length = _dictionary[0].Text.Length;

            while (pos < _text.Length)
            {
                c = _text[pos];
                _token.AppendsChar(c);

                prediction = "";
                if (pos + 1 < _text.Length)
                {
                    count = Math.Min(prediction_length, _text.Length - pos - 1);
                    prediction = _text.Substring(pos + 1, count);
                }

                token = _dictionary.TryMatch(_token.Text, prediction);

                if (token != null)
                {
                    _token.SetText(token.Text);
                    _token.SetIndex(_position);
                    _token.SetLexerTag(token.Tag);
                    _position += _token.Text.Length;

                    break;
                }

                pos++;
            }

            return (true);
        }

        #region InternalToken

        class InternalToken :
            LexToken
        {
            /// <summary>
            /// Builds a concrete instance of LexToken. By default, created instance
            /// are setup with LexerTag.Text value.
            /// </summary>
            /// <param name="startingPosition">The starting startingPosition of this token in the text.</param>
            public InternalToken(int index)
            {
                _tag = LexerTag.Text;
                _text = "";
                _start = index;

                return;
            }

            /// <summary>
            /// Appends this character to this token.
            /// </summary>
            public void AppendsChar(char c)
            {
                _text += c;
            }

            /// <summary>
            /// Removes the "count" ending character of this token.
            /// </summary>
            public void PopChars(int count)
            {
                _text = _text.Remove(_text.Length - count);
            }

            /// <summary>
            /// Set a new value to the Tag property.
            /// </summary>
            public void SetLexerTag(LexerTag tag)
            {
                _tag = tag;
            }

            public void SetText(string text)
            {
                _text = text;
            }

            public void SetIndex(int index)
            {
                _start = index;
            }
        }

        #endregion
    }
}
