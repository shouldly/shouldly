// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// Tests whether a value is greater than the value supplied to its constructor
    /// </summary>
    public class GreaterThanConstraint : ComparisonConstraint
    {
        /// <summary>
        /// The value against which a comparison is to be made
        /// </summary>
        private object expected;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GreaterThanConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public GreaterThanConstraint(object expected) : base(expected)
        {
            this.expected = expected;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("greater than");
            writer.WriteExpectedValue(expected);
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (expected == null || actual == null)
                throw new ArgumentException("Cannot compare using a null reference");

            return comparer.Compare(actual, expected) > 0;
        }
    }
}
