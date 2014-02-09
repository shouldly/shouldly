// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
    /// <summary>
    /// Helper class with static methods used to supply constraints
    /// that operate on strings.
    /// </summary>
    [Obsolete("Use Is class for string constraints")]
    public class Text
    {
        #region All
        
        /// <summary>
        /// Returns a ConstraintExpression, which will apply
        /// the following constraint to all members of a collection,
        /// succeeding if all of them succeed.
        /// </summary>
        [Obsolete("Use Is.All")]
        public static ConstraintExpression All
        {
            get { return new ConstraintExpression().All; }
        }
        
        #endregion
        
        #region Contains
        
        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value contains the substring supplied as an argument.
        /// </summary>
        [Obsolete("Use Is.StringContaining")]
        public static SubstringConstraint Contains(string expected)
        {
            return new SubstringConstraint(expected);
        }
        
        #endregion
        
        #region DoesNotContain
        
        /// <summary>
        /// Returns a constraint that fails if the actual
        /// value contains the substring supplied as an argument.
        /// </summary>
        [Obsolete("Use Is.Not.StringContaining")]
        public static SubstringConstraint DoesNotContain(string expected)
        {
            return new ConstraintExpression().Not.ContainsSubstring(expected);
        }
        
        #endregion
        
        #region StartsWith
        
        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value starts with the substring supplied as an argument.
        /// </summary>
        [Obsolete("Use Is.StringStarting")]
        public static StartsWithConstraint StartsWith(string expected)
        {
            return new StartsWithConstraint(expected);
        }
        
        #endregion
        
        #region DoesNotStartWith
        
        /// <summary>
        /// Returns a constraint that fails if the actual
        /// value starts with the substring supplied as an argument.
        /// </summary>
        public static StartsWithConstraint DoesNotStartWith(string expected)
        {
            return new ConstraintExpression().Not.StartsWith(expected);
        }
        
        #endregion
        
        #region EndsWith
        
        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value ends with the substring supplied as an argument.
        /// </summary>
        [Obsolete("Use Is.StringEnding")]
        public static EndsWithConstraint EndsWith(string expected)
        {
            return new EndsWithConstraint(expected);
        }
        
        #endregion
        
        #region DoesNotEndWith
        
        /// <summary>
        /// Returns a constraint that fails if the actual
        /// value ends with the substring supplied as an argument.
        /// </summary>
        public static EndsWithConstraint DoesNotEndWith(string expected)
        {
            return new ConstraintExpression().Not.EndsWith(expected);
        }
        
        #endregion
        
        #region Matches
        
        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value matches the Regex pattern supplied as an argument.
        /// </summary>
        [Obsolete("Use Is.StringMatching")]
        public static RegexConstraint Matches(string pattern)
        {
            return new RegexConstraint(pattern);
        }
        
        #endregion
        
        #region DoesNotMatch
        
        /// <summary>
        /// Returns a constraint that fails if the actual
        /// value matches the pattern supplied as an argument.
        /// </summary>
        [Obsolete]
        public static RegexConstraint DoesNotMatch(string pattern)
        {
            return new ConstraintExpression().Not.Matches(pattern);
        }
        
        #endregion
        
    }
}
