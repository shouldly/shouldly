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
    /// (formerly named CSTokenCollection)
    /// 
    /// Manages an ordered collection of ClassifiedToken present in one line of text.
    /// </summary>
    public class ClassifiedTokenCollection
    {
        /// <summary>
        /// Target location when building a ClassifiedToken instance on the fly.
        /// </summary>
        protected ClassifiedToken _token;

        /// <summary>
        /// Keeps tracks of the data source.
        /// </summary>
        protected FormattedCode.CodeInfo _info;

        /// <summary>
        /// Store the current line startingPosition.
        /// </summary>
        protected int _lineIndex;

        /// <summary>
        /// This class requires subclassing.
        /// </summary>
        protected ClassifiedTokenCollection()
        {
            _token = new InternalToken();
        }

        /// <summary>
        /// Gets the number of ClassifiedToken present in this line of text.
        /// </summary>
        public int Count
        {
            get
            {
                int count;

                if (_lineIndex + 1 < _info.LineArray.Count)
                {
                    count = _info.LineArray[_lineIndex + 1] -
                            _info.LineArray[_lineIndex];
                }
                else
                {
                    count = _info.IndexArray.Count -
                            _info.LineArray[_lineIndex];
                }

                return (count);
            }
        }

        /// <summary>
        /// Gets the ClassifiedToken instance at the specified startingPosition.
        /// Warning: this indexer always return the same instance.
        /// To keep data safe, it is strongly recommanded to make
        /// a deep copy of the returned ClassifiedToken.
        /// </summary>
        /// <param name="startingPosition">A zero based value in the range: [0 - Count[</param>
        /// <returns>The ClassifiedToken at this startingPosition.</returns>
        public ClassifiedToken this[int index]
        {
            get
            {
                InternalToken result;

                result = (InternalToken)_token;
                _populateToken(_lineIndex, index, result);

                return (result); 
            }
        }

        /// <summary>
        /// Gets the part of the text at the given position.
        /// The returned string can be composed of one or severals words 
        /// all with the same style.
        /// </summary>
        private void _populateToken(int lineIndex, int tokenIndex, InternalToken output)
        {
            int tagZero;
            int tagStart;
            int tagEnd;
            int strIndex_start;
            int strIndex_end;
            string res;
            ClassificationTag tag;

            //
            // Gets value of tagStart and tagEnd
            // from which string indexes can be extracted 
            //

            tagZero = _info.LineArray[lineIndex];
            tagStart = tagZero + tokenIndex;
            tagEnd = tagStart + 1;

            strIndex_start = _info.IndexArray[tagStart];

            if (tagEnd < _info.IndexArray.Count)
            {
                strIndex_end = _info.IndexArray[tagEnd];
                res = _info.Text.Substring(strIndex_start, strIndex_end - strIndex_start);
            }
            else
                res = _info.Text.Substring(strIndex_start);

            //
            // Check the need to trimEnd() the resulting string.
            // We only want to trimEnd when current string is the
            // last part of the current line. Intermediary strings
            // must not be trimed end. At display time this would
            // lead to introduce bad shifting of text sequences.
            //

            if (res.EndsWith("\n"))
                res = res.TrimEnd();

            //
            // Convert the byte code into a ClassificationTag
            // for this string sequence
            //

            tag = _getTagFromByteValue(_info.TagArray[tagStart]);

            // and populate result

            output.Setup(res, tag);

            return;
        }

        /// <summary>
        /// Converts the given value into the matching ClassificationTag.
        /// </summary>
        private ClassificationTag _getTagFromByteValue(byte value)
        {
            switch (value)
            {
                case 0: return (ClassificationTag.Code);
                case 1: return (ClassificationTag.Keyword);
                case 2: return (ClassificationTag.Comment);
                case 3: return (ClassificationTag.String);
                default:
                    UiExceptionHelper.CheckTrue(false, "should not reach this code", "value");
                    break;
            }

            return (ClassificationTag.Code);
        }

        /// <summary>
        /// Return a string filled with the text present at the current line startingPosition.
        /// </summary>
        public string Text
        {
            get
            {
                int index_start;
                int index_start_ptr; 
                int index_end;
                int index_end_ptr;
                string text;
                
                index_start_ptr = _info.LineArray[_lineIndex];
                index_start = _info.IndexArray[index_start_ptr];

                if (_lineIndex + 1 >= _info.LineArray.Count)
                    index_end = _info.Text.Length;
                else
                {
                    index_end_ptr = _info.LineArray[_lineIndex + 1];
                    index_end = _info.IndexArray[index_end_ptr];
                }

                if (index_end - index_start < 0)
                    throw new Exception(
                        "ClassifiedTokenCollection: Text: error: calling substring with negative length");

                text = _info.Text.Substring(index_start, index_end - index_start);
                text = text.TrimEnd();

                return (text);
            }
        }

        #region InternalToken

        class InternalToken : 
            ClassifiedToken
        {
            public InternalToken()
            {
            }

            public void Setup(string text, ClassificationTag tag)
            {
                _text = text;
                _tag = tag;
            }
        }

        #endregion                   
    }
}
