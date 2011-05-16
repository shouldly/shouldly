// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
#if NET_2_0
using System.Collections.Generic;
#endif

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// Abstract base class for constraints that compare values to
    /// determine if one is greater than, equal to or less than
    /// the other.
    /// </summary>
    public abstract class ComparisonConstraint : Constraint
    {
        /// <summary>
        /// The value against which a comparison is to be made
        /// </summary>
        protected object expected;
        /// <summary>
        /// If true, less than returns success
        /// </summary>
        protected bool ltOK = false;
        /// <summary>
        /// if true, equal returns success
        /// </summary>
        protected bool eqOK = false;
        /// <summary>
        /// if true, greater than returns success
        /// </summary>
        protected bool gtOK = false;
        /// <summary>
        /// The predicate used as a part of the description
        /// </summary>
        private string predicate;

        /// <summary>
        /// ComparisonAdapter to be used in making the comparison
        /// </summary>
        private ComparisonAdapter comparer = ComparisonAdapter.Default;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ComparisonConstraint"/> class.
        /// </summary>
        /// <param name="value">The value against which to make a comparison.</param>
        /// <param name="ltOK">if set to <c>true</c> less succeeds.</param>
        /// <param name="eqOK">if set to <c>true</c> equal succeeds.</param>
        /// <param name="gtOK">if set to <c>true</c> greater succeeds.</param>
        /// <param name="predicate">String used in describing the constraint.</param>
        public ComparisonConstraint(object value, bool ltOK, bool eqOK, bool gtOK, string predicate)
            : base(value)
        {
            this.expected = value;
            this.ltOK = ltOK;
            this.eqOK = eqOK;
            this.gtOK = gtOK;
            this.predicate = predicate;
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (expected == null)
                throw new ArgumentException("Cannot compare using a null reference", "expected");

            if (actual == null)
                throw new ArgumentException("Cannot compare to null reference", "actual");

            int icomp = comparer.Compare(expected, actual);

            return icomp < 0 && gtOK || icomp == 0 && eqOK || icomp > 0 && ltOK;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate(predicate);
            writer.WriteExpectedValue(expected);
        }

        /// <summary>
        /// Modifies the constraint to use an IComparer and returns self
        /// </summary>
        public ComparisonConstraint Using(IComparer comparer)
        {
            this.comparer = ComparisonAdapter.For(comparer);
            return this;
        }

#if NET_2_0
        /// <summary>
        /// Modifies the constraint to use an IComparer&lt;T&gt; and returns self
        /// </summary>
        public ComparisonConstraint Using<T>(IComparer<T> comparer)
        {
            this.comparer = ComparisonAdapter.For(comparer);
            return this;
        }

        /// <summary>
        /// Modifies the constraint to use a Comparison&lt;T&gt; and returns self
        /// </summary>
        public ComparisonConstraint Using<T>(Comparison<T> comparer)
        {
            this.comparer = ComparisonAdapter.For(comparer);
            return this;
        }
#endif
    }

    /// <summary>
    /// Tests whether a value is greater than the value supplied to its constructor
    /// </summary>
    public class GreaterThanConstraint : ComparisonConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GreaterThanConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public GreaterThanConstraint(object expected) : base(expected, false, false, true, "greater than") { }
    }

    /// <summary>
    /// Tests whether a value is greater than or equal to the value supplied to its constructor
    /// </summary>
    public class GreaterThanOrEqualConstraint : ComparisonConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GreaterThanOrEqualConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public GreaterThanOrEqualConstraint(object expected) : base(expected, false, true, true, "greater than or equal to") { }
    }

    /// <summary>
    /// Tests whether a value is less than the value supplied to its constructor
    /// </summary>
    public class LessThanConstraint : ComparisonConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:LessThanConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public LessThanConstraint(object expected) : base(expected, true, false, false, "less than") { }
    }

    /// <summary>
    /// Tests whether a value is less than or equal to the value supplied to its constructor
    /// </summary>
    public class LessThanOrEqualConstraint : ComparisonConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:LessThanOrEqualConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public LessThanOrEqualConstraint(object expected) : base(expected, true, true, false, "less than or equal to") { }
    }
}
