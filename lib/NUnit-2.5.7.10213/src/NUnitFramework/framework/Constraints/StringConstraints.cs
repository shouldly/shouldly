// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
#if !NETCF
using System.Text.RegularExpressions;
#endif

namespace NUnit.Framework.Constraints
{
    #region StringConstraint
    /// <summary>
    /// StringConstraint is the abstract base for constraints
    /// that operate on strings. It supports the IgnoreCase
    /// modifier for string operations.
    /// </summary>
    public abstract class StringConstraint : Constraint
    {
        /// <summary>
        /// The expected value
        /// </summary>
        protected string expected;

        /// <summary>
        /// Indicates whether tests should be case-insensitive
        /// </summary>
        protected bool caseInsensitive;

        /// <summary>
        /// Constructs a StringConstraint given an expected value
        /// </summary>
        /// <param name="expected">The expected value</param>
        public StringConstraint(string expected)
            : base(expected)
        {
            this.expected = expected;
        }

        /// <summary>
        /// Modify the constraint to ignore case in matching.
        /// </summary>
        public StringConstraint IgnoreCase
        {
            get { caseInsensitive = true; return this; }
        }
    }
    #endregion

    #region EmptyStringConstraint
    /// <summary>
    /// EmptyStringConstraint tests whether a string is empty.
    /// </summary>
    public class EmptyStringConstraint : Constraint
    {
        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (!(actual is string))
                return false;

            return (string)actual == string.Empty;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("<empty>");
        }
    }
    #endregion

    #region NullOrEmptyStringConstraint
    /// <summary>
    /// NullEmptyStringConstraint tests whether a string is either null or empty.
    /// </summary>
    public class NullOrEmptyStringConstraint : Constraint
    {
        /// <summary>
        /// Constructs a new NullOrEmptyStringConstraint
        /// </summary>
        public NullOrEmptyStringConstraint()
        {
            this.DisplayName = "nullorempty";
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (actual == null)
                return true;

            if (!(actual is string))
                throw new ArgumentException("Actual value must be a string", "actual");

            return (string)actual == string.Empty;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("null or empty string");
        }
    }
    #endregion

    #region Substring Constraint
    /// <summary>
	/// SubstringConstraint can test whether a string contains
	/// the expected substring.
	/// </summary>
    public class SubstringConstraint : StringConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SubstringConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected.</param>
        public SubstringConstraint(string expected) : base(expected) { }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;
            
            if ( !(actual is string) )
                return false;

            if (this.caseInsensitive)
                return ((string)actual).ToLower().IndexOf(expected.ToLower()) >= 0;
            else
                return ((string)actual).IndexOf(expected) >= 0;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("String containing");
            writer.WriteExpectedValue(expected);
			if ( this.caseInsensitive )
				writer.WriteModifier( "ignoring case" );
		}
    }
    #endregion

    #region StartsWithConstraint
    /// <summary>
	/// StartsWithConstraint can test whether a string starts
	/// with an expected substring.
	/// </summary>
    public class StartsWithConstraint : StringConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StartsWithConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected string</param>
        public StartsWithConstraint(string expected) : base(expected) { }

        /// <summary>
        /// Test whether the constraint is matched by the actual value.
        /// This is a template method, which calls the IsMatch method
        /// of the derived class.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (!(actual is string))
                return false;

            if ( this.caseInsensitive )
                return ((string)actual).ToLower().StartsWith(expected.ToLower());
            else
                return ((string)actual).StartsWith(expected);
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("String starting with");
            writer.WriteExpectedValue( MsgUtils.ClipString(expected, writer.MaxLineLength - 40, 0) );
			if ( this.caseInsensitive )
				writer.WriteModifier( "ignoring case" );
		}
    }
    #endregion

    #region EndsWithConstraint
    /// <summary>
    /// EndsWithConstraint can test whether a string ends
    /// with an expected substring.
    /// </summary>
    public class EndsWithConstraint : StringConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:EndsWithConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected string</param>
        public EndsWithConstraint(string expected) : base(expected) { }

        /// <summary>
        /// Test whether the constraint is matched by the actual value.
        /// This is a template method, which calls the IsMatch method
        /// of the derived class.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (!(actual is string))
                return false;

            if ( this.caseInsensitive )
                return ((string)actual).ToLower().EndsWith(expected.ToLower());
            else
                return ((string)actual).EndsWith(expected);
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("String ending with");
            writer.WriteExpectedValue(expected);
			if ( this.caseInsensitive )
				writer.WriteModifier( "ignoring case" );
		}
    }
    #endregion

    #region RegexConstraint
#if !NETCF
    /// <summary>
    /// RegexConstraint can test whether a string matches
    /// the pattern provided.
    /// </summary>
    public class RegexConstraint : StringConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:RegexConstraint"/> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        public RegexConstraint(string pattern) : base(pattern) { }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            return actual is string && 
                Regex.IsMatch( 
                    (string)actual, 
                    this.expected,
                    this.caseInsensitive ? RegexOptions.IgnoreCase : RegexOptions.None );
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("String matching");
            writer.WriteExpectedValue(this.expected);
			if ( this.caseInsensitive )
				writer.WriteModifier( "ignoring case" );
		}
    }
#endif
    #endregion
}
