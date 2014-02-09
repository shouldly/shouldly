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
    /// This enum indicate the kind of a string sequence.
    /// </summary>
    public enum ClassificationTag : byte
    {
        /// <summary>
        /// The string refer to C# source code.
        /// </summary>
        Code = 0,           // 0

        /// <summary>
        /// The string refers to C# keywords.
        /// </summary>
        Keyword = 1,        // 1

        /// <summary>
        /// The string refers to C# comments.
        /// </summary>
        Comment = 2,        // 2

        /// <summary>
        /// The string refers to a string/char value.
        /// </summary>
        String = 3          // 3
    }

    /// <summary>
    /// (formerly named CSToken)
    /// 
    /// Classifies a string and make it falls into one of the categories below:
    ///   - Code (the value should be interpreted as regular code)
    ///   - Keyword (the value should be interpreted as a language keyword)
    ///   - Comment (the value should be interpreted as comments)
    ///   - String (the value should be interpreted as a string)
    /// </summary>
    public class ClassifiedToken
    {
        /// <summary>
        /// The string held by this token.
        /// </summary>
        protected string _text; 

        /// <summary>
        /// The matching tag.
        /// </summary>
        protected ClassificationTag _tag;

        /// <summary>
        /// Starting startingPosition of the string.
        /// </summary>
        protected int _indexStart;

        /// <summary>
        /// This class cannot be build directly.
        /// </summary>
        protected ClassifiedToken()
        {
            // this class requires subclassing
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        public string Text
        {
            get { return (_text); }
        }

        /// <summary>
        /// Gets the classification value for the string in Text.
        ///   - Code:  Text should be interpreted as regular code,
        ///   - Keyword: Text should be interpreted as a language keyword,
        ///   - Comments: Text should be interpreted as comments,
        ///   - String: Text should be interpreted as a string.
        /// </summary>
        public ClassificationTag Tag
        {
            get { return (_tag); }
        }

        /// <summary>
        /// Gets the string's starting startingPosition.
        /// </summary>
        public int IndexStart
        {
            get { return (_indexStart); }
        }

        /// <summary>
        /// Returns true if 'obj' is an instance of ClassifiedToken 
        /// that contains same data that the current instance.
        /// </summary>
        public override bool Equals(object obj)
        {
            ClassifiedToken token;

            if (obj == null || !(obj is ClassifiedToken))
                return (false);

            token = obj as ClassifiedToken;

            return (Text == token.Text &&
                    Tag == token.Tag);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return (String.Format(
                "ClassifiedToken {Text='{0}', Tag={1}}",
                Text,
                Tag));
        }
    }
}
